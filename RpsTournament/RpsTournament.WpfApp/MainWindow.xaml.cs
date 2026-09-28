using RpsTournament.Core.ViewModels;
using System.Windows;

namespace RpsTournament.WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Show name dialog after window is loaded (Owner can be set safely)
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;

            var nameDialog = new NameDialog { Owner = this };
            bool? result = nameDialog.ShowDialog();

            string playerName = "Player";
            if (result == true && !string.IsNullOrWhiteSpace(nameDialog.PlayerName))
            {
                playerName = nameDialog.PlayerName;
            }

            DataContext = new BattleViewModel(playerName);
        }
    }
}