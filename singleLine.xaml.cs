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
    public partial class singleLine : Window
    {
        Action<object, string> ApplyEvent;
        object Caller;
        public singleLine()
        {
            InitializeComponent();
        }
        public singleLine(object caller, string Text, Action<object, string> action, string fill = "")
        {
            InitializeComponent();
            this.Caller = caller;
            Title.Text = Text;
            ApplyEvent = action;
            TextLine.Text = fill;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            this.ApplyEvent(sender, TextLine.Text);
            this.Close();
        }
    }
}
