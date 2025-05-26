using AppVitaSangue.Models;

namespace AppVitaSangue.Views;

public partial class pgPrincipal : ContentPage
{
	public pgPrincipal()
	{
		InitializeComponent();
	}

    private void btnPerfil_Clicked(object sender, EventArgs e)
    {
        Doador doador = new Doador();

        Application.Current.MainPage.Navigation.PushAsync(new pgPerfil(doador));
    }
}