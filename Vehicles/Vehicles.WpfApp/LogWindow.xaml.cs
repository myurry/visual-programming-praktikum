using System.Windows;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class LogWindow : Window
    {
        public LogWindow(Vehicle vehicle)
        {
            InitializeComponent();
            TxtHeader.Text = $"{vehicle.Name} — Action Log";
            LstLogs.ItemsSource = vehicle.Log;
        }
    }
}