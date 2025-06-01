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

        // Obt�m CPF logado (sem formata��o)
        _cpfLogado = Preferences.Get("UsuarioLogado", string.Empty);

        // Carrega dados do doador
        CarregarDadosDoador();
    }

    private void CarregarDadosDoador()
    {
        var doador = _doadorController.GetByCpf(_cpfLogado);
        if (doador == null)
        {
            DisplayAlert("Erro", "Doador n�o encontrado", "OK");
            return;
        }

        entTipoSanguineo.Text = doador.TipoSangue ?? "N�o informado";
    }

    private async void btnDoar_Clicked(object sender, EventArgs e)
    {
        // Valida��o b�sica dos campos
        if (!ValidarCampos())
            return;

        var doador = _doadorController.GetByCpf(_cpfLogado);
        if (doador == null)
        {
            await DisplayAlert("Erro", "Doador n�o encontrado", "OK");
            return;
        }

        var dataSelecionada = dpDataDoacao.Date;

        // Valida��o da data
        if (!ValidarDataDoacao(dataSelecionada))
            return;

        // Verifica��o do intervalo entre doa��es
        if (!ValidarIntervaloDoacoes(doador.Id, dataSelecionada))
            return;

        // Registra a nova doa��o
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
            DisplayAlert("Erro", "A data da doa��o n�o pode ser no futuro", "OK");
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
                var mensagem = $"Intervalo m�nimo entre doa��es � de 6 meses.\n\n" +
                             $"�ltima doa��o: {ultimaDoacao.DataDoacao:dd/MM/yyyy}\n" +
                             $"Pr�xima doa��o permitida: {dataProximaDoacaoPermitida:dd/MM/yyyy}";

                DisplayAlert("Aten��o", mensagem, "OK");
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