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
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<DocumentService>();
            builder.Services.AddSingleton<WindowService>();

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

            // ❌ REMOVE THIS (do NOT auto maximize)
            // var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
            // var workArea = displayArea.WorkArea;
            // appWindow.MoveAndResize(workArea);
        });
    });
#endif
            });

            return builder.Build();
        }
    }
}