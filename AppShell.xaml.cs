using AppVitaSangue.Views;

namespace AppVitaSangue
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("pgPrincipal", typeof(pgPrincipal));
        }
    }
}
