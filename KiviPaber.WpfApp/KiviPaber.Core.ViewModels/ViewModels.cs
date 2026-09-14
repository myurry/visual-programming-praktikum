using KiviPaber.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace KiviPaber.Core.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class BattleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GameRound> Log { get; } = new ObservableCollection<GameRound>();

        private int _roundNumber;

        public void PlayRound(Move playerMove)
        {
            var computerMove = GameLogic.GetComputerMove();
            var result = GameLogic.GetResult(playerMove, computerMove);

            _roundNumber++;
            var round = new GameRound
            {
                Number = _roundNumber,
                PlayerMove = playerMove,
                ComputerMove = computerMove,
                Result = result
            };

            // Insert at 0 to show newest first (optional)
            Log.Insert(0, round);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void ClearLog()
        {
            Log.Clear();
            _roundNumber = 0;
        }

        // Or expose an ICommand (RelayCommand) bound to the button:
        public ICommand ClearLogCommand { get; }

        public BattleViewModel()
        {
            ClearLogCommand = new RelayCommand(_ => Log?.Clear());
        }
    }
}