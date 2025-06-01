using AppVitaSangue.Controllers;
using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgCadDoacao : ContentPage
{
    private readonly DoacaoController _doacaoController;
    private readonly DoadorController _doadorController;
    private readonly string _cpfLogado;

    public pgCadDoacao()
    {
        InitializeComponent();
        pckrHospital.SelectedIndex = 0;

        _doacaoController = new DoacaoController();
        _doadorController = new DoadorController();

        // Obtém CPF logado (sem formatação)
        _cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);

        // Carrega dados do doador
        CarregarDadosDoador();
    }

    private void CarregarDadosDoador()
    {
        var doador = _doadorController.GetByCpf(_cpfLogado);
        if (doador == null)
        {
            DisplayAlert("Erro", "Doador não encontrado", "OK");
            return;
        }

        entTipoSanguineo.Text = doador.TipoSangue ?? "Não informado";
    }

    private async void btnDoar_Clicked(object sender, EventArgs e)
    {
        // Validação básica dos campos
        if (!ValidarCampos())
            return;

        var doador = _doadorController.GetByCpf(_cpfLogado);
        if (doador == null)
        {
            await DisplayAlert("Erro", "Doador não encontrado", "OK");
            return;
        }

        var dataSelecionada = dpDataDoacao.Date;

        // Validação da data
        if (!ValidarDataDoacao(dataSelecionada))
            return;

        // Verificação do intervalo entre doações
        if (!ValidarIntervaloDoacoes(doador.Id, dataSelecionada))
            return;

        // Registra a nova doação
        await RegistrarNovaDoacao(doador.Id, dataSelecionada);
    }

    private bool ValidarCampos()
    {
        if (dpDataDoacao.Date == null || pckrHospital.SelectedIndex == -1)
        {
            DisplayAlert("Erro", "Preencha todos os campos", "OK");
            return false;
        }
        return true;
    }

    private bool ValidarDataDoacao(DateTime dataSelecionada)
    {
        if (dataSelecionada > DateTime.Today)
        {
            DisplayAlert("Erro", "A data da doação não pode ser no futuro", "OK");
            return false;
        }
        return true;
    }

    private bool ValidarIntervaloDoacoes(int doadorId, DateTime dataSelecionada)
    {
        var ultimaDoacao = _doacaoController.ObterUltimaDoacao(doadorId);

        if (ultimaDoacao != null)
        {
            var dataProximaDoacaoPermitida = ultimaDoacao.DataDoacao.AddMonths(6);

            if (dataSelecionada < dataProximaDoacaoPermitida)
            {
                var mensagem = $"Intervalo mínimo entre doações é de 6 meses.\n\n" +
                             $"Última doação: {ultimaDoacao.DataDoacao:dd/MM/yyyy}\n" +
                             $"Próxima doação permitida: {dataProximaDoacaoPermitida:dd/MM/yyyy}";

                DisplayAlert("Atenção", mensagem, "OK");
                return false;
            }
        }
        return true;
    }

    private async Task RegistrarNovaDoacao(int doadorId, DateTime dataDoacao)
    {
        var novaDoacao = new Doacao
        {
            DoadorId = doadorId,
            Hospital = pckrHospital.SelectedItem.ToString(),
            DataDoacao = dataDoacao
        };

        if (_doacaoController.RegistrarDoacao(novaDoacao))
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