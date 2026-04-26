using DocArchive.Models;
using DocArchive.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace DocArchive
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            // =========================
            // APP SERVICES
            // =========================
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<DocumentService>();
            builder.Services.AddSingleton<WindowService>();

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<AuthMessageHandler>();

            builder.Services.AddHttpClient("AuthorizedClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5250/");
            })
            .AddHttpMessageHandler<AuthMessageHandler>();
            builder.Services.AddMauiBlazorWebView();


            // =========================
            // HTTP CLIENT (FIXED)
            // =========================
            builder.Services.AddScoped(sp =>
            {
                var http = new HttpClient();

                // IMPORTANT:
                // Windows uses localhost
                // Android emulator uses 10.0.2.2
                // Physical device uses PC IP

#if WINDOWS
                http.BaseAddress = new Uri("http://localhost:5250/");
#else
                http.BaseAddress = new Uri("http://10.0.2.2:5250/");
#endif

                return http;
            });


            // =========================
            // WINDOWS WINDOW CONTROL
            // =========================
            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated(window =>
                    {
                        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                        // Keep default window behavior
                    });
                });
#endif
            });

            return builder.Build();
        }


    }
}