namespace AppVitaSangue.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnSerDoador_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pgCadDoador());
        }

        private async void btnJaSouDoador_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pgLogin());
        }
    }

}
