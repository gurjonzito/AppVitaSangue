namespace AppVitaSangue
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Preferences.ContainsKey("user_theme_preference"))
            {
                var theme = Preferences.Get("user_theme_preference", "Light");
                UserAppTheme = theme == "Dark" ? AppTheme.Dark : AppTheme.Light;
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            window.Height = 800;
            window.Width = 400;

            return window;
        }
    }
}