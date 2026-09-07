using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaSplit.WpfApp_MVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    using PizzaSplit.Core;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float costPerPerson = float.Parse(costPerPersonTextBox.Text);
                int numberOfPeople = int.Parse(nLabel.Text);
                bool addTip = tipBox.IsChecked == true;

                // Perform the calculation
                float totalCost = costPerPerson * numberOfPeople;
                if (addTip)
                {
                    totalCost *= 1.1f; // Add 10% tip
                }

                resultLabel.Content = "Result: $" + totalCost.ToString("F2");
            }
            catch (System.FormatException)
            {
                resultLabel.Content = "Result: $" + "ERROR";
            }
        }
    }
}