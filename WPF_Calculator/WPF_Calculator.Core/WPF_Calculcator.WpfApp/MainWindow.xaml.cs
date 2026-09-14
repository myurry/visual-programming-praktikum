using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using WPF_Calculator.Core;

namespace WPF_Calculcator.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Parse inputs using current culture to accept comma or dot decimals
            var culture = CultureInfo.CurrentCulture;
            if (!double.TryParse(PowerTextBox.Text, NumberStyles.Number, culture, out double powerWatts) ||
                !double.TryParse(HoursTextBox.Text, NumberStyles.Number, culture, out double hoursPerDay) ||
                !double.TryParse(PriceTextBox.Text, NumberStyles.Number, culture, out double pricePerKwh))
            {
                MessageBox.Show("Please enter valid numeric values for power, hours and price.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // kWh per day = (W * hours) / 1000
            double totalCost = ElectricityCostCalculator.EvaluateCost(powerWatts, hoursPerDay, pricePerKwh);


            ResultTextBlock.Text = $"€{totalCost:F2}";
        }
    }
}