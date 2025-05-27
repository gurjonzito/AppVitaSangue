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
        txtCPF.TextChanged += FormatCPF;

    }

    private void FormatCPF(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        var text = entry.Text;

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

    private async void btnAdicionar_Clicked(object sender, EventArgs e)
    {
        string nome = txtNome.Text;
        string cpf = new string(txtCPF.Text.Where(char.IsDigit).ToArray());
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
            await DisplayAlert(
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
            await DisplayAlert(
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

            await Navigation.PushAsync(new pgLogin());
        }
        else
            await DisplayAlert(
                "Erro",
                "Falha ao salvar o registro no banco de dados.",
                "OK");
    }
}