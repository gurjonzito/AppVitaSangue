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

    private void OnTelefoneCompleted(object sender, EventArgs e)
    {
        FormatTelefone();
    }

    private void OnTelefoneUnfocused(object sender, FocusEventArgs e)
    {
        FormatTelefone();
    }

    // M�todo principal de formata��o
    private void FormatTelefone()
    {
        var text = txtTelefone.Text ?? "";

        // Remove tudo que n�o � d�gito
        string digitsOnly = new string(text.Where(char.IsDigit).ToArray());

        // Limita a 11 caracteres (DDD + 9 d�gitos)
        if (digitsOnly.Length > 11)
        {
            digitsOnly = digitsOnly.Substring(0, 11);
        }

        // Aplica a formata��o de acordo com o tamanho
        string formatted = digitsOnly;

        if (digitsOnly.Length == 11) // Celular com 9 d�gitos
        {
            formatted = $"({digitsOnly.Substring(0, 2)}) {digitsOnly.Substring(2, 5)}-{digitsOnly.Substring(7)}";
        }
        else if (digitsOnly.Length == 10) // Telefone fixo com 8 d�gitos
        {
            formatted = $"({digitsOnly.Substring(0, 2)}) {digitsOnly.Substring(2, 4)}-{digitsOnly.Substring(6)}";
        }
        else if (digitsOnly.Length > 2)
        {
            formatted = $"({digitsOnly.Substring(0, 2)}) {digitsOnly.Substring(2)}";
        }
        else if (digitsOnly.Length > 0)
        {
            formatted = $"({digitsOnly}";
        }

        // Atualiza o texto formatado
        txtTelefone.Text = formatted;
    }

    private async void btnAdicionar_Clicked(object sender, EventArgs e)
    {
        string nome = txtNome.Text.Trim();
        string cpf = string.IsNullOrEmpty(txtCPF.Text) ? "" : new string(txtCPF.Text.Where(char.IsDigit).ToArray());
        string dataNasc = dpDataNasc.Date.ToString("dd/MM/yyyy");
        string tipoSangue = pckrTipoSangue.SelectedItem?.ToString()?.Trim();
        string email = txtEmail.Text.Trim();
        string telefone = string.IsNullOrEmpty(txtTelefone.Text) ? "" : new string(txtTelefone.Text.Where(char.IsDigit).ToArray());
        string endereco = txtEndereco.Text.Trim();
        string cidade = txtCidade.Text.Trim();
        string senha = txtSenha.Text.Trim();

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cpf)
            || string.IsNullOrEmpty(dataNasc) || string.IsNullOrEmpty(tipoSangue)
            || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(endereco) || string.IsNullOrEmpty(cidade)
            || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert(
                "Aten��o",
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
                "Informa��o",
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