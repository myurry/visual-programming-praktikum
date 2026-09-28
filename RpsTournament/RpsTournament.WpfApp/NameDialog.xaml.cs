using System.Windows;
using RpsTournament.Core;


namespace RpsTournament.WpfApp
{
    public partial class NameDialog : Window
    {
        public string PlayerName { get; private set; } = string.Empty;

        public NameDialog()
        {
            InitializeComponent();
            NameTextBox.Focus();
        }

        private void NameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = NameTextBox.Text?.Trim() ?? string.Empty;
            if (text.Length < 2)
            {
                ErrorText.Text = GameStates.ShortNameError;
                ErrorText.Visibility = Visibility.Visible;
                OkButton.IsEnabled = false;
            }
            else if (text.Length > 30)
            {
                ErrorText.Text = GameStates.LongNameError;
                ErrorText.Visibility = Visibility.Visible;
                OkButton.IsEnabled = false;
            }
            else
            {
                ErrorText.Visibility = Visibility.Collapsed;
                OkButton.IsEnabled = true;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerName = NameTextBox.Text.Trim();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}