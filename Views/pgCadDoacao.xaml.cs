using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgCadDoacao : ContentPage
{
    private DoacaoController doacaoController;
    private DoadorController doadorController;
    private string cpfLogado;

    public pgCadDoacao()
    {
        InitializeComponent();
        pckrHospital.SelectedIndex = 0;

        doacaoController = new DoacaoController();
        doadorController = new DoadorController();

        cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);
        var doador = doadorController.GetByCpf(cpfLogado);
        entTipoSanguineo.Text = doador?.TipoSangue ?? "N�o informado";
    }

    private async void btnDoar_Clicked(object sender, EventArgs e)
    {
        if (dpDataDoacao.Date == null || pckrHospital.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Preencha todos os campos", "OK");
            return;
        }

        var doador = doadorController.GetByCpf(cpfLogado);
        if (doador == null)
        {
            await DisplayAlert("Erro", "Doador n�o encontrado", "OK");
            return;
        }
        var dataSelecionada = dpDataDoacao.Date;
        var dataAtual = DateTime.Today;

        if (dataSelecionada > dataAtual)
        {
            await DisplayAlert("Erro", "A data da doa��o n�o pode ser no futuro", "OK");
            return;
        }
        var ultimaDoacao = doacaoController.ObterUltimaDoacao(doador.Id);
        if (ultimaDoacao != null)
        {
            var dataProximaDoacaoPermitida = ultimaDoacao.DataDoacao.AddMonths(6);

            if (dataSelecionada < dataProximaDoacaoPermitida)
            {
                var mensagem = $"Intervalo m�nimo entre doa��es � de 6 meses.\n\n" +
                             $"�ltima doa��o: {ultimaDoacao.DataDoacao:dd/MM/yyyy}\n" +
                             $"Pr�xima doa��o permitida: {dataProximaDoacaoPermitida:dd/MM/yyyy}";

                await DisplayAlert("Aten��o", mensagem, "OK");
                return;
            }
        }

        var novaDoacao = new Doacao
        {
            DoadorId = doador.Id,
            Hospital = pckrHospital.SelectedItem.ToString(),
            DataDoacao = dpDataDoacao.Date
        };

        if (doacaoController.RegistrarDoacao(novaDoacao))
        {
            await DisplayAlert("Sucesso", "Doa��o registrada com sucesso!", "OK");
            dpDataDoacao.Date = DateTime.Today;

            await Shell.Current.GoToAsync("//pgPrincipal");
        }
        else
        {
            await DisplayAlert("Erro", "Falha ao registrar doa��o", "OK");
        }
    }
}