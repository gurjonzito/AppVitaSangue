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

    private void btnEntrar_Clicked(object sender, EventArgs e)
    {
        string cpf = txtCPF.Text;
        string senha = txtSenha.Text;

        if (doadorController.ValidarLogin(cpf, senha))
        {
            Preferences.Set("UsuarioLogado", cpf);

            Application.Current.MainPage.Navigation.PushAsync(new pgPrincipal());
        }
        else
        {
            DisplayAlert("Atenção!", "CPF ou senha inválidos.", "OK");
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

    private void tapCadastrar_Tapped(object sender, TappedEventArgs e)
    {
        Application.Current.MainPage.Navigation.PushAsync(new pgCadDoador());
    }
}