using CadastroEventosApp.ViewModels;

namespace CadastroEventosApp.Views;

// A Página implementa IQueryAttributable
public partial class ResumoPage : ContentPage, IQueryAttributable
{
    public ResumoPage()
    {
        InitializeComponent();
        // BindingContext é definido no XAML
    }

    // Método da interface IQueryAttributable
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Pega a instância do ViewModel definida no XAML
        if (BindingContext is ResumoViewModel vm)
        {
            // Repassa os dados da query para o ViewModel
            // O ViewModel saberá o que fazer (ver ResumoViewModel.cs)
            vm.ApplyQueryAttributes(query);
        }
    }

    // Event handler para o botão de voltar
    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // ".." é o comando do Shell para voltar à página anterior
        await Shell.Current.GoToAsync("..");
    }
}