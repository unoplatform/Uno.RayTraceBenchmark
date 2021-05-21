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

#if DEBUG
			ConfigureFilters(LogExtensionPoint.AmbientLoggerFactory);
#endif

            Windows.UI.Xaml.Application.Start(_ => _app = new App());
		}
		static void ConfigureFilters(ILoggerFactory factory)
		{
			factory
				.WithFilter(new FilterLoggerSettings
					{
						{ "Uno", LogLevel.Warning },
						{ "Windows", LogLevel.Warning },
						{ "SampleControl.Presentation", LogLevel.Debug },

						// Generic Xaml events
						// { "Windows.UI.Xaml", LogLevel.Debug },

						// { "Uno.UI.Controls.AsyncValuePresenter", LogLevel.Debug },
						// { "Uno.UI.Controls.IfDataContext", LogLevel.Debug },
						   
						// Layouter specific messages
						// { "Windows.UI.Xaml.Controls", LogLevel.Debug },
						//{ "Windows.UI.Xaml.Controls.Layouter", LogLevel.Debug },
						//{ "Windows.UI.Xaml.Controls.Panel", LogLevel.Debug },
						   
						// Binding related messages
						// { "Windows.UI.Xaml.Data", LogLevel.Debug },
						// { "Windows.UI.Xaml.Data", LogLevel.Debug },
						   
						//  Binder memory references tracking
						// { "ReferenceHolder", LogLevel.Debug },
					}
				)
				.AddConsole(LogLevel.Debug);
		}
	}
}
