using AppVitaSangue.Services;
using AppVitaSangue.Models;
using AppVitaSangue.Controllers;

namespace AppVitaSangue.Views;

public partial class pgEditarPerfil : ContentPage
{
    Doador doadorVisualizar;
    private DoadorController doadorController;
    private string sImagemSelecionada;

    public pgEditarPerfil()
    {
        InitializeComponent();
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
            txtDescricaoPerfil.Text = doadorVisualizar.DescricaoPerfil;

            if (!string.IsNullOrEmpty(doadorVisualizar.DirImagem))
            {
                imgPerfil.Source = ImageSource.FromFile(doadorVisualizar.DirImagem);
                sImagemSelecionada = doadorVisualizar.DirImagem;
            }
        }
    }

    private void AtualizarVisibilidadeBotoes()
    {
        btnRemover.IsVisible = !string.IsNullOrEmpty(sImagemSelecionada);
    }

    private async void btnSelecionar_Clicked(object sender, EventArgs e)
    {
        sImagemSelecionada = await ImageService.SelecionarImagem();
        imgPerfil.Source = sImagemSelecionada;
        AtualizarVisibilidadeBotoes();
    }

    private void btnRemover_Clicked(object sender, EventArgs e)
    {
        imgPerfil.Source = null;
        sImagemSelecionada = null;
        AtualizarVisibilidadeBotoes();
    }

    private async void btnSalvar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text) ||
         string.IsNullOrWhiteSpace(txtEmail.Text) ||
         string.IsNullOrWhiteSpace(txtTelefone.Text) ||
         string.IsNullOrWhiteSpace(txtEndereco.Text) ||
         string.IsNullOrWhiteSpace(txtCidade.Text))
        {
            await DisplayAlert("Atenção", "Por favor, preencha todos os campos obrigatórios.", "OK");
            return;
        }

        if (doadorVisualizar != null)
        {
            doadorVisualizar.Nome = txtNome.Text.Trim();
            doadorVisualizar.Email = txtEmail.Text.Trim();
            doadorVisualizar.Telefone = txtTelefone.Text.Trim();
            doadorVisualizar.Endereco = txtEndereco.Text.Trim();
            doadorVisualizar.Cidade = txtCidade.Text.Trim();
            doadorVisualizar.DescricaoPerfil = txtDescricaoPerfil.Text?.Trim();

            if (!string.IsNullOrEmpty(sImagemSelecionada))
            {
                doadorVisualizar.DirImagem = sImagemSelecionada;
            }
            else if (imgPerfil.Source == null)
            {
                doadorVisualizar.DirImagem = string.Empty;
            }

            bool resultado = doadorController.Update(doadorVisualizar);

            if (resultado)
            {
                await DisplayAlert("Sucesso", "Perfil atualizado com sucesso!", "OK");
                await Shell.Current.GoToAsync("//PrincipalTabBar/pgPerfil");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao atualizar o perfil", "OK");
            }
        }
    }
}