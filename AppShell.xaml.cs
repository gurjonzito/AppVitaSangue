using AppVitaSangue.Views;

namespace AppVitaSangue
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("pgLogin", typeof(pgLogin));
            Routing.RegisterRoute("pgPrincipal", typeof(pgPrincipal));
            Routing.RegisterRoute("pgCadDoacao", typeof(pgCadDoacao));
            Routing.RegisterRoute("pgPerfil", typeof(pgPerfil));
            Routing.RegisterRoute("pgConfig", typeof(pgConfig));


        }
    }
}
