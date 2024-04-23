using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace RayTraceBenchmark
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _renderersCount;
        private object _imageHeight;
        private object _imageWidth;

        public ObservableCollection<RayTracerControl> Renderers { get; } = new ObservableCollection<RayTracerControl>();

        public MainPage()
        {
            this.InitializeComponent();
            RenderersCount = 0;
        }

        public int RenderersCount
        {
            get => _renderersCount; 
            
            set
            {
                var previous = _renderersCount;
                _renderersCount = value;

                RenderersCountChanged(previous, value);
            }
        }

        private void RenderersCountChanged(int oldCount, int newCount)
        {
            if(oldCount > newCount)
            {
                for (int i = 0; i < oldCount - newCount; i++)
                {
                    Renderers.RemoveAt(Renderers.Count-1);
                }
            }
            else if (oldCount < newCount)
            {
                for (int i = 0; i < newCount - oldCount; i++)
                {
                    Renderers.Add(new RayTracerControl() { Width = Benchmark.Width, Height = Benchmark.Height });
                }
            }
        }
    }
}
