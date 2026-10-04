using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Vehicle> Vehicles { get; } = new ObservableCollection<Vehicle>();

        public MainWindow()
        {
            InitializeComponent();
            LstVehicles.ItemsSource = Vehicles;
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            CreateVehicle dialog = new CreateVehicle { Owner = this };
            if (dialog.ShowDialog() == true && dialog.CreatedVehicle != null)
            {
                Vehicles.Add(dialog.CreatedVehicle);
            }
        }

        // Action: Standalone Drive command
        private void DriveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is IDrivable drivable)
            {
                if (TryGetSteps(btn, out int steps))
                {
                    drivable.Drive(steps);
                    LstVehicles.Items.Refresh();
                }
            }
        }

        // Action: Standalone Swim command
        private void SwimItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is ISwimmable swimmable)
            {
                if (TryGetSteps(btn, out int steps))
                {
                    swimmable.Swim(steps);
                    LstVehicles.Items.Refresh();
                }
            }
        }

        // Action: View Vehicle Log Window via Log Button
        private void ViewLogItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Vehicle vehicle)
            {
                LogWindow logWin = new LogWindow(vehicle) { Owner = this };
                logWin.ShowDialog();
            }
        }

        // Helper to retrieve step input from row
        private bool TryGetSteps(Button button, out int steps)
        {
            steps = 0;
            DependencyObject parent = VisualTreeHelper.GetParent(button);
            while (parent != null && !(parent is Grid))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is Grid rowGrid && rowGrid.FindName("TxtItemSteps") is TextBox stepsBox)
            {
                if (int.TryParse(stepsBox.Text, out steps))
                {
                    return true;
                }
            }

            MessageBox.Show("Please enter a valid integer number of steps.", "Invalid Steps", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
    }
}