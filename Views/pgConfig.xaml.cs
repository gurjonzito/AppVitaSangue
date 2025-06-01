using System.Threading.Tasks;
using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgConfig : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;

    public pgConfig()
	{
		InitializeComponent();
        doadorController = new DoadorController();
        ExibirDados();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ExibirDados();
    }

    private void ExibirDados()
    {
        var cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);
        doadorVisualizar = doadorController.GetByCpf(cpfLogado);

        if (doadorVisualizar != null)
        {
            lblNome.Text = doadorVisualizar.Nome;
            lblEmail.Text = doadorVisualizar.Email;

            if (!string.IsNullOrEmpty(doadorVisualizar.DirImagem))
            {
                imgPerfil.Source = doadorVisualizar.DirImagem;
            }
            else
            {
                imgPerfil.Source = null;
            }
        }
    }

    private void EditarPerfil_Tapped(object sender, TappedEventArgs e)
    {

    }

    private async void Acessibilidade_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new pgAcessibilidade());
    }

    private void Sobre_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void Privacidade_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void EncerrarConta_Tapped(object sender, TappedEventArgs e)
    {

    }

    private async void Sair_Tapped(object sender, TappedEventArgs e)
    {
        bool confirmacao = await DisplayAlert(
            "Confirmação",
            "Deseja realmente sair da aplicação?",
            "Sim", "Não");

        if (confirmacao)
        {
            Preferences.Remove("UsuarioLogado");
            await Shell.Current.GoToAsync("//pgLogin", true);
        }
    }
}