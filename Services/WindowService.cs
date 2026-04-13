#if WINDOWS
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
#endif

namespace DocArchive.Services
{
    public class WindowService
    {
#if WINDOWS
        private AppWindow? _appWindow;
#endif

        public void Attach(Microsoft.Maui.Controls.Window mauiWindow)
        {
#if WINDOWS
            var hwnd = WindowNative.GetWindowHandle(mauiWindow.Handler.PlatformView);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            _appWindow = AppWindow.GetFromWindowId(windowId);
#endif
        }

        // ================= LOGIN MODE (HIDE TITLE BAR FULLY) =================
        public void SetLoginMode()
        {
#if WINDOWS
            if (_appWindow == null) return;

            var presenter = _appWindow.Presenter as OverlappedPresenter;

            // 🔥 THIS IS THE KEY: REMOVE TITLE BAR STYLE
            presenter.SetBorderAndTitleBar(false, false);

            presenter.IsMaximizable = false;
            presenter.IsMinimizable = false;
            presenter.IsResizable = false;

            _appWindow.Resize(new SizeInt32(670, 620));
            CenterWindow();
#endif
        }

        // ================= NORMAL MODE (RESTORE TITLE BAR) =================
        public void SetMainAppMode()
        {
#if WINDOWS
            if (_appWindow == null) return;

            var presenter = _appWindow.Presenter as OverlappedPresenter;

            // 🔥 RESTORE TITLE BAR
            presenter.SetBorderAndTitleBar(true, true);

            presenter.IsMaximizable = true;
            presenter.IsMinimizable = true;
            presenter.IsResizable = true;

            _appWindow.Resize(new SizeInt32(1200, 800));
#endif
        }

#if WINDOWS
        private void CenterWindow()
        {
            var windowId = _appWindow.Id;
            var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);

            int x = (displayArea.WorkArea.Width - _appWindow.Size.Width) / 2;
            int y = (displayArea.WorkArea.Height - _appWindow.Size.Height) / 2;

            _appWindow.Move(new PointInt32(x, y));
        }
#endif

        // ================= EXIT APP =================
        public void ExitApp()
        {
#if WINDOWS
            App.Current?.CloseWindow(App.Current.Windows[0]);
#endif
        }
    }
}