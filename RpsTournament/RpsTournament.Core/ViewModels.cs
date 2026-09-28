using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using static RpsTournament.Core.GameStates; // uses FormattedRoundResult and InvalidMoveException

namespace RpsTournament.Core.ViewModels
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
        private int _roundNumber;
        private int _seriesRoundCount;
        private int _playerScore;
        private int _computerScore;
        private string _seriesWinner = string.Empty;
        private readonly Random _rng = new Random();

        // UI bindings
        public int PlayerScore
        {
            get => _playerScore;
            private set => SetProperty(ref _playerScore, value);
        }

        public int ComputerScore
        {
            get => _computerScore;
            private set => SetProperty(ref _computerScore, value);
        }

        public string SeriesWinner
        {
            get => _seriesWinner;
            private set => SetProperty(ref _seriesWinner, value);
        }

        // Log as strings for easy binding
        public ObservableCollection<string> Log { get; } = new ObservableCollection<string>();

        // Commands
        public ICommand PlayCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand NewRoundCommand { get; }

        public BattleViewModel()
        {
            PlayCommand = new RelayCommand(param => {
                if (param is Move m) PlayRound(m);
                else if (param is string s && Enum.TryParse<Move>(s, true, out var parsed)) PlayRound(parsed);
            });

            ClearCommand = new RelayCommand(_ => ClearLog());
            NewRoundCommand = new RelayCommand(_ => ResetSeries());
        }

        private void PlayRound(Move playerMove)
        {
            var computerMove = GameLogic.GetComputerMove();
            var result = GameLogic.GetResult(playerMove, computerMove);

            _roundNumber++;
            _seriesRoundCount++;

            // update scores
            int winner = CompareMoves(playerMove, computerMove); // 1 player, -1 computer, 0 draw
            if (winner == 1) _playerScore++;
            else if (winner == -1) _computerScore++;

            // update exposed properties (will raise notifications)
            PlayerScore = _playerScore;
            ComputerScore = _computerScore;

            // create formatted log line using resource; pass 4 args so {3} resolves in the resource
            try
            {
                string line = string.Format(FormattedRoundResult,
                    playerMove.ToString(),
                    computerMove.ToString(),
                    result.ToString(),
                    result.ToString());
                Log.Insert(0, $"#{_roundNumber}: {line}");
            }
            catch (FormatException)
            {
                // fallback if resource has unexpected placeholders
                Log.Insert(0, $"#{_roundNumber}: {playerMove} vs {computerMove} -> {result}");
            }

            SeriesWinner = string.Empty;

            if (_seriesRoundCount >= 5)
            {
                if (_playerScore > _computerScore) SeriesWinner = "Player wins the series";
                else if (_computerScore > _playerScore) SeriesWinner = "Computer wins the series";
                else
                {
                    if (_rng.Next(2) == 0) SeriesWinner = "Player wins the series (tie-break)";
                    else SeriesWinner = "Computer wins the series (tie-break)";
                }

                // add series winner to log
                Log.Insert(0, $"-- {SeriesWinner} --");

                // reset counters for next series
                _seriesRoundCount = 0;
                _playerScore = 0;
                _computerScore = 0;
                PlayerScore = 0;
                ComputerScore = 0;
            }
        }

        private static int CompareMoves(Move player, Move computer)
        {
            if (player == computer) return 0;
            if ((player == Move.Rock && computer == Move.Scissors) ||
                (player == Move.Paper && computer == Move.Rock) ||
                (player == Move.Scissors && computer == Move.Paper)) return 1;
            return -1;
        }

        public void ClearLog()
        {
            Log.Clear();
            _roundNumber = 0;
            _seriesRoundCount = 0;
            _playerScore = 0;
            _computerScore = 0;
            PlayerScore = 0;
            ComputerScore = 0;
            SeriesWinner = string.Empty;
        }

        public void ResetSeries()
        {
            ClearLog();
        }

        #region INotify / helpers
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}