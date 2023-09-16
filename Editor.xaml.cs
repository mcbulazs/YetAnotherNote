using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for Editor.xaml
    /// </summary>
    /// 
    public partial class Editor : Window
    {

        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = (-20);
        Preset Context;

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public Editor(Preset context)
        {
            InitializeComponent();
            Context = context;
            Loaded += Editor_Loaded;
        }

        private void Editor_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
        private void DragEditorWindow(object sender, MouseButtonEventArgs e)
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
            Application.Current.Shutdown();
        }
        private void ImagePaste(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                // Ctrl+V (paste) was pressed. Check clipboard data here.
                //IDataObject clipboardData = Clipboard.GetDataObject();

                if (Clipboard.ContainsImage())
                {
                    BitmapSource clipboardImage = Clipboard.GetImage();
                    e.Handled = true;
                    IDataObject imgInfo = Clipboard.GetDataObject();


                    string filePath = Context.Path;

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(clipboardImage));
                    filePath = "D:\\Test\\xd.PNG";
                    if (encoder != null)
                    {
                        // Save the BitmapSource using the specified encoder and file location
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            //encoder.Frames.Add(BitmapFrame.Create(clipboardImage));
                            encoder.Save(stream);
                        }

                        MessageBox.Show("Image saved successfully.");
                    }

                }

            }
        }
    }
}
