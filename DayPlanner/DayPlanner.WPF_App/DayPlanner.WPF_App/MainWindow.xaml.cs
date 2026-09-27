
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DayPlanner.WPF_App
{
    public partial class MainWindow : Window
    {
        private readonly List<NoteItem> _notes = new();
        private NoteItem? _selected;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddNoteWindow { Owner = this, IsEditMode = false };
            if (dlg.ShowDialog() == true)
            {
                var note = new NoteItem
                {
                    Id = Guid.NewGuid(),
                    Text = dlg.NoteText,
                    Time = dlg.NoteTime,
                    ColorHex = "#FFFFFF" // not used visually for notepad style
                };

                _notes.Add(note);
                RefreshNotes();
            }
        }

        private void RefreshNotes()
        {
            NotesPanel.Children.Clear();
            foreach (var n in _notes.OrderBy(x => x.Time))
            {
                NotesPanel.Children.Add(CreateLineVisual(n));
            }
        }

        private Border CreateLineVisual(NoteItem n)
        {
            // Outer border to draw a subtle divider line at the bottom (like ruled paper)
            var outer = new Border
            {
                BorderThickness = new Thickness(0, 0, 0, 1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)),
                Padding = new Thickness(8, 6, 8, 6),
                Background = Brushes.White
            };

            // Grid with two columns: time on left, text on right
            var grid = new Grid { HorizontalAlignment = HorizontalAlignment.Stretch };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) }); // time column
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var timeText = new TextBlock
            {
                Text = n.Time.ToString(@"h\:mm", CultureInfo.InvariantCulture),
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Consolas"),
                Foreground = new SolidColorBrush(Color.FromRgb(70, 70, 70)),
                Margin = new Thickness(0,0,8,0)
            };
            Grid.SetColumn(timeText, 0);

            var descText = new TextBlock
            {
                Text = n.Text,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Segoe UI"),
                Foreground = new SolidColorBrush(Color.FromRgb(30, 30, 30))
            };
            Grid.SetColumn(descText, 1);

            grid.Children.Add(timeText);
            grid.Children.Add(descText);

            outer.Child = grid;

            // click to edit/delete
            outer.Tag = n.Id;
            outer.MouseLeftButtonUp += Note_Click;
            outer.Cursor = Cursors.Hand;

            return outer;
        }

        private void Note_Click(object? sender, MouseButtonEventArgs e)
        {
            if (sender is Border b && b.Tag is Guid id)
            {
                _selected = _notes.FirstOrDefault(x => x.Id == id);
                if (_selected == null) return;

                var dlg = new AddNoteWindow { Owner = this, IsEditMode = true };
                dlg.TxtText.Text = _selected.Text;
                dlg.TxtTime.Text = _selected.Time.ToString(@"hh\:mm");

                var result = dlg.ShowDialog();

                if (dlg.IsDeleteRequested)
                {
                    _notes.Remove(_selected);
                    _selected = null;
                    RefreshNotes();
                    return;
                }

                if (result == true)
                {
                    _selected.Text = dlg.NoteText;
                    _selected.Time = dlg.NoteTime;
                    RefreshNotes();
                }
            }
        }

        private class NoteItem
        {
            public Guid Id { get; set; }
            public string Text { get; set; } = string.Empty;
            public TimeSpan Time { get; set; } = TimeSpan.Zero;
            public string ColorHex { get; set; } = "#FFFFFF";
        }
    }
}