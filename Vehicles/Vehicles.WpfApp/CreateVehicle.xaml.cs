using System.Windows;
using System.Windows.Controls;
using Vehicles.Core;

namespace Vehicles.WpfApp
{
    public partial class CreateVehicle : Window
    {
        public Vehicle CreatedVehicle { get; private set; }

        public CreateVehicle()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtName.Text.Trim();
            string selectedType = (CmbType.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (selectedType)
            {
                case "Car":
                    CreatedVehicle = new Car(name);
                    break;
                case "Boat":
                    CreatedVehicle = new Boat(name);
                    break;
                case "AmphibiousCar":
                    CreatedVehicle = new AmphibiousCar(name);
                    break;
            }

            DialogResult = true; // Closes window and signals success
        }
    }
}