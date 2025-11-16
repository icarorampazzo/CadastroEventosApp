using CadastroEventosApp.Views;

namespace CadastroEventosApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Requisito 3: Registrar a rota para a página de resumo
        Routing.RegisterRoute(nameof(ResumoPage), typeof(ResumoPage));
    }
}