using AppVitaSangue.Controllers;

namespace AppVitaSangue.Views;

public partial class pgLogin : ContentPage
{
    private DoadorController doadorController;

    public pgLogin()
	{
		InitializeComponent();
        doadorController = new DoadorController();
    }

    private void OnCPFCompleted(object sender, EventArgs e)
    {
        FormatCPF();
    }

    private void OnCPFUnfocused(object sender, FocusEventArgs e)
    {
        FormatCPF();
    }

    private void FormatCPF()
    {
        var text = txtCPF.Text ?? "";
        string digitsOnly = new string(text.Where(char.IsDigit).ToArray());

        // Garante que n�o ultrapasse 11 d�gitos
        if (digitsOnly.Length > 11)
        {
            digitsOnly = digitsOnly.Substring(0, 11);
        }

        // Aplica formata��o apenas se tiver 11 d�gitos
        if (digitsOnly.Length == 11)
        {
            txtCPF.Text = $"{digitsOnly.Substring(0, 3)}.{digitsOnly.Substring(3, 3)}.{digitsOnly.Substring(6, 3)}-{digitsOnly.Substring(9)}";
        }
        else
        {
            // Mant�m apenas os d�gitos (sem formata��o incompleta)
            txtCPF.Text = digitsOnly;
        }
    }
    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
        string cpf = string.IsNullOrEmpty(txtCPF.Text) ? string.Empty : new string(txtCPF.Text.Where(char.IsDigit).ToArray());
        string senha = txtSenha.Text?.Trim();

        if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert(
            "Aten��o",
            "Informe todos os dados corretamente.",
            "OK");
            return;
        }

        if (doadorController.ValidarLogin(cpf, senha))
        {
            Preferences.Set("UsuarioLogado", cpf);

            await Shell.Current.GoToAsync("//pgPrincipal");

        }
        else
        {
            await DisplayAlert("Aten��o!", "CPF ou senha inv�lidos.", "OK");
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