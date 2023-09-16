using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace YetAnotherNote
{
    public class ContentImage
    {
        public double Width;
        public double Height;
        public double X;
        public double Y;
        public string Image;
    }
    public class PresetContent 
    {
        public double X;
        public double Y;
        public string ForegroundHex;
        public string BackgroundHex;
        public double ForegroundAlpha;
        public double BackgroundAlpha;
        public bool TopMost;
        public bool RemoveBorder;
        public bool ClickThrough;
        public int FontSize;
        List<ContentImage> ContentImages;
    }
    class ItemSettings
    {
        public string Type;
        public string Name;
        public string Path;
    }
    class FolderItemSettings : ItemSettings
    {
        public List<string> Children;
        public bool IsExpanded;
    }
    class PresetItemSettings : ItemSettings
    {
        public PresetContent Content;
    }
    public class PresetItem
    {
        public string Name;
        public string Path;
        public Folder Parent;
        protected Presets Context;
        public StackPanel StackPanelItem;
        public string FolderName;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\YetAnotherNote";
        public PresetItem(Folder parent, string name, Presets context, string path)
        {
            this.Name = name;
            this.Parent = parent;
            if (parent != null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    reCalcPath();
                }
                else
                {
                    this.Path = path;
                    this.FolderName = path.Substring(path.LastIndexOf("\\") + 1, path.Length - path.LastIndexOf("\\") - 1);
                }
            }
            else
            {
                this.Path = "\\Presets";
                this.FolderName = "Presets";
            }
            Context = context;
        }
        private void reCalcPath()
        {
            int i = 0;
            if (this.GetType() == typeof(Folder))
            {
                while (Directory.Exists(AppDataPath + this.Parent.Path + "\\" + StringToPascalCase(this.Name) + "_" + i))
                {
                    i += 1;
                }
                this.Path = this.Parent.Path + "\\" + StringToPascalCase(this.Name) + "_" + i;
            }
            else
            {
                while (File.Exists(AppDataPath + this.Parent.Path + "\\" + StringToPascalCase(this.Name) + "_" + i + ".json"))
                {
                    i += 1;
                }

                this.Path = this.Parent.Path + "\\" + StringToPascalCase(this.Name) + "_" + i + ".json";
            }

            this.FolderName = this.GetType() == typeof(Folder) ? StringToPascalCase(this.Name) + "_" + i : this.Parent.FolderName;
            if (this.GetType() == typeof(Folder) && ((Folder)this)?.Presets != null)
            {
                foreach (var item in ((Folder)this)?.Presets)
                {
                    item.reCalcPath();
                }
            }
        }
        public virtual void Delete()
        {
            this.Parent.Presets.Remove(this);
            this.Parent.UpdateItemSettings();
            if (this.GetType() == typeof(Folder))
            {
                Directory.Delete(this.getWholePath(), true);
                int index = Context.StackPanels.IndexOf(this.StackPanelItem);
                int amount = Context.calcRemoveAmount((Folder)this) + 1;
                for (int i = index; i < index + amount; i++)
                {
                    Context.MainWindow.ScrollViewerContent.Children.Remove(Context.StackPanels[i]);
                }
                Context.StackPanels.RemoveRange(index, amount);
            }
            else
            {
                int index = Context.StackPanels.IndexOf(this.StackPanelItem);
                Context.MainWindow.ScrollViewerContent.Children.Remove(Context.StackPanels[index]);
                Context.StackPanels.RemoveAt(index);
            }
            Context.resizeStackPanels();
        }
        public virtual string getJsonPath()
        {
            return AppDataPath + this.Path + "\\" + this.FolderName + ".json";
        }
        public static string getJsonPath(string text)
        {
            if (text.EndsWith(".json"))
            {
                return AppDataPath + text;
            }
            return AppDataPath + text + "\\" + text.Substring(text.LastIndexOf('\\') + 1) + ".json";
        }

        public string getWholePath()
        {
            return AppDataPath + this.Path;
        }
        public void setName(string name)
        {
            this.Name = name;
        }
        protected string StringToPascalCase(string s)
        {
            StringBuilder resultBuilder = new StringBuilder();

            foreach (char c in s)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    resultBuilder.Append(" ");
                }
                else
                {
                    resultBuilder.Append(c);
                }
            }
            string output = "";
            foreach (string e in resultBuilder.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x[0].ToString().ToUpper() + x.Substring(1)))
                output += e;
            return output;
        }
        public virtual StackPanel GenerateItem()
        {
            int numberOfParents = GetNumberOfParents();
            StackPanel item = new StackPanel();
            item.Height = 15;
            item.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            item.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            item.Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            item.Orientation = Orientation.Horizontal;
            item.SizeChanged += StackPanelSizeChanged;
            //Text
            TextBlock Text = new TextBlock();
            Text.Height = 15;

            Text.Text = Name;
            Text.Foreground = new SolidColorBrush(Color.FromRgb(0, 192, 192));
            Text.Margin = new Thickness(0, 0, 0, 0);

            Text.LineHeight = 15;
            Text.FontSize = 11;
            Text.TextAlignment = System.Windows.TextAlignment.Center;
            Text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Text.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            item.Children.Add(Text);


            TextBlock Move = new TextBlock();
            Move.Cursor = Cursors.Hand;
            Move.Text = "━";
            Move.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Move.TextAlignment = TextAlignment.Center;
            Move.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            Move.Width = 15;
            Move.Height = 15;
            Move.LineHeight = 15;
            Move.FontSize = 11;
            Move.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            Move.MouseLeftButtonDown += MoveCommand;
            if (!Context.InMove || this.GetType() != typeof(Folder))
            {
                Move.Visibility = Visibility.Collapsed;
            }
            item.Children.Add(Move);

            item.MouseLeftButtonDown += OpenControls;



            ContextMenu contextMenu = new ContextMenu();

            MenuItem rename = new MenuItem();
            rename.Header = "Rename";
            rename.Click += RenameMenu;
            contextMenu.Items.Add(rename);

            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            delete.Click += DeleteMenu;
            contextMenu.Items.Add(delete);

            MenuItem move = new MenuItem();
            move.Header = "Move";
            move.Click += MoveMenu;
            contextMenu.Items.Add(move);

            item.ContextMenu = contextMenu;
            item.ContextMenuOpening += (sender, e) =>
            {
                if (Context.InMove)
                {
                    e.Handled = true; // Prevent the context menu from opening
                }
            };
            return item;
        }
        public void OpenControls(object sender, RoutedEventArgs e)
        {
            return;
            MainWindow controls = Context.MainWindow;
            if (this.GetType() == typeof(Preset))
            {
                controls.PresetControls.Visibility = Visibility.Visible;
            }
            else
            {
                controls.PresetControls.Visibility = Visibility.Hidden;
                return;
            }
            Preset item = (Preset)this;

            controls.ActivePresetControl = item;
            //foreground
            var FColor = controls.ConvertHexToRgb(item.Content.ForegroundHex);
            controls.FColorSquare.Color.RGB_R =  FColor.Red; 
            controls.FColorSquare.Color.RGB_G =  FColor.Green; 
            controls.FColorSquare.Color.RGB_B =  FColor.Blue;

            controls.FColorHex.Text = item.Content.ForegroundHex;

            /*controls.FColorHex.Color.RGB_R =  FColor.Red; 
            controls.FColorHex.Color.RGB_R =  FColor.Green; 
            controls.FColorHex.Color.RGB_R =  FColor.Blue;*/

            controls.FColorAlpha.Color.A = item.Content.ForegroundAlpha;

            controls.FontSizeTextBox.Text = item.Content.FontSize.ToString();
            //background
            var BColor = controls.ConvertHexToRgb(item.Content.BackgroundHex);
            controls.BColorSquare.Color.RGB_R = BColor.Red;
            controls.BColorSquare.Color.RGB_G = BColor.Green;
            controls.BColorSquare.Color.RGB_B = BColor.Blue;

            controls.BColorHex.Text = item.Content.BackgroundHex;


            controls.BColorAlpha.Color.A = item.Content.BackgroundAlpha;

            controls.CBTopMost.IsChecked = item.Content.TopMost;
            controls.CBClickThrough.IsChecked = item.Content.ClickThrough;
            controls.CBRemoveBorder.IsChecked = item.Content.RemoveBorder;


        }
        public void MoveMenu(object sender, RoutedEventArgs e)
        {
            Context.StackPanels.Where(x => x.Name == "Folder").ToList().ForEach(x =>
            {
                TextBlock item = ((TextBlock)x.Children[2]);
                item.Visibility = Visibility.Visible;
                item.Text = "━";
                item.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            });
            if (this.Parent != Context.Main)
            {
                ((TextBlock)this.Parent.StackPanelItem.Children[2]).Visibility = Visibility.Collapsed;
            }
            TextBlock thisItem = (TextBlock)this.StackPanelItem.Children[2];
            thisItem.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            thisItem.Text = "✖";
            thisItem.Visibility = Visibility.Visible;
            Context.InMove = true;
            Context.StackPanels.ForEach(x => x.ContextMenu.IsEnabled = false);
            Context.MovedItem = this;
        }
        public void MoveCommand(object sender, RoutedEventArgs e)
        {
            Context.StackPanels.ForEach(x => ((TextBlock)x.Children[2]).Visibility = Visibility.Collapsed);
            Context.InMove = false;

            Context.StackPanels.ForEach(x => x.ContextMenu.IsEnabled = true);

            if (Context.MovedItem.Equals(this))
            {
                return;
            }
            Context.MovedItem.Parent.Presets.Remove(Context.MovedItem);
            Context.MovedItem.Parent.UpdateItemSettings();
            ((Folder)this).Presets.Add(Context.MovedItem);
            Context.MovedItem.Parent = (Folder)this;

            string oldPath = Context.MovedItem.getWholePath();
            Context.MovedItem.reCalcPath();
            if (Context.MovedItem.GetType() == typeof(Folder))
            {
                Directory.Move(oldPath, Context.MovedItem.getWholePath());
                ((Folder)Context.MovedItem).UpdateItemSettings();
            }
            else
            {
                File.Move(oldPath, Context.MovedItem.getJsonPath());
                ((Preset)Context.MovedItem).UpdateItemSettings();
            }
            ((Folder)this).UpdateItemSettings();
            Context.GenerateStackPanels(Context.Main, true);
        }
        public void StackPanelSizeChanged(object sender, RoutedEventArgs e)
        {
            TextBlock modifyitem = ((TextBlock)((StackPanel)sender).Children[2]);
            if (modifyitem.Text== "✖")
            {
                Context.MainWindow.FontSizeTextBox.Text = ((TextBlock)((StackPanel)sender).Children[1]).ActualWidth.ToString();
                //Context.MainWindow.FontSizeTextBox.Text = modifyitem.Margin.Left.ToString();
            }
            double margin = (((StackPanel)sender).ActualWidth - ((TextBlock)((StackPanel)sender).Children[1]).ActualWidth - 30 - this.GetNumberOfParents() * 10 + 5) - 10;
            modifyitem.Margin = new Thickness(margin, 0, 0, 0);
        }


        public void DeleteMenu(object sender, RoutedEventArgs e)
        {
            new Confirm(this.Name, Delete).ShowDialog();

            //Context.GenerateStackPanels(Context.Main);
        }
        public void RenameMenu(object sender, RoutedEventArgs e)
        {
            new singleLine(this, "Rename", Rename, this.Name).ShowDialog();

            //Context.GenerateStackPanels(Context.Main);
        }
        private void Rename(object sender, string name)
        {
            this.Name = name;
            ((TextBlock)this.StackPanelItem.Children[1]).Text = name;
            if (this.GetType() == typeof(Folder))
            {
                ((Folder)this).UpdateItemSettings();
                return;
            }
            ((Preset)this).UpdateItemSettings();
        }
        protected int GetNumberOfParents()
        {
            return this.Path.Count(x => x == '\\') - 1;
        }
    }
    public class Preset : PresetItem
    {
        public PresetContent Content;
        public Preset(Folder parent, string name, Presets context, string path = null) : base(parent, name, context, path)
        {
            if (!File.Exists(getJsonPath()))
                GenerateItemSettings();
        }
        public override void Delete()
        {
            base.Delete();
            File.Delete(this.getJsonPath());
        }
        public void GenerateItemSettings()
        {
            PresetItemSettings settings = new PresetItemSettings();
            settings.Type = "Preset";
            settings.Name = this.Name;
            settings.Path = this.Path;
            settings.Content = this.Content;

            string json = JsonConvert.SerializeObject(settings);
            string path = getJsonPath();
            File.WriteAllText(path, json);
        }
        public void UpdateItemSettings()
        {
            string json = File.ReadAllText(getJsonPath());
            PresetItemSettings settings = JsonConvert.DeserializeObject<PresetItemSettings>(json);
            settings.Name = this.Name;
            settings.Path = this.Path;
            settings.Content = this.Content;
            File.WriteAllText(getJsonPath(), JsonConvert.SerializeObject(settings));
        }
        public override StackPanel GenerateItem()
        {
            int numberOfParents = GetNumberOfParents();
            StackPanelItem = base.GenerateItem();
            StackPanelItem.Name = "Preset";
            TextBlock icon = new TextBlock();
            icon.Text = "🗎";
            icon.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            icon.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            icon.Width = 10;
            icon.Height = 15;
            icon.LineHeight = 15;
            icon.FontSize = 11;
            icon.Margin = new Thickness(numberOfParents * 10 + 5, 0, 0, 0);
            icon.Foreground = new SolidColorBrush(Color.FromRgb(203, 119, 35));
            StackPanelItem.Children.Insert(0, icon);

            MenuItem duplicate = new MenuItem();
            duplicate.Header = "Duplicate";
            duplicate.Click += Duplicate;
            StackPanelItem.ContextMenu.Items.Add(duplicate);
            ((TextBlock)StackPanelItem.Children[0]).Margin = new Thickness(numberOfParents * 10 + 5, 0, 0, 0);

            StackPanelItem.MouseLeftButtonDown += (sender, e) =>
            {
                Editor asd = new Editor(this);
                asd.ShowDialog();
            };
            return StackPanelItem;
        }
        public override string getJsonPath()
        {
            return AppDataPath + this.Path;
        }
        //TODO
        //expand with other settings
        public void Duplicate(object sender, RoutedEventArgs e)
        {
            Preset newItem = new Preset(this.Parent, this.Name + "_copy", this.Context);
            newItem.Content = this.Content;
            this.Parent.Presets.Add(newItem);
            this.Parent.UpdateItemSettings();
            newItem.GenerateItem();
            Context.GenerateStackPanels(Context.StackPanels.IndexOf(this.StackPanelItem) + 1, this.Parent);
        }
    }
    public class Folder : PresetItem
    {
        public List<PresetItem> Presets;
        public bool IsExpanded;
        public Folder(Folder parent, string name, Presets context, string path = null) : base(parent, name, context, path)
        {
            Presets = new List<PresetItem>();
            IsExpanded = false;
            if (!Directory.Exists(getWholePath()))
            {
                Directory.CreateDirectory(getWholePath());
                GenerateItemSettings();
            }

            if (!File.Exists(getJsonPath()))
                GenerateItemSettings();
        }
        public void GenerateItemSettings()
        {
            FolderItemSettings settings = new FolderItemSettings();
            settings.Type = "Folder";
            settings.Name = this.Name;
            settings.Children = new List<string>();
            settings.Path = this.Path;
            settings.IsExpanded = false;

            string json = JsonConvert.SerializeObject(settings);
            string path = getJsonPath();
            File.WriteAllText(path, json);
        }
        public void UpdateItemSettings()
        {
            string json = File.ReadAllText(getJsonPath());
            FolderItemSettings settings = JsonConvert.DeserializeObject<FolderItemSettings>(json);
            settings.Name = this.Name;
            settings.Children = this.Presets.Select(x => x.Path).ToList();
            settings.IsExpanded = this.IsExpanded;
            File.WriteAllText(getJsonPath(), JsonConvert.SerializeObject(settings));
        }
        public void CreateChildFolderMenu(object sender, RoutedEventArgs e)
        {
            new singleLine(this, "Create New Folder", CreateChildFolder).ShowDialog();

            //Context.GenerateStackPanels(Context.Main);
        }
        private void CreateChildFolder(object sender, string name)
        {
            Folder item = new Folder(this, name, this.Context);

            Presets.Add(item);
            int index = 0;
            if (this.StackPanelItem != null)
            {
                index = Context.StackPanels.IndexOf(this.StackPanelItem);
                Context.GenerateStackPanels(index + 1, this);
                ((TextBlock)this.StackPanelItem.Children[0]).RenderTransform = new RotateTransform(0);
            }
            else
            {
                index = Context.StackPanels.Count() - 1;
                Context.GenerateStackPanels(index + 1, this);
            }
            IsExpanded = true;
            UpdateItemSettings();
        }

        public void CreateChildPresetMenu(object sender, RoutedEventArgs e)
        {
            new singleLine(this, "Create New Preset", CreateChildPreset).ShowDialog();

            //Context.GenerateStackPanels(Context.Main);
        }
        private void CreateChildPreset(object sender, string name)
        {
            Preset item = new Preset(this, name, this.Context);

            Presets.Add(item);
            int index = 0;
            if (this.StackPanelItem != null)
            {
                index = Context.StackPanels.IndexOf(this.StackPanelItem);
                Context.GenerateStackPanels(index + 1, this);
                ((TextBlock)this.StackPanelItem.Children[0]).RenderTransform = new RotateTransform(0);
            }
            else
            {
                index = Context.StackPanels.Count() - 1;
                Context.GenerateStackPanels(index + 1, this);
            }
            IsExpanded = true;
            UpdateItemSettings();
        }
        public void CreateChildPreset(string Name)
        {
            Preset item = new Preset(this, Name, this.Context);
            Presets.Add(item);
            UpdateItemSettings();
        }
        public void AddChild(PresetItem item)
        {
            Presets.Add(item);
            UpdateItemSettings();
        }
        public void RemoveChild(PresetItem item)
        {
            Presets.Remove(item);
            UpdateItemSettings();
        }
        public void Rename(string Name)
        {
            this.Name = Name;
            UpdateItemSettings();
        }
        public override StackPanel GenerateItem()
        {
            int numberOfParents = GetNumberOfParents();
            StackPanelItem = base.GenerateItem();
            StackPanelItem.Name = "Folder";
            //Collapse button
            TextBlock collapse = new TextBlock();
            collapse.Text = "▼";
            collapse.Cursor = Cursors.Hand;
            if (!this.IsExpanded)
            {
                collapse.RenderTransform = new RotateTransform(30, 5, 5);
            }
            collapse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            collapse.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            collapse.Width = 10;
            collapse.Height = 15;
            collapse.LineHeight = 15;
            collapse.FontSize = 11;
            collapse.Margin = new Thickness(numberOfParents * 10 + 5, 0, 0, 0);
            collapse.Foreground = new SolidColorBrush(Color.FromRgb(0, 192, 0));
            collapse.MouseLeftButtonDown += CollapseFolder;
            StackPanelItem.Children.Insert(0, collapse);

            //Context menu


            /*contextMenu.Background = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            contextMenu.Foreground = new SolidColorBrush(Color.FromRgb(247, 199, 195));
            contextMenu.BorderBrush = new SolidColorBrush(Color.FromRgb(247, 199, 195));*/
            MenuItem newFolder = new MenuItem();
            newFolder.Header = "Create New Folder";
            newFolder.Click += CreateChildFolderMenu;
            StackPanelItem.ContextMenu.Items.Add(newFolder);

            MenuItem newPreset = new MenuItem();
            newPreset.Header = "Create New Preset";
            newPreset.Click += CreateChildPresetMenu;
            StackPanelItem.ContextMenu.Items.Add(newPreset);

            //StackPanelItem.MouseRightButtonDown += savePanelOnContextOpen;
            return StackPanelItem;
        }

        private void CollapseFolder(object sender, MouseButtonEventArgs e)
        {
            if (((TextBlock)sender).RenderTransform is RotateTransform rotateTransform)
            {
                if (rotateTransform.Angle == 30)//open
                {
                    ((TextBlock)sender).RenderTransform = new RotateTransform(0);
                    int index = Context.StackPanels.IndexOf((StackPanel)((TextBlock)sender).Parent);
                    Context.GenerateStackPanels(index + 1, this);
                    IsExpanded = true;
                    UpdateItemSettings();
                }
                else//close
                {
                    ((TextBlock)sender).RenderTransform = new RotateTransform(30, 5, 5);
                    Context.RemoveStackPanels(this);
                    IsExpanded = false;
                    UpdateItemSettings();
                }
            }
            else//close
            {
                ((TextBlock)sender).RenderTransform = new RotateTransform(30, 5, 5);
                Context.RemoveStackPanels(this);
                IsExpanded = false;
                UpdateItemSettings();
            }
        }
    }
    public class Presets
    {
        public bool InMove;
        public PresetItem MovedItem;
        public Folder Main;
        public MainWindow MainWindow;
        public List<StackPanel> StackPanels;
        public Presets(MainWindow window)
        {
            StackPanels = new List<StackPanel>();
            MainWindow = window;
            Main = new Folder(null, "Presets", this);
            GenerateAppData();
            SetPresets();
            GenerateStackPanels(Main);
            //StackPanels.Add(Main.GenerateItem(0));
            resizeStackPanels();
        }
        private void GenerateAppData()
        {
            string appdataPath = PresetItem.AppDataPath;
            if (!Directory.Exists(appdataPath))
            {
                Directory.CreateDirectory(appdataPath);
            }
            if (!Directory.Exists(appdataPath + "\\Presets"))
            {
                Directory.CreateDirectory(appdataPath + "\\Presets");
                Main.GenerateItemSettings();
            }
            if (!File.Exists(appdataPath + "\\Presets\\Presets.json"))
            {
                Main.GenerateItemSettings();
            }
        }
        private void SetPresets()
        {
            string json = File.ReadAllText(Main.getJsonPath());
            FolderItemSettings settings = JsonConvert.DeserializeObject<FolderItemSettings>(json);
            foreach (var item in settings.Children)
            {
                ReadPresetItem(item, Main);
            }
        }
        private void ReadPresetItem(string item, Folder parent)
        {
            string json = File.ReadAllText(PresetItem.getJsonPath(item));
            ItemSettings settings = JsonConvert.DeserializeObject<ItemSettings>(json);

            if (settings.Type == "Folder")
            {
                settings = JsonConvert.DeserializeObject<FolderItemSettings>(json);
                Folder f = new Folder(parent, settings.Name, this, settings.Path);
                f.IsExpanded = ((FolderItemSettings)settings).IsExpanded;
                parent.Presets.Add(f);

                foreach (var el in ((FolderItemSettings)settings).Children)
                {
                    ReadPresetItem(el, f);
                }
            }
            else if (settings.Type == "Preset")
            {
                Preset f = new Preset(parent, settings.Name, this);
                parent.Presets.Add(f);
            }
        }
        public void GenerateStackPanels(Folder baseFolder, bool repeat = false)
        {
            if (repeat)
            {
                MainWindow.ScrollViewerContent.Children.RemoveRange(0, StackPanels.Count());
                StackPanels.Clear();
            }
            //StackPanels.AddRange(baseFolder.Presets.Select(x => x.GenerateItem(GetNumberOfParents(x))).ToList());
            foreach (PresetItem item in baseFolder.Presets)
            {
                StackPanels.Add(item.GenerateItem());
                if (item.GetType() == typeof(Folder) && ((Folder)item).IsExpanded)
                {
                    GenerateStackPanels((Folder)item);
                }
            }
            resizeStackPanels();
        }
        public void GenerateStackPanels(int index, Folder baseFolder)
        {
            foreach (PresetItem item in baseFolder.Presets)
            {
                if (!StackPanels.Contains(item.StackPanelItem))
                {
                    StackPanels.Insert(index++, item.GenerateItem());
                    if (item.GetType() == typeof(Folder) && ((Folder)item).IsExpanded)
                    {
                        GenerateStackPanels(index, (Folder)item);
                    }
                }
            }
            resizeStackPanels();
        }
        public void RemoveStackPanels(Folder baseFolder)
        {
            int index = StackPanels.IndexOf(baseFolder.StackPanelItem) + 1;
            int amount = calcRemoveAmount(baseFolder);
            for (int i = index; i < index + amount; i++)
            {
                MainWindow.ScrollViewerContent.Children.Remove(StackPanels[i]);
            }
            StackPanels.RemoveRange(index, amount);
            resizeStackPanels();
        }
        public int calcRemoveAmount(Folder folder)
        {
            int i = folder.Presets.Count(x => x.GetType() != typeof(Folder));
            foreach (Folder item in folder.Presets.Where(x => x.GetType() == typeof(Folder)))
            {
                if (item.IsExpanded)
                {
                    i += calcRemoveAmount((Folder)item);
                }
                i++;
            }
            return i;
        }
        public void resizeStackPanels()
        {
            int i = 0;
            foreach (var item in StackPanels)
            {
                item.Margin = new Thickness(0, 0.1, 0, 0);
                if (!MainWindow.ScrollViewerContent.Children.Contains(item))
                    MainWindow.ScrollViewerContent.Children.Insert(i, item);
                i++;
                item.MouseEnter += MainWindow.PresetMouseEnter;
                item.MouseLeave += MainWindow.PresetMouseLeave;
            }
            //ScrollBar scrollBar = new ScrollBar();
            /*
            if (StackPanels.Count() > 21 && !MainWindow.ScrollViewerContent.Children.Contains(scrollBar))
            {
                MainWindow.ScrollViewerContent.Children.Add(scrollBar);
            }*/
        }
    }

}
