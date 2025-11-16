using CadastroEventosApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CadastroEventosApp.ViewModels
{
    // IQueryAttributable permite que esta VM receba dados da navegação
    public class ResumoViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private Evento _evento;
        public Evento Evento
        {
            get => _evento;
            set => SetProperty(ref _evento, value);
        }

        // Este método é chamado automaticamente pelo Shell quando navegamos para cá
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Verifica se o dicionário contém a chave que enviamos
            if (query.TryGetValue("EventoCadastrado", out object eventoObj))
            {
                // Atribui o objeto recebido à propriedade Evento
                // A UI (ResumoPage) fará o binding a esta propriedade
                Evento = eventoObj as Evento;
            }
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
