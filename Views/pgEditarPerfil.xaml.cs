using AppVitaSangue.Services;
using AppVitaSangue.Models;
using AppVitaSangue.Controllers;

namespace AppVitaSangue.Views;

public partial class pgEditarPerfil : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;
    private string sImagemSelecionada;

    public pgEditarPerfil(Doador doador)
	{
		InitializeComponent();
        doadorVisualizar = doador;
        doadorController = new DoadorController();
        ExibirDados();
        AtualizarVisibilidadeBotoes();
    }

    private void ExibirDados()
    {
        var cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);
        doadorVisualizar = doadorController.GetByCpf(cpfLogado);

        if (doadorVisualizar != null)
        {
            txtNome.Text = doadorVisualizar.Nome;
            txtEmail.Text = doadorVisualizar.Email;
            txtTelefone.Text = doadorVisualizar.Telefone;
            txtEndereco.Text = doadorVisualizar.Endereco;
            txtCidade.Text = doadorVisualizar.Cidade;

            if (!string.IsNullOrEmpty(doadorVisualizar.DirImagem))
            {
                imgPerfil.Source = ImageSource.FromFile(doadorVisualizar.DirImagem);
                sImagemSelecionada = doadorVisualizar.DirImagem;
            }
        }
    }

    private void AtualizarVisibilidadeBotoes()
    {
        btnRemover.IsVisible = imgPerfil.Source != null;
    }

    private async void btnSelecionar_Clicked(object sender, EventArgs e)
    {
        sImagemSelecionada = await ImageService.SelecionarImagem();

        imgPerfil.Source = sImagemSelecionada;

        AtualizarVisibilidadeBotoes();
    }

    private void btnRemover_Clicked(object sender, EventArgs e)
    {
        imgPerfil.Source = "";
        sImagemSelecionada = "";
        AtualizarVisibilidadeBotoes();
    }

    private async void btnSalvar_Clicked(object sender, EventArgs e)
    {
        if (doadorVisualizar != null)
        {
            doadorVisualizar.Nome = txtNome.Text;
            doadorVisualizar.Email = txtEmail.Text;
            doadorVisualizar.Telefone = txtTelefone.Text;
            doadorVisualizar.Endereco = txtEndereco.Text;
            doadorVisualizar.Cidade = txtCidade.Text;

            if (!string.IsNullOrEmpty(sImagemSelecionada))
            {
                doadorVisualizar.DirImagem = ImageService.CopiarImagemDirApp(sImagemSelecionada);
            }
            else if (imgPerfil.Source == null)
            {
                doadorVisualizar.DirImagem = string.Empty;
            }

            bool resultado = doadorController.Update(doadorVisualizar);

            if (resultado)
            {
                await DisplayAlert("Sucesso", "Perfil atualizado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao atualizar o perfil", "OK");
            }
        }
    }
}