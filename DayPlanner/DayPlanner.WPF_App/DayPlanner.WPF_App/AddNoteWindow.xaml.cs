using System;
using System.Windows;

using DayPlanner.Core;

namespace DayPlanner.WPF_App
{
    public partial class AddNoteWindow : Window
    {
        // Ensure non-null defaults to satisfy nullable analysis
        public string NoteText { get; private set; } = string.Empty;
        public TimeSpan NoteTime { get; private set; } = TimeSpan.Zero;

        // Flags used by MainWindow
        public bool IsEditMode { get; set; } = false;
        public bool IsDeleteRequested { get; private set; } = false;

        public AddNoteWindow()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!Logic.TryValidateNote(TxtText.Text, TxtTime.Text, out var time, out var errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NoteText = TxtText.Text.Trim();
            NoteTime = time;
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        // Add the missing Delete handler referenced by XAML
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Delete this note?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                IsDeleteRequested = true;
                DialogResult = false;
                Close();
            }
        }
    }
}