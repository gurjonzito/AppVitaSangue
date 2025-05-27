using AppVitaSangue.Controllers;

namespace AppVitaSangue.Views;

public partial class pgLogin : ContentPage
{
    private DoadorController doadorController;

    public pgLogin()
	{
		InitializeComponent();
        doadorController = new DoadorController();
        txtCPF.TextChanged += FormatCPF;
    }

    private void FormatCPF(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var text = entry.Text;

        if (string.IsNullOrEmpty(text))
            return;

        text = new string(text.Where(char.IsDigit).ToArray());

        if (text.Length > 3 && text.Length <= 6)
        {
            text = $"{text.Substring(0, 3)}.{text.Substring(3)}";
        }
        else if (text.Length > 6 && text.Length <= 9)
        {
            text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6)}";
        }
        else if (text.Length > 9)
        {
            text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6, 3)}-{text.Substring(9)}";
        }

        if (entry.Text != text)
        {
            entry.Text = text;
            entry.CursorPosition = text.Length;
        }
    }
    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        string cpf = string.IsNullOrEmpty(txtCPF.Text) ? string.Empty : new string(txtCPF.Text.Where(char.IsDigit).ToArray());
        string senha = txtSenha.Text?.Trim();

        if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert(
            "Atenção",
            "Informe todos os dados corretamente.",
            "OK");
            return;
        }

        if (doadorController.ValidarLogin(cpf, senha))
        {
            Preferences.Set("UsuarioLogado", cpf);

            await Navigation.PushAsync(new pgPrincipal());

        }
        else
        {
            await DisplayAlert("Atenção!", "CPF ou senha inválidos.", "OK");
        }
    }

    private void MostrarSenha()
    {
        txtSenha.IsPassword = !cbxMostrarSenha.IsChecked;
    }

    private void cbxMostrarSenha_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        MostrarSenha();
    }

    private void tapMostrarSenha_Tapped(object sender, TappedEventArgs e)
    {
        cbxMostrarSenha.IsChecked = !cbxMostrarSenha.IsChecked;
        MostrarSenha();
    }

    private async void tapCadastrar_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new pgCadDoador());
    }
}