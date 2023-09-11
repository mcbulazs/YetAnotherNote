using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
    }
    class PresetItem
    {
        public string Name;
        public string Path;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\YetAnotherNote";
        public PresetItem(Folder parent, string name) 
        {
            this.Name = name;
            if (parent != null)
            {
                this.Path = parent.Path+"\\"+ StringToPascalCase(name);
            }
            else
            {
                this.Path = "\\Presets";
            }
        }
        public string getJsonPath()
        {
            return AppDataPath + this.Path + "\\" + this.Path.Substring(this.Path.LastIndexOf('\\') + 1) + ".json";
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
            string output="";
            foreach (string e in resultBuilder.ToString().Split(' ').ToList().Select(x => x[0].ToString().ToUpper() + x.Substring(1)))
                output += e;
            return output;
        }
        public TextBlock GenerateItem()
        {
            TextBlock item = new TextBlock();
            item.Text = Name;
            item.Height = 15;
            item.Width = 200;
            item.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            item.VerticalAlignment= System.Windows.VerticalAlignment.Top;
            item.TextAlignment = System.Windows.TextAlignment.Left;

            TextBlock collapse = new TextBlock();
            collapse.Text = "∟";
            collapse.RenderTransform = new RotateTransform(-135);

            collapse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            collapse.VerticalAlignment= System.Windows.VerticalAlignment.Center;
            item.Inlines.Add(collapse);

            return item;
        }
        
    }
    class Preset : PresetItem
    {
        public Preset(Folder parent, string name) : base(parent, name)
        {

        }
        public void GenerateItemSettings()
        {
            /*FolderItemSettings settings = new FolderItemSettings();
            settings.Type = "Folder";
            settings.Name = this.Name;
            settings.Children = new List<string>();

            string json = JsonConvert.SerializeObject(settings);
            string path = getJsonPath();
            File.WriteAllText(path, json);*/
        }
        public void UpdateItemSettings()
        {
           /* string json = File.ReadAllText(getJsonPath());
            FolderItemSettings settings = JsonConvert.DeserializeObject<FolderItemSettings>(json);
            settings.Name = this.Name;
            settings.Children = this.Presets.Select(x => x.Path).ToList();

            File.WriteAllText(getJsonPath(), JsonConvert.SerializeObject(settings));*/
        }
    }
    class Folder : PresetItem
    {
        List<PresetItem> Presets;

        public Folder(Folder parent, string name) : base(parent, name)
        {
            Presets = new List<PresetItem>();
            if (!Directory.Exists(getWholePath()))
            {
                Directory.CreateDirectory(getWholePath());
                GenerateItemSettings();
            }
            if(!File.Exists(getJsonPath()))
                GenerateItemSettings();
            else
                UpdateItemSettings();
        }

        public void GenerateItemSettings()
        {
            FolderItemSettings settings = new FolderItemSettings();
            settings.Type = "Folder";
            settings.Name = this.Name;
            settings.Children = new List<string>();
            settings.Path= this.Path;

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

            File.WriteAllText(getJsonPath(), JsonConvert.SerializeObject(settings));
        }
        public void CreateChildFolder(string Name)
        {
            Folder item = new Folder(this, Name);
            Presets.Add(item);
            UpdateItemSettings();
        }
        public void CreateChildPreset(string Name)
        {
            Preset item = new Preset(this, Name);
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
            this.Name= Name;
            UpdateItemSettings();
        }
    }
    class Presets
    {
        Folder Main;
        public Presets()
        {
            Main = new Folder(null,"Presets");
            GenerateAppData();
            Main.CreateChildFolder("csao");
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
            return item.Path.Count(x => x == '\\');
        }
        private void setPresets()
        { 
        
        }

    }

}
