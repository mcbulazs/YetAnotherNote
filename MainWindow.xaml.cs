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
using System.Windows.Controls.Primitives;
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
        Presets CurrentPresets;
        public Preset ActivePresetControl;
        public MainWindow()
        {
            InitializeComponent();
            ActivePresetControl = null;
            CurrentPresets = new Presets(this);
            //ButtonStyle();

        }
        private void DragMainWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
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
            CurrentPresets.Main.CreateChildFolderMenu(sender, e);
        }
        private void CreateNewPreset(object sender, MouseButtonEventArgs e)
        {
            CurrentPresets.Main.CreateChildPresetMenu(sender, e);
        }
        private void Import(object sender, MouseButtonEventArgs e)
        {
            CurrentPresets.Main.Import(sender, e);
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
       

        private void FColorSquereChanged(object sender, RoutedEventArgs e)
        {
            if (inHex)
            {
                return;
            }
            FColorHex.Text = Presets.ConvertRgbToHex((int)Math.Round(FColorSquare.Color.RGB_R), (int)Math.Round(FColorSquare.Color.RGB_G), (int)Math.Round(FColorSquare.Color.RGB_B)).ToUpper();
            ActivePresetControl.Content.ForegroundHex = FColorHex.Text;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }
        private void FColorHexChanged(object sender, KeyEventArgs e)
        {
            inHex = true;
            string key = e.Key.ToString();
            if (FColorHex.Text.Length >= 6 && !new Regex("^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            if (!new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$|^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            try
            {
                if (new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$").IsMatch(key) && new Regex("^([A-Fa-f0-9]{5}|[A-Fa-f0-9]{2})$").IsMatch(FColorHex.Text))
                {
                    FColorSquare.Color.RGB_R = Presets.ConvertHexToRgb(FColorHex.Text + key[key.Length - 1]).Red;
                    FColorSquare.Color.RGB_G = Presets.ConvertHexToRgb(FColorHex.Text + key[key.Length - 1]).Green;
                    FColorSquare.Color.RGB_B = Presets.ConvertHexToRgb(FColorHex.Text + key[key.Length - 1]).Red;
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
            BColorHex.Text = Presets.ConvertRgbToHex((int)Math.Round(BColorSquare.Color.RGB_R), (int)Math.Round(BColorSquare.Color.RGB_G), (int)Math.Round(BColorSquare.Color.RGB_B)).ToUpper();
            ActivePresetControl.Content.BackgroundHex = BColorHex.Text;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
            //FColorHex. = ((ColorPicker.SquarePicker)sender).Color;
        }
        private void BColorHexChanged(object sender, KeyEventArgs e)
        {
            inHex = true;
            string key = e.Key.ToString();
            if (BColorHex.Text.Length >= 6 && !new Regex("^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            if (!new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$|^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
            try
            {
                if (new Regex("^[A-Fa-f]$|^NumPad[0-9]$|^D[0-9]$").IsMatch(key) && new Regex("^([A-Fa-f0-9]{5}|[A-Fa-f0-9]{2})$").IsMatch(BColorHex.Text))
                {
                    BColorSquare.Color.RGB_R = Presets.ConvertHexToRgb(BColorHex.Text + key[key.Length - 1]).Red;
                    BColorSquare.Color.RGB_G = Presets.ConvertHexToRgb(BColorHex.Text + key[key.Length - 1]).Green;
                    BColorSquare.Color.RGB_B = Presets.ConvertHexToRgb(BColorHex.Text + key[key.Length - 1]).Red;
                }

            }
            catch (Exception)
            { }

            inHex = false;
        }

        private void FColorAplhaChanged(object sender, RoutedEventArgs e)
        {
            ActivePresetControl.Content.ForegroundAlpha = FColorAlpha.Color.A;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }
        private void BColorAplhaChanged(object sender, RoutedEventArgs e)
        {
            ActivePresetControl.Content.BackgroundAlpha = BColorAlpha.Color.A;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }

        private void CBClickThrough_Click(object sender, RoutedEventArgs e)
        {

            ActivePresetControl.Content.ClickThrough = (bool)CBClickThrough.IsChecked;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }

        private void CBTopMost_Click(object sender, RoutedEventArgs e)
        {
            ActivePresetControl.Content.TopMost = (bool)CBTopMost.IsChecked;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }

        private void CBRemoveBorder_Click(object sender, RoutedEventArgs e)
        {
            ActivePresetControl.Content.RemoveBorder = (bool)CBRemoveBorder.IsChecked;
            ActivePresetControl.UpdateItemSettings();
            ActivePresetControl.editor?.UpdateSettings();
        }

        private void FontSizeKeyDown(object sender, KeyEventArgs e)
        {
            string key = e.Key.ToString();
            if (!new Regex("^NumPad[0-9]$|^D[0-9]$|^Back$|^Left$|^Right$|^Delete$").IsMatch(key))
            {
                e.Handled = true;
                return;
            }
        }

        private void FontSizeIncreaseValue(object sender, RoutedEventArgs e)
        {
            FontSizeTextBox.Text = (int.Parse(FontSizeTextBox.Text) + 1).ToString();
        }

        private void FontSizeDecreaseValue(object sender, RoutedEventArgs e)
        {
            if (FontSizeTextBox.Text!="1")
            {
                FontSizeTextBox.Text = (int.Parse(FontSizeTextBox.Text) - 1).ToString();
            }
        }

        private void UpdateFontSizeSettings(object sender, TextChangedEventArgs e)
        {
            if(FontSizeTextBox.Text.Length == 0 || new Regex("^0*$").IsMatch(FontSizeTextBox.Text))
            {
                FontSizeTextBox.Text = "1";

            }
            if (ActivePresetControl!=null)
            {
                ActivePresetControl.Content.FontSize = int.Parse(FontSizeTextBox.Text);
                ActivePresetControl.UpdateItemSettings();
                ActivePresetControl.editor?.UpdateSettings();
            }
        }
        List<Editor> EditorWindows = new List<Editor>();
        public void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                if (toggleButton.IsChecked == true)
                {
                    this.ActivePresetControl.StackPanelItem.Background = new SolidColorBrush(Color.FromRgb(120, 120, 120));
                    this.ActivePresetControl.isSelected = true;
                    Editor editor = new Editor(ActivePresetControl,this);
                    ActivePresetControl.editor= editor;
                    editor.Show();
                    EditorWindows.Add(editor);
                    // Button is checked (toggled on)
                    // Perform actions for when the button is checked
                }
                else
                {
                    this.ActivePresetControl.isSelected = false ;
                    Editor editor = ActivePresetControl.editor;
                    editor.Close();
                    ActivePresetControl.editor = null;
                    EditorWindows.Remove(editor);
                    // Button is unchecked (toggled off)
                    // Perform actions for when the button is unchecked
                }
            }
            else
            {
                if (ActivePresetControl!=null)
                {
                    Editor editor = ActivePresetControl.editor;
                    editor.Close();
                    ActivePresetControl.editor = null;
                    EditorWindows.Remove(editor);
                }
                // Button is unchecked (toggled off)
                // Perform actions for when the button is unchecked
            }
        }
    }
}
