using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Schema;

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
            this.WindowState = WindowState.Minimized;
        }
        private void CreateNewFolder(object sender, MouseButtonEventArgs e)
        {
            CurrentPresetes.Main.CreateChildFolderMenu(sender, e);
        }
        private void CreateNewPreset(object sender, MouseButtonEventArgs e)
        {
            CurrentPresetes.Main.CreateChildPresetMenu(sender, e);
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

        public void PresetMouseEnter(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = new SolidColorBrush(Color.FromRgb(80, 80, 80));
            //((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }
        public void PresetMouseLeave(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            //((Border)((TextBlock)sender).Parent).BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }
        public (int Red, int Green, int Blue) ConvertHexToRgb(string hex)
        {
            var r = Convert.ToInt32(hex.Substring(0, 2), 16);
            var g = Convert.ToInt32(hex.Substring(2, 2), 16);
            var b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return (r, g, b);
        }
        public string ConvertRgbToHex(int Red, int Green, int Blue)
        {
            return Convert.ToString(Red, 16) + Convert.ToString(Green, 16) + Convert.ToString(Blue, 16);
        }

        private void FColorSquereChanged(object sender, RoutedEventArgs e)
        {
            if (inHex)
            {
                return;
            }
            FColorHex.Text =ConvertRgbToHex((int)Math.Round(FColorSquare.Color.RGB_R), (int)Math.Round(FColorSquare.Color.RGB_G), (int)Math.Round(FColorSquare.Color.RGB_B)).ToUpper();
            //FColorHex. = ((ColorPicker.SquarePicker)sender).Color;
        }
        private void FColorHexChanged(object sender, KeyEventArgs e)
        {
            inHex = true;
            string key = e.Key.ToString();
            if (FColorHex.Text.Length>=6 &&  !new Regex("^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            if (!new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$|^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled= true;
                return;
            }
            try
            {
                if (new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$").IsMatch(key) && new Regex("^([A-Fa-f0-9]{5}|[A-Fa-f0-9]{2})$").IsMatch(FColorHex.Text))
                {
                    FColorSquare.Color.RGB_R = ConvertHexToRgb(FColorHex.Text + key[key.Length-1]).Red;
                    FColorSquare.Color.RGB_G = ConvertHexToRgb(FColorHex.Text + key[key.Length - 1]).Green;
                    FColorSquare.Color.RGB_B = ConvertHexToRgb(FColorHex.Text + key[key.Length - 1]).Red;
                }

            }
            catch (Exception)
            { }

            inHex = false;
        }
        private bool inHex;
        private void BColorSquereChanged(object sender, RoutedEventArgs e)
        {
            if (inHex)
            {
                return;
            }
            BColorHex.Text = ConvertRgbToHex((int)Math.Round(BColorSquare.Color.RGB_R), (int)Math.Round(BColorSquare.Color.RGB_G), (int)Math.Round(BColorSquare.Color.RGB_B)).ToUpper();
            //FColorHex. = ((ColorPicker.SquarePicker)sender).Color;
        }
        private void BColorHexChanged(object sender, KeyEventArgs e)
        {
            inHex=true;
            string key = e.Key.ToString();
            if (BColorHex.Text.Length>=6 &&  !new Regex("^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            if (!new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$|^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled= true;
                return;
            }
            try
            {
                if (new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$").IsMatch(key) && new Regex("^([A-Fa-f0-9]{5}|[A-Fa-f0-9]{2})$").IsMatch(BColorHex.Text))
                {
                    BColorSquare.Color.RGB_R = ConvertHexToRgb(BColorHex.Text + key[key.Length-1]).Red;
                    BColorSquare.Color.RGB_G = ConvertHexToRgb(BColorHex.Text + key[key.Length - 1]).Green;
                    BColorSquare.Color.RGB_B = ConvertHexToRgb(BColorHex.Text + key[key.Length - 1]).Red;
                }

            }
            catch (Exception)
            { }

            inHex = false;
        }
    }
}
