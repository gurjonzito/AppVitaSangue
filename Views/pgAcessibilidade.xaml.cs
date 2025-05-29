using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace AppVitaSangue.Views
{
    public partial class pgAcessibilidade : ContentPage
    {
        public pgAcessibilidade()
        {
            InitializeComponent();

            // Remove o binding do XAML e configura manualmente
            themeSwitch.IsToggled = Application.Current.RequestedTheme == AppTheme.Dark;
        }

        private void OnThemeToggled(object sender, ToggledEventArgs e)
        {
            // Verifica��o adicional de seguran�a
            if (Application.Current == null) return;

            // Atualiza o tema
            Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;

            // Opcional: Salvar prefer�ncia
            Preferences.Set("user_theme_preference", e.Value ? "Dark" : "Light");
        }
    }
}