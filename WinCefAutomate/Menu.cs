using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using CefSharp.WinForms;
using CefSharp;

namespace WinCefAutomate
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private Dictionary<string, Script> Scripts = new Dictionary<string, Script>();

        private void Menu_Shown(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings
            {
                CachePath = Environment.CurrentDirectory + @"\CEF"
            };
            Cef.Initialize(settings);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            foreach (var f in Directory.GetFiles("scripts", "*.yaml"))
            {
                string yaml = File.ReadAllText(f, Encoding.UTF8);
                var ymlobj = deserializer.Deserialize<Script>(yaml);
                ymlobj.FileName = Path.GetFileNameWithoutExtension(f);
                Scripts.Add(f, ymlobj);
            }

            foreach (var scr in Scripts)
            {
                lbox_scripts.Items.Add($"{scr.Value.Name} ({scr.Key})");
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            ScriptLoadingScreen loadscr = new ScriptLoadingScreen(Scripts.ToArray()[lbox_scripts.SelectedIndex].Value);
            loadscr.ShowDialog();

            if (!loadscr.Success) return;

            AutomateWindow awind = new AutomateWindow() {
                ExecScr = loadscr.Processor.script,
                Dataset = loadscr.Processor.Dataset,
            };
            awind.ShowDialog();

            loadscr.Dispose();
            awind.Dispose();
        }
    }
}
