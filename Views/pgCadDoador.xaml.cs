using AppVitaSangue.Models;
using AppVitaSangue.Controllers;

namespace AppVitaSangue.Views;

public partial class pgCadDoador : ContentPage
{
    private DoadorController doadorController;

    public pgCadDoador()
	{
		InitializeComponent();
        doadorController = new DoadorController();
    }

    private void btnAdicionar_Clicked(object sender, EventArgs e)
    {
        string nome = txtNome.Text;
        string cpf = txtCPF.Text;
        string dataNasc = dpDataNasc.Date.ToString("dd/MM/yyyy");
        string tipoSangue = pckrTipoSangue.SelectedItem?.ToString();
        string email = txtEmail.Text;
        string telefone = txtTelefone.Text;
        string endereco = txtEndereco.Text;
        string cidade = txtCidade.Text;
        string senha = txtSenha.Text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf)
            || string.IsNullOrEmpty(dataNasc) || string.IsNullOrEmpty(tipoSangue)
            || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(endereco) || string.IsNullOrEmpty(cidade)
            || string.IsNullOrEmpty(senha))
        {
            DisplayAlert(
                "Atenção",
                "Informe todos os dados corretamente.",
                "OK");
            return;
        }

        if (pckrTipoSangue.SelectedIndex != -1)
        {
            tipoSangue = pckrTipoSangue.Items[pckrTipoSangue.SelectedIndex];
        }

        Doador doador = new Doador();
        doador.Nome = nome;
        doador.CPF = cpf;
        doador.DataNasc = dataNasc;
        doador.TipoSangue = tipoSangue;
        doador.Email = email;
        doador.Telefone = telefone;
        doador.Endereco = endereco;
        doador.Cidade = cidade;
        doador.Senha = senha;

        if (doadorController.Insert(doador))
        {
            DisplayAlert(
                "Informação",
                "Registro salvo com sucesso!",
                "OK");

            txtNome.Text = "";
            txtCPF.Text = "";
            dpDataNasc.Date = DateTime.Today;
            pckrTipoSangue.SelectedIndex = -1;
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtSenha.Text = "";
        }
        else
            DisplayAlert(
                "Erro",
                "Falha ao salvar o registro no banco de dados.",
                "OK");
    }
}