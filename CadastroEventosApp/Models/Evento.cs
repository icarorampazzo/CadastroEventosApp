using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CadastroEventosApp.Models
{
    // Implementa INotifyPropertyChanged para atualizar a UI
    // quando as propriedades (especialmente as calculadas) mudarem.
    public class Evento : INotifyPropertyChanged
    {
        // Campos privados
        private string _nome;
        private DateTime _dataInicio = DateTime.Today;
        private DateTime _dataTermino = DateTime.Today.AddDays(1); // Default de 1 dia
        private int _numeroParticipantes;
        private string _local;
        private decimal _custoPorParticipante;

        // Propriedades Públicas (para o Binding)
        public string Nome
        {
            get => _nome;
            set => SetProperty(ref _nome, value);
        }

        public DateTime DataInicio
        {
            get => _dataInicio;
            set
            {
                if (SetProperty(ref _dataInicio, value))
                {
                    // Se a data de início mudar, notifique a UI
                    // que as propriedades calculadas também mudaram.
                    OnPropertyChanged(nameof(Duracao));
                    OnPropertyChanged(nameof(DuracaoEmDias));
                }
            }
        }

        public DateTime DataTermino
        {
            get => _dataTermino;
            set
            {
                if (SetProperty(ref _dataTermino, value))
                {
                    // O mesmo para a data de término.
                    OnPropertyChanged(nameof(Duracao));
                    OnPropertyChanged(nameof(DuracaoEmDias));
                }
            }
        }

        public int NumeroParticipantes
        {
            get => _numeroParticipantes;
            set
            {
                if (SetProperty(ref _numeroParticipantes, value))
                {
                    // Se o número de participantes mudar, recalcule o custo total.
                    OnPropertyChanged(nameof(CustoTotal));
                }
            }
        }

        public string Local
        {
            get => _local;
            set => SetProperty(ref _local, value);
        }

        public decimal CustoPorParticipante
        {
            get => _custoPorParticipante;
            set
            {
                if (SetProperty(ref _custoPorParticipante, value))
                {
                    // Se o custo por participante mudar, recalcule o custo total.
                    OnPropertyChanged(nameof(CustoTotal));
                }
            }
        }

        // --- Requisito 1 & 2: Propriedades Calculadas ---

        // Requisito 2: Usa TimeSpan para a diferença
        public TimeSpan Duracao => DataTermino - DataInicio;

        // Requisito 1: Lógica para calcular a duração em dias
        public int DuracaoEmDias
        {
            get
            {
                // Usa Ceiling para arredondar para cima. (Ex: 1.5 dias = 2 dias de evento)
                // Garante que a duração seja pelo menos 0.
                var dias = (int)Math.Ceiling(Duracao.TotalDays);
                return dias < 0 ? 0 : dias;
            }
        }


        // Requisito 1: Lógica para calcular o custo total
        public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;


        // --- Implementação do INotifyPropertyChanged ---
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
