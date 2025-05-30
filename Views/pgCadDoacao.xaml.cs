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
        entTipoSanguineo.Text = doador?.TipoSangue ?? "Não informado";
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
            await DisplayAlert("Erro", "Doador não encontrado", "OK");
            return;
        }
        var dataSelecionada = dpDataDoacao.Date;
        var dataAtual = DateTime.Today;

        if (dataSelecionada > dataAtual)
        {
            await DisplayAlert("Erro", "A data da doação não pode ser no futuro", "OK");
            return;
        }
        var ultimaDoacao = doacaoController.ObterUltimaDoacao(doador.Id);
        if (ultimaDoacao != null)
        {
            var dataProximaDoacaoPermitida = ultimaDoacao.DataDoacao.AddMonths(6);

            if (dataSelecionada < dataProximaDoacaoPermitida)
            {
                var mensagem = $"Intervalo mínimo entre doações é de 6 meses.\n\n" +
                             $"Última doação: {ultimaDoacao.DataDoacao:dd/MM/yyyy}\n" +
                             $"Próxima doação permitida: {dataProximaDoacaoPermitida:dd/MM/yyyy}";

                await DisplayAlert("Atenção", mensagem, "OK");
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
            await DisplayAlert("Sucesso", "Doação registrada com sucesso!", "OK");
            dpDataDoacao.Date = DateTime.Today;

            await Shell.Current.GoToAsync("//pgPrincipal");
        }
        else
        {
            await DisplayAlert("Erro", "Falha ao registrar doação", "OK");
        }
    }
}