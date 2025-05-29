using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgPerfil : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;

    public pgPerfil() : this(null)
    {
    }
    public pgPerfil(Doador doador)
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
            lblTipoSanguineo.Text = doadorVisualizar.TipoSangue;
            lblCidade.Text = doadorVisualizar.Cidade;
            lblDescricaoPerfil.Text = doadorVisualizar.DescricaoPerfil;

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

    private void btnEditarPerfil_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new pgEditarPerfil());
    }
}