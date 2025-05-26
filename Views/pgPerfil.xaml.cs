using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgPerfil : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;

    public pgPerfil(Doador doador)
	{
		InitializeComponent();
        doadorVisualizar = doador;
        doadorController = new DoadorController();
        ExibirDados();
	}

    private void ExibirDados()
    {
        var cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);
        doadorVisualizar = doadorController.GetByCpf(cpfLogado);

        if (doadorVisualizar != null)
        {
            lblNome.Text = doadorVisualizar.Nome;
            lblTipoSanguineo.Text = doadorVisualizar.TipoSangue;
            lblCidade.Text = doadorVisualizar.Cidade;

            if (!string.IsNullOrEmpty(doadorVisualizar.DirImagem))
            {
                imgPerfil.Source = doadorVisualizar.DirImagem;
            }
        }
    }

    private void btnEditarPerfil_Clicked(object sender, EventArgs e)
    {
        Doador doador = new Doador();

        Application.Current.MainPage.Navigation.PushAsync(new pgEditarPerfil(doador));
    }
}