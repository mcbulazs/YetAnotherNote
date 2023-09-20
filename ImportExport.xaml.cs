using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace YetAnotherNote
{
    /// <summary>
    /// Interaction logic for ImportExport.xaml
    /// </summary>
    public partial class ImportExport : Window
    {
        Action<object, string> ApplyEvent;
        public ImportExport(string text="", Action<object, string> ApplyEvent = null)
        {
            InitializeComponent();
            ImportExportTextBox.Text = text;
            if (!String.IsNullOrWhiteSpace(text))
            {
                SaveButton.Visibility = Visibility.Hidden;
                ImportExportTextBox.SelectionStart = 0;
                ImportExportTextBox.SelectionLength = ImportExportTextBox.Text.Length;
                ImportExportTextBox.Focus();
            }
            this.ApplyEvent = ApplyEvent;
            this.ShowDialog();
        }
        private void DragImportExport(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void TitleBarButtonMouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            ((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }

        private void TitleBarButtonMouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }
        private void CloseEditorWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ApplyEvent(sender, ImportExportTextBox.Text);
        }
    }
}
