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
    }
}
