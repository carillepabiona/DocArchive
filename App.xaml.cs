using DocArchive.Services;

namespace DocArchive
{
    public partial class App : Application
    {
        private readonly WindowService _windowService;

        public App(WindowService windowService)
        {
            InitializeComponent();
            _windowService = windowService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new MainPage())
            {
                Title = "DocArchive"
            };

            window.Created += (s, e) =>
            {
                _windowService.Attach(window);
                _windowService.SetLoginMode();
            };

            return window;
        }
    }
}