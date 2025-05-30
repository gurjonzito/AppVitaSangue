using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgPerfil : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;
    private DoacaoController doacaoController; 

    public pgPerfil() : this(null)
    {
    }

    public pgPerfil(Doador doador)
    {
        InitializeComponent();
        doadorController = new DoadorController();
        doacaoController = new DoacaoController(); 
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
            lblDescricaoPerfil.Text = string.IsNullOrEmpty(doadorVisualizar.DescricaoPerfil)
                ? "Descrições sobre o doador."
                : doadorVisualizar.DescricaoPerfil;

            imgPerfil.Source = !string.IsNullOrEmpty(doadorVisualizar.DirImagem)
                ? ImageSource.FromFile(doadorVisualizar.DirImagem)
                : null;

            VerificarStatusDoacao(doadorVisualizar.Id);

            lstDoacoes.ItemsSource = doacaoController.ObterDoacoesPorDoador(doadorVisualizar.Id);
        }
    }

    private void VerificarStatusDoacao(int doadorId)
    {
        var ultimaDoacao = doacaoController.ObterUltimaDoacao(doadorId);
        var hoje = DateTime.Today;

        if (ultimaDoacao == null || hoje >= ultimaDoacao.DataDoacao.AddMonths(6))
        {
            lblStatusDoacao.Text = "Apto a doar";
            lblStatusDoacao.TextColor = Color.FromArgb("#2F855A");
        }
        else
        {
            lblStatusDoacao.Text = "Não apto";
            lblStatusDoacao.TextColor = Color.FromArgb("#C53030");
        }
    }

    private void btnEditarPerfil_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new pgEditarPerfil());
    }
}