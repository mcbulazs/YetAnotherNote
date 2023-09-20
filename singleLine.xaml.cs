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
        public bool isSaved;
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
            TextLine.Focus();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            isSaved=false;
            this.Close();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            isSaved=true;
            this.ApplyEvent(sender, TextLine.Text);
            this.Close();
        }

        private void TextLine_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                isSaved = true;
                this.ApplyEvent(sender, TextLine.Text);
                this.Close();
                return;
            }
            if (e.Key==Key.Escape)
            {
                isSaved = false;
                this.Close();
                return;
            }
        }
    }
}
