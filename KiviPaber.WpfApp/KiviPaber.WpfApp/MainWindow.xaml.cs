using System.Windows;
using KiviPaber.Core;
using KiviPaber.Core.ViewModels;

namespace KiviPaber.WpfApp
{
    public partial class MainWindow : Window
    {
        private BattleViewModel VM => (BattleViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BattleViewModel();
        }

        private void RockButton_Click(object sender, RoutedEventArgs e)
        {
            VM.PlayRound(Move.Rock);
        }

        private void PaperButton_Click(object sender, RoutedEventArgs e)
        {
            VM.PlayRound(Move.Paper);
        }

        private void ScissorsButton_Click(object sender, RoutedEventArgs e)
        {
            VM.PlayRound(Move.Scissors);
        }

    }
}