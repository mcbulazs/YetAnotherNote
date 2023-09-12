using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;


namespace YetAnotherNote
{
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
        public string Content;
    }
    class PresetItem
    {
        public string Name;
        public string Path;
        public Folder Parent;
        protected Presets Context;
        public StackPanel StackPanelItem;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\YetAnotherNote";
        public PresetItem(Folder parent, string name, Presets context)
        {
            this.Name = name;
            this.Parent = parent;
            if (parent != null)
            {
                this.Path = parent.Path + "\\" + StringToPascalCase(name);
            }
            else
            {
                this.Path = "\\Presets";
            }
            Context = context;
        }
        public virtual string getJsonPath()
        {
            return AppDataPath + this.Path + "\\" + this.Path.Substring(this.Path.LastIndexOf('\\') + 1) + ".json";
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
        public virtual StackPanel GenerateItem(int numberOfParents)
        {
            StackPanel item = new StackPanel();
            item.Height = 15;
            item.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            item.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            item.Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
            item.ClipToBounds = false;
            item.Orientation = Orientation.Horizontal;
            //Text
            TextBlock Text = new TextBlock();
            Text.Height = 15;
            Text.Width = 400;
            Text.Text = Name;
            Text.Foreground = new SolidColorBrush(Color.FromRgb(0, 192, 192));
            Text.Margin = new Thickness(0, 0, 0, 0);

            Text.LineHeight = 15;
            Text.FontSize = 11;
            Text.TextAlignment = System.Windows.TextAlignment.Left;
            Text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            Text.VerticalAlignment = System.Windows.VerticalAlignment.Center;


            item.Children.Add(Text);
            return item;
        }


    }
    class Preset : PresetItem
    {
        string Content;
        public Preset(Folder parent, string name, Presets context) : base(parent, name, context)
        {
            this.Path += ".json";
            if (!File.Exists(getJsonPath()))
                GenerateItemSettings();
        }
        public void GenerateItemSettings()
        {
            PresetItemSettings settings = new PresetItemSettings();
            settings.Type = "Folder";
            settings.Name = this.Name;
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
            settings.Content = this.Content;

             File.WriteAllText(getJsonPath(), JsonConvert.SerializeObject(settings));
        }
        public override StackPanel GenerateItem(int numberOfParents)
        {
            StackPanelItem = base.GenerateItem(numberOfParents);
            ((TextBlock)StackPanelItem.Children[0]).Margin = new Thickness(numberOfParents * 10 + 15, 0, 0, 0);
            return StackPanelItem;
        }
        public override string getJsonPath()
        {
            return AppDataPath + this.Path;
        }
    }
    class Folder : PresetItem
    {
        public List<PresetItem> Presets;
        public bool IsExpanded;
        public Folder(Folder parent, string name, Presets context) : base(parent, name, context)
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
            new singleLine("Create New Folder", CreateChildFolder).ShowDialog();
            
            //Context.GenerateStackPanels(Context.Main);
        }
        private void CreateChildFolder(object sender, string name)
        {
            Folder item = new Folder(this, name, this.Context);

            Presets.Add(item);
            int index = 0;
            if (contextMenuStackPanel!=null)
            {
                index = Context.StackPanels.IndexOf(contextMenuStackPanel);
                Context.GenerateStackPanels(index + 1, this);
                ((TextBlock)contextMenuStackPanel.Children[0]).RenderTransform = new RotateTransform(0);
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
            new singleLine("Create New Preset", CreateChildPreset).ShowDialog();
            
            //Context.GenerateStackPanels(Context.Main);
        }
        private void CreateChildPreset(object sender, string name)
        {
            Preset item = new Preset(this, name, this.Context);

            Presets.Add(item);
            int index = 0;
            if (contextMenuStackPanel!=null)
            {
                index = Context.StackPanels.IndexOf(contextMenuStackPanel);
                Context.GenerateStackPanels(index + 1, this);
                ((TextBlock)contextMenuStackPanel.Children[0]).RenderTransform = new RotateTransform(0);
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
        public override StackPanel GenerateItem(int numberOfParents)
        {
            StackPanelItem = base.GenerateItem(numberOfParents);
            //Collapse button
            TextBlock collapse = new TextBlock();
            collapse.Text = "▼";
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
            collapse.MouseDown += CollapseFolder;
            StackPanelItem.Children.Insert(0, collapse);

            //Context menu

            ContextMenu contextMenu= new ContextMenu();
            MenuItem newFolder = new MenuItem();
            newFolder.Header = "Create New Folder";
            newFolder.Click += CreateChildFolderMenu;
            contextMenu.Items.Add(newFolder);
            
            MenuItem newPreset = new MenuItem();
            newPreset.Header = "Create New Preset";
            newPreset.Click += CreateChildPresetMenu;
            contextMenu.Items.Add(newPreset);
            
            StackPanelItem.MouseRightButtonDown += savePanelOnContextOpen;
            StackPanelItem.ContextMenu= contextMenu;
            return StackPanelItem;
        }
        private StackPanel contextMenuStackPanel = null;
        private void savePanelOnContextOpen(object sender, MouseButtonEventArgs e)
        {
            contextMenuStackPanel = (StackPanel)sender;
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
                    IsExpanded= true;
                    UpdateItemSettings();
                }
                else//close
                {
                    ((TextBlock)sender).RenderTransform = new RotateTransform(30, 5, 5);
                    Context.RemoveStackPanels(this);
                    IsExpanded= false;
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
    class Presets
    {
        public Folder Main;
        MainWindow MainWindow;
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
        private int GetNumberOfParents(PresetItem item)
        {
            return item.Path.Count(x => x == '\\') - 1;
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
                Folder f = new Folder(parent, settings.Name, this);
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
        public void GenerateStackPanels(Folder baseFolder)
        {

            //StackPanels.AddRange(baseFolder.Presets.Select(x => x.GenerateItem(GetNumberOfParents(x))).ToList());
            foreach (PresetItem item in baseFolder.Presets)
            {
                StackPanels.Add(item.GenerateItem(GetNumberOfParents(item)));
                if (item.GetType()==typeof(Folder) && ((Folder)item).IsExpanded)
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
                if(!StackPanels.Contains(item.StackPanelItem))
                {
                    StackPanels.Insert(index++,item.GenerateItem(GetNumberOfParents(item)));
                    if (item.GetType() == typeof(Folder) && ((Folder)item).IsExpanded)
                    {
                        GenerateStackPanels(index,(Folder)item);
                    }
                }
            }
            resizeStackPanels();
        }
        public void RemoveStackPanels(Folder baseFolder)
        {
            int index = StackPanels.IndexOf(baseFolder.StackPanelItem) +1;
            int amount = calcRemoveAmount(baseFolder) ;
            for (int i = index; i < index+ amount; i++)
            {
                MainWindow.PresetsGrid.Children.Remove(StackPanels[i]);
            }
            StackPanels.RemoveRange(index, amount);
            resizeStackPanels();
        }
        private int calcRemoveAmount(Folder folder)
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
        private void resizeStackPanels()
        {
            int i = 0;
            foreach (var item in StackPanels)
            {
                item.Margin = new Thickness(0, i++ * 15 * 1.1, 0, 0);
                Grid.SetColumn(item, 0);
                if (!MainWindow.PresetsGrid.Children.Contains(item))
                    MainWindow.PresetsGrid.Children.Add(item);
                item.MouseEnter += MainWindow.PresetMouseEnter;
                item.MouseLeave += MainWindow.PresetMouseLeave;
            }
        }
    }

}
