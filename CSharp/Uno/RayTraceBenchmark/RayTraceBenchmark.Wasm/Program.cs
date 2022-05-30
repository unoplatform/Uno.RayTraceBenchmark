using System;
using Microsoft.Extensions.Logging;
using Uno.Extensions;
using Uno.Foundation;

namespace RayTraceBenchmark.Wasm
{
	public class Program
	{
		private static App _app;

		static void Main(string[] args)
		{
            WebAssemblyRuntime.InvokeJS("Uno.UI.Demo.Analytics.reportPageView('main');");

			App.InitializeLogging();

            Windows.UI.Xaml.Application.Start(_ => _app = new App());
		}
    }
}
