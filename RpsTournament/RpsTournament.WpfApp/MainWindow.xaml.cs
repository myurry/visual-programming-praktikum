using RpsTournament.Core.ViewModels;
using System.Windows;

namespace RpsTournament.WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BattleViewModel();
        }
    }
}