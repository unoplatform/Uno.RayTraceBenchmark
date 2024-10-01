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
            var r = Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_FEATURES")?.Split(',');
            var r2 = r?.Contains("threads", StringComparer.OrdinalIgnoreCase) ?? false;

            Console.WriteLine($"Main: r.Length:{r.Length} r2:{r2} r3:[{r[0]}] UNO_BOOTSTRAP_MONO_RUNTIME_FEATURES:{Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_FEATURES")}");
            // WebAssemblyRuntime.InvokeJS("Uno.UI.Demo.Analytics.reportPageView('main');");

            App.InitializeLogging();

            Microsoft.UI.Xaml.Application.Start(_ => _app = new App());
		}
    }
}
