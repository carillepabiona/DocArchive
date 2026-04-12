using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;
using DocArchive.Services;
using DocArchive.Models;


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
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<DocumentService>();

            builder.Services.AddScoped(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5250/")
                };
            });
#endif

            // 🖥️ WINDOWS WINDOW CONTROL
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

                        // ✅ Recommended: Maximized (NOT fullscreen)
                        var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                            windowId,
                            Microsoft.UI.Windowing.DisplayAreaFallback.Primary
                        );

                        var workArea = displayArea.WorkArea;
                        appWindow.MoveAndResize(workArea);
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}