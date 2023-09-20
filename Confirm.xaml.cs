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
    /// Interaction logic for singleLine.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        Action OkEvent;
        public Confirm()
        {
            InitializeComponent();
        }
        public Confirm(string Text, Action action)
        {
            InitializeComponent();
            Title.Text += Text + "?";
            OkEvent = action;
            NO.Focus();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            this.OkEvent();
            this.Close();
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.OkEvent();
                this.Close();
                return;
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
                return;
            }
        }

        private void ApplyButton_GotFocus(object sender, RoutedEventArgs e)
        {
            ((Button)sender).BorderBrush=new SolidColorBrush(Colors.Aqua);
        }
        private void ApplyButton_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Button)sender).BorderBrush=new SolidColorBrush(Colors.Transparent);
        }
    }
}
