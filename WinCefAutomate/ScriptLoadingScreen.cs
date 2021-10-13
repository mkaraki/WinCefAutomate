using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCefAutomate
{
    public partial class ScriptLoadingScreen : Form
    {
        internal bool Success = false;

        internal ScriptProcessor Processor;

        public ScriptLoadingScreen(Script scr)
        {
            InitializeComponent();

            pbar_status.Minimum = 0;
            pbar_status.Maximum = scr.Steps.Length + 2;

            Processor = new ScriptProcessor(scr);
        }

        private async void ScriptLoadingScreen_Shown(object sender, EventArgs e)
        {
            lbl_status.Text = "Processing Scripts: line 1";
            for (int i = 0; i < Processor.script.Steps.Length; i++)
            {
                pbar_status.Value = i + 1;

                lbl_status.Text = $"Processing Scripts: line {i + 1}";
                await Task.Run(() => {
                    try
                    {
                        Processor.ProcessScriptLine(i);
                    }
                    catch (ScriptException ex)
                    { 
                        MessageBox.Show($"Error on Script: {ex.Message}{Environment.NewLine}{Environment.NewLine}Aborted.");
                        if (InvokeRequired)
                            Invoke((Action)delegate { Close(); });
                    }
                });
            }

            lbl_status.Text = "Reading DataSet";
            pbar_status.Value++;
            await Processor.ReadDatasetAsync();

            Success = true;

            Close();
        }
    }
}
