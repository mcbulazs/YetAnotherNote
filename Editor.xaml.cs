using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        public Preset Context;
        MainWindow MainWindow;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        public List<PastableImage> Images;
        public Editor(Preset context, MainWindow mainWindow)
        {
            InitializeComponent();
            Images = new List<PastableImage>();
            Context = context;
            setToSettings();
            MainWindow = mainWindow;

            Context.Content.ContentImages.ForEach(image => createImage(
                BytesToBitmap(image.Image),
                image.Id,
                image.Width,
                image.Height,
                image.X,
                image.Y));
        }
        private void DragEditorWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
                Context.Content.X = this.Left;
                Context.Content.Y = this.Top;
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
        private void ImagePaste(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var x = Clipboard.GetDataObject();
                if (Clipboard.ContainsImage())
                {
                    BitmapSource clipboardImage = Clipboard.GetImage();
                    e.Handled = true;
                    createImage(clipboardImage);

                }
                if (Clipboard.ContainsFileDropList())
                {
                    // Get the list of file paths from the clipboard
                    StringCollection fileDropList = Clipboard.GetFileDropList();

                    // Loop through each file path and process the image files
                    foreach (string filePath in fileDropList)
                    {
                        if (IsImageFile(filePath))
                        {
                            // Process the image file (e.g., read its content)
                            BitmapImage bitmapImage = new BitmapImage(new Uri(filePath));

                            // Convert the BitmapImage to a BitmapSource
                            BitmapSource bitmapSource =new FormatConvertedBitmap(bitmapImage, PixelFormats.Pbgra32, null, 0);
                            createImage(bitmapSource);

                            // Here, you can read the content of the image file as needed
                        }
                    }
                }
            }
        }
        static bool IsImageFile(string filePath)
        {
            // Check if the file has an image file extension (you can customize this list)
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
            string fileExtension = System.IO.Path.GetExtension(filePath);
            return Array.Exists(imageExtensions, ext => ext.Equals(fileExtension, StringComparison.OrdinalIgnoreCase));
        }
        private void createImage(BitmapSource bmp, long? id = null, double? width = null, double? height = null, double? x = null, double? y = null)
        {
            PastableImage image = new PastableImage(bmp, this, id, width, height, x, y);
            Images.Add(image);
            MainCanvas.Children.Add(image.rectangle);
        }
        private void setToSettings()
        {
            EditorTextBox.Text = Context.Content.Text;

            var Frgb = Presets.ConvertHexToRgb(Context.Content.ForegroundHex);
            var FColor = new SolidColorBrush(Color.FromRgb((byte)Frgb.Red, (byte)Frgb.Green, (byte)Frgb.Blue));
            FColor.Opacity = Context.Content.ForegroundAlpha / 255;
            EditorTextBox.Foreground = FColor;

            Images.ForEach(x => x.rectangle.Background.Opacity = Context.Content.ForegroundAlpha / 255);

            var Brgb = Presets.ConvertHexToRgb(Context.Content.BackgroundHex);
            var BColor = new SolidColorBrush(Color.FromRgb((byte)Brgb.Red, (byte)Brgb.Green, (byte)Brgb.Blue));
            BColor.Opacity = Context.Content.BackgroundAlpha / 255;
            EditorTextBox.Background = BColor;

            EditorTextBox.FontSize = Context.Content.FontSize;

            this.Topmost = Context.Content.TopMost;

            if (Context.Content.RemoveBorder)
            {
                // EditorTitleBarRow.Height = new GridLength(0);
                EditorTitleBar.Visibility = Visibility.Hidden;
                EditorBorder.BorderThickness = new Thickness(0);
                EditorCloseButton.Visibility = Visibility.Hidden;
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                //EditorTitleBarRow.Height = new GridLength(20);
                EditorTitleBar.Visibility = Visibility.Visible;
                EditorBorder.BorderThickness = new Thickness(3, 0, 3, 3);
                EditorCloseButton.Visibility = Visibility.Visible;
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            if (Context.Content.ClickThrough)
            {

                if (this.IsLoaded)
                {
                    IntPtr hwnd = new WindowInteropHelper(this).Handle;
                    int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                    SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
                }
                else
                {
                    this.Loaded += (object sender, RoutedEventArgs e) =>
                    {
                        IntPtr hwnd = new WindowInteropHelper(this).Handle;
                        int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                        SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
                    };
                }
            }
            else
            {

                if (this.IsLoaded)
                {
                    IntPtr hwnd = new WindowInteropHelper(this).Handle;
                    int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                    SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
                }
                else
                {
                    this.Loaded += (object sender, RoutedEventArgs e) =>
                    {
                        IntPtr hwnd = new WindowInteropHelper(this).Handle;
                        int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                        SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
                    };
                }
            }
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = Context.Content.X;
            this.Top = Context.Content.Y;
            this.Width = Context.Content.Width;
            this.Height = Context.Content.Height;
        }
        public static byte[] ImageToBytes(BitmapSource image)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
        public static BitmapSource BytesToBitmap(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public void UpdateSettings()
        {
            setToSettings();
        }

        private void EditorSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState==WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            Context.Content.Width = this.Width;
            Context.Content.Height = this.Height;
        }

        private void EditorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Context.Content.Text = EditorTextBox.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Context.isSelected= false;
            ((StackPanel)this.Context.StackPanelItem.Children[0]).Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            if (this.MainWindow.ActivePresetControl.Equals(this.Context))
            {
                this.MainWindow.ShowButton.IsChecked = false;
            }
        }

    }
    public class PastableImage
    {
        public Grid rectangle;
        private bool isDragging;
        private Point startPoint;
        private Editor editor;
        private Thumb resizeThumb;
        long Id;
        public PastableImage(BitmapSource bmp, Editor editor, long? Id = null, double? width = null, double? height = null, double? x = null, double? y = null)
        {
            this.editor = editor;
            ImageBrush image = new ImageBrush();
            image.ImageSource = bmp;
            this.Id = Id ?? editor.Context.Content.NextImageId++;
            image.Opacity = editor.Context.Content.ForegroundAlpha / 255;
            rectangle = new Grid
            {
                Width = width ?? bmp.Width,
                Height = height ?? bmp.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = image,
                Cursor = Cursors.Hand,
            };
            Canvas.SetLeft(rectangle, x ?? 50);
            Canvas.SetTop(rectangle, y ?? 50);


            rectangle.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rectangle.MouseMove += Rectangle_MouseMove;
            rectangle.MouseLeftButtonUp += Rectangle_MouseLeftButtonUp;




            resizeThumb = new Thumb
            {
                Width = 10,
                Height = 10,
                Background = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Cursor = Cursors.SizeNWSE,
                Visibility = Visibility.Hidden
            };

            ControlTemplate gripTemplate = new ControlTemplate(typeof(Thumb));
            var ellipse = new FrameworkElementFactory(typeof(Ellipse));
            ellipse.SetValue(Ellipse.WidthProperty, 16.0);
            ellipse.SetValue(Ellipse.HeightProperty, 16.0);
            ellipse.SetValue(Ellipse.StrokeProperty, Brushes.Black);
            ellipse.SetValue(Ellipse.StrokeThicknessProperty, 1.0);
            ellipse.SetValue(Ellipse.FillProperty, Brushes.LightBlue);

            gripTemplate.VisualTree = ellipse;

            resizeThumb.Template = gripTemplate;
            resizeThumb.DragDelta += ResizeThumb_DragDelta;

            Border bord = new Border
            {
                BorderBrush = new SolidColorBrush(Colors.Aqua),
                BorderThickness = new Thickness(0),
            };
            rectangle.Children.Add(bord);
            rectangle.MouseEnter += (object sender, MouseEventArgs e) =>
            {
                bord.BorderThickness = new Thickness(2, 2, 2, 2);
                resizeThumb.Visibility = Visibility.Visible;
            };
            rectangle.MouseLeave += (object sender, MouseEventArgs e) =>
            {
                bord.BorderThickness = new Thickness(0);
                resizeThumb.Visibility = Visibility.Hidden;
            };
            rectangle.Children.Add(resizeThumb);



            ContextMenu delMenu = new ContextMenu();
            MenuItem del = new MenuItem
            {
                Header = "Delete"
            };
            del.Click += deleteImage;
            delMenu.Items.Add(del);
            rectangle.ContextMenu = delMenu;

            if (Id == null)
            {
                editor.Context.Content.ContentImages.Add(new ContentImage
                {
                    Id = this.Id,
                    Width = bmp.Width,
                    Height = bmp.Height,
                    Image = Editor.ImageToBytes(bmp),
                    X = Canvas.GetLeft(rectangle),
                    Y = Canvas.GetTop(rectangle)
                });
            }
            editor.Context.UpdateItemSettings();
        }
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(rectangle);
            rectangle.CaptureMouse();
        }
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newPoint = e.GetPosition(editor);

                double deltaX = newPoint.X - startPoint.X;
                double deltaY = newPoint.Y - startPoint.Y - 20;
                /*
                double newLeft = Canvas.GetLeft(rectangle) + deltaX;
                double newTop = Canvas.GetTop(rectangle) + deltaY;*/
                var x = Canvas.GetLeft(rectangle);
                if ((Canvas.GetLeft(rectangle) >= 0 || deltaX >= 0) && (Canvas.GetLeft(rectangle) + rectangle.Width <= editor.MainCanvas.ActualWidth - 2 || deltaX - Canvas.GetLeft(rectangle) <= 0))
                {
                    Canvas.SetLeft(rectangle, deltaX);
                }
                if ((Canvas.GetTop(rectangle) >= 0 || deltaY >= 0) && (Canvas.GetTop(rectangle) + rectangle.Height <= editor.MainCanvas.ActualHeight - 2 || deltaY - Canvas.GetTop(rectangle) <= 0))
                {
                    Canvas.SetTop(rectangle, deltaY);
                }
                editor.Context.Content.ContentImages.Single(x => x.Id == this.Id).X = Canvas.GetLeft(rectangle);
                editor.Context.Content.ContentImages.Single(x => x.Id == this.Id).Y = Canvas.GetTop(rectangle);
                editor.Context.UpdateItemSettings();
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            rectangle.ReleaseMouseCapture();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = rectangle.ActualWidth + e.HorizontalChange;
            double newHeight = rectangle.ActualHeight / rectangle.ActualWidth * newWidth;

            if (newWidth >= 30 && newHeight >= 30 && Canvas.GetLeft(rectangle) + newWidth <= editor.MainCanvas.ActualWidth - 2 && Canvas.GetTop(rectangle) + newHeight <= editor.MainCanvas.ActualHeight - 2)
            {
                rectangle.Width = newWidth;
                rectangle.Height = newHeight;
            }
            editor.Context.Content.ContentImages.Where(x => x.Id == this.Id).SingleOrDefault().Width = newWidth;
            editor.Context.Content.ContentImages.Where(x => x.Id == this.Id).SingleOrDefault().Height = newHeight;
            editor.Context.Content.X = Canvas.GetLeft(rectangle);
            editor.Context.UpdateItemSettings();
        }
        private void deleteImage(object sender, RoutedEventArgs e)
        {
            editor.Images.Remove(this);
            editor.MainCanvas.Children.Remove(this.rectangle);
            ContentImage deletable = editor.Context.Content.ContentImages.Where(x => x.Id == this.Id).SingleOrDefault();
            editor.Context.Content.ContentImages.Remove(deletable);
            editor.Context.UpdateItemSettings();

        }
    }
}
