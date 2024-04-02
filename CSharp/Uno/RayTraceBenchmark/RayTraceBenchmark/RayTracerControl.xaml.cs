using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RayTraceBenchmark
{
    public sealed partial class RayTracerControl : UserControl
    {
        private bool _isRunning = false;
        private object _pixelGate = new object();

        public RayTracerControl()
        {
            this.InitializeComponent();

            Loaded += RayTracerControl_Loaded;
            Unloaded += RayTracerControl_Unloaded;
        }

        private void RayTracerControl_Loaded(object sender, RoutedEventArgs e)
        {
            StartRender();
        }

        private void RayTracerControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _isRunning = false;
        }

        private void StartRender()
        {
            var bench = new Benchmark2();

            var rgbaBuffer = new byte[(bench.Pixels.Length / 3) * 4];

            var sw = new Stopwatch();
            bool queued = false;
            _isRunning = true;

            System.Console.WriteLine($"Creating renderer");

            Task.Run(() =>
            {
                while (_isRunning)
                {
                    sw.Reset();
                    sw.Start();
                    bench.Render();
                    sw.Stop();

                    if (!queued)
                    {
                        queued = true;

                        var elapsed = sw.Elapsed;

                        lock (_pixelGate)
                        {
                            BenchmarkMain.ConvertRGBToBGRA(bench.Pixels, rgbaBuffer);
                        }

                        _ = Dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                if (_isRunning)
                                {
                                    var bitmap = new WriteableBitmap(Benchmark.Width, Benchmark.Height);
                                    var pixelBuffer = bitmap.PixelBuffer;
                                    var stream = pixelBuffer.AsStream();
                                    lock (_pixelGate)
                                    {
                                        stream.Write(rgbaBuffer, 0, rgbaBuffer.Length);
                                    }
                                    stream.Flush();

                                    image.Source = bitmap;
                                    queued = false;

                                    info.Text = $"FPS: {1 / elapsed.TotalSeconds: 0.000}";
                                }
                            }
                        );
                    }
                }
            });
        }
    }
}
