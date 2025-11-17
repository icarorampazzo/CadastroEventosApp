using CadastroEventosApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CadastroEventosApp.ViewModels
{
    public class CadastroViewModel : INotifyPropertyChanged
    {
        private Evento _evento;

        public Evento Evento
        {
            get => _evento;
            set => SetProperty(ref _evento, value);
        }

        public ICommand CadastrarCommand { get; }

        public CadastroViewModel()
        {
            Evento = new Evento();
            CadastrarCommand = new Command(async () => await OnCadastrar());
        }

        private async Task OnCadastrar()
        {
            if (string.IsNullOrWhiteSpace(Evento.Nome) || Evento.NumeroParticipantes <= 0)
            {
                // Adicione uma lógica de validação simples (exibindo um Alerta)
                await Shell.Current.DisplayAlert("Erro", "Preencha pelo menos o Nome e o Número de Participantes.", "OK");
                return;
            }

            // Requisito 3: Passar os dados para a próxima página
            // Usamos a navegação do Shell para passar o objeto Evento como parâmetro
            var navigationParameter = new Dictionary<string, object>
            {
                { "EventoCadastrado", Evento }
            };

            // Navega para a "ResumoPage" (que vamos registrar no AppShell)
            await Shell.Current.GoToAsync("ResumoPage", navigationParameter);

            
            // O Binding com 'Evento' na UI será atualizado
            Evento = new Evento();
        }

        // --- Implementação do INotifyPropertyChanged ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}