using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YetAnotherNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Presets CurrentPresetes;
        public MainWindow()
        {
            InitializeComponent();
            CurrentPresetes = new Presets(this);
        }
        private void DragMainWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Main(object sender, MouseButtonEventArgs e)
        {

        }

        private void CloseMainWindow(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MinimizeMainWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }
        private void CreateNewFolder(object sender, MouseButtonEventArgs e)
        {
            CurrentPresetes.Main.CreateChildFolderMenu(sender,e);
        }

        private void TitleBarButtonMouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background= new SolidColorBrush(Color.FromRgb(128,128,128));
            ((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }

        private void TitleBarButtonMouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public void PresetMouseEnter(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = new SolidColorBrush(Color.FromRgb(80, 80, 80));
            //((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }
        public void PresetMouseLeave(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = new SolidColorBrush(Color.FromRgb(64,64,64));
            //((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

    }
}
