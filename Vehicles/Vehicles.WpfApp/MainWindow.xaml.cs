using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Vehicle> Cars { get; } = new ObservableCollection<Vehicle>();
        public ObservableCollection<Vehicle> Boats { get; } = new ObservableCollection<Vehicle>();

        public MainWindow()
        {
            InitializeComponent();

            LstCars.ItemsSource = Cars;
            LstBoats.ItemsSource = Boats;
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            CreateVehicle dialog = new CreateVehicle { Owner = this };
            if (dialog.ShowDialog() == true && dialog.CreatedVehicle != null)
            {
                var vehicle = dialog.CreatedVehicle;

                
                if (vehicle is Boat && !(vehicle is Car))
                {
                    
                    foreach (var existingBoat in Boats)
                    {
                        existingBoat.LogMessage($"{vehicle.Name}({vehicle.VehicleType}) did a welcoming *beep-beep*");
                    }
                    Boats.Add(vehicle);
                }
                else
                {
                    
                    foreach (var existingCar in Cars)
                    {
                        existingCar.LogMessage($"{vehicle.Name}({vehicle.VehicleType}) did a welcoming *beep-beep*");
                    }
                    Cars.Add(vehicle);
                }
            }
        }

        // Action: Remove Vehicle from list
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Vehicle vehicle)
            {
                if (Cars.Contains(vehicle))
                {
                    Cars.Remove(vehicle);
                    
                    foreach (var remainingCar in Cars)
                    {
                        remainingCar.LogMessage($"{vehicle.Name}({vehicle.VehicleType}) flashed their lights in goodbye");
                    }
                }
                else if (Boats.Contains(vehicle))
                {
                    Boats.Remove(vehicle);
                    
                    foreach (var remainingBoat in Boats)
                    {
                        remainingBoat.LogMessage($"{vehicle.Name}({vehicle.VehicleType}) flashed their lights in goodbye");
                    }
                }
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
                    LstCars.Items.Refresh();
                    LstBoats.Items.Refresh();
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
                    LstCars.Items.Refresh();
                    LstBoats.Items.Refresh();
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