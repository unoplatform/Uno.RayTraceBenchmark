using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RayTraceBenchmark
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			RayTraceBenchmark.Console.WriteLineCallback = print;
			BenchmarkMain.SaveImageCallback = showImage;
			await BenchmarkMain.Start();
		}

		private void print(string value)
		{
			secText.Text = value;
		}

		private void showImage(byte[] data)
		{
			var bitmap = new WriteableBitmap(Benchmark.Width, Benchmark.Height);
			var pixelBuffer = bitmap.PixelBuffer;
			var stream = pixelBuffer.AsStream();
			data = BenchmarkMain.ConvertRGBToBGRA(data);
			stream.Write(data, 0, data.Length);
			stream.Flush();
			image.Source = bitmap;
		}
	}
}
