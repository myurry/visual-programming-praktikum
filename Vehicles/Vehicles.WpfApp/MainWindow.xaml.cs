using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class MainWindow : Window
    {
        // ObservableCollection automatically refreshes UI list when items are added
        public ObservableCollection<Vehicle> Vehicles { get; } = new ObservableCollection<Vehicle>();

        public MainWindow()
        {
            InitializeComponent();
            LstVehicles.ItemsSource = Vehicles;
        }

        // 1. Open CreateVehicle dialog
        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            CreateVehicle dialog = new CreateVehicle();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true && dialog.CreatedVehicle != null)
            {
                Vehicles.Add(dialog.CreatedVehicle);
            }
        }



        private void MoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button moveBtn && moveBtn.Tag is Vehicle vehicle)
            {
                // Traverse visual tree to find the TxtItemSteps TextBox inside the same row
                DependencyObject parent = VisualTreeHelper.GetParent(moveBtn);
                while (parent != null && !(parent is Grid))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent is Grid rowGrid)
                {
                    TextBox stepsBox = rowGrid.FindName("TxtItemSteps") as TextBox;

                    if (stepsBox != null && int.TryParse(stepsBox.Text, out int steps))
                    {
                        vehicle.Move(steps);
                        LstVehicles.Items.Refresh(); // Refresh UI to display new mileage
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid integer for steps.", "Invalid Steps", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        // 3. Open LogWindow on vehicle item click/double-click
        private void LstVehicles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LstVehicles.SelectedItem is Vehicle selectedVehicle)
            {
                LogWindow logWin = new LogWindow(selectedVehicle);
                logWin.Owner = this;
                logWin.ShowDialog();
            }
        }
    }
}