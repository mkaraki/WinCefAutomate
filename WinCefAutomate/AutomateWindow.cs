using CefSharp;
using CefSharp.WinForms;
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
    public partial class AutomateWindow : Form
    {
        internal Script ExecScr { private get; set; }

        private ChromiumWebBrowser cef;

        public AutomateWindow()
        {
            InitializeComponent();

            CefSettings settings = new CefSettings
            {
                CachePath = Environment.CurrentDirectory + @"\CEF"
            };
            Cef.Initialize(settings);

            cef = new ChromiumWebBrowser("about:blank");
            splitContainer1.Panel2.Controls.Add(cef);
            cef.Dock = DockStyle.Fill;
        }

        internal string[,] Dataset { private get; set; }

        private int DatasetRow = 0;

        private Logger Log;

        private void AutomateWindow_Shown(object sender, EventArgs e)
        {
            // Initialize Logging System
            Log = new Logger(Logger.GenerateDefaultLogName(ExecScr.FileName));

            // Initial DataSet Progress bar
            pbar_dataset_current.Maximum = Dataset.Length;
            pbar_dataset_current.Value = 0;

            // Initial Step Viewer
            foreach (var n in ExecScr.Steps.Select(v => v.Name))
            {
                lbox_proc.Items.Add(n);
            }
        }

        private async Task<int> RunScriptAsync(int current, Step scriptstep)
        {
            Invoke((Action)delegate { lbox_proc.SelectedIndex = current; });
            switch (scriptstep.EnumAction)
            {
                case ScriptAction.Go:
                    Go(scriptstep);
                    break;

                case ScriptAction.WaitUser:
                    WaitUser();
                    break;

                case ScriptAction.Goto:
                    return scriptstep.Id;

                case ScriptAction.EvalGoto:
                    if ((bool)await EvalJSAsync(scriptstep.What) == true)
                    {
                        if (scriptstep.Logres) Log.Log(Logger.GenerateLogString("Runner", "INFO", "Eval was true", current, DatasetRow));
                        return scriptstep.Id;
                    }
                    else if (scriptstep.Logres) Log.Log(Logger.GenerateLogString("Runner", "INFO", "Eval was false", current, DatasetRow));
                    break;

                case ScriptAction.Click:
                    cef.GetMainFrame().ExecuteJavaScriptAsync($"document.querySelector('{scriptstep.Where}').click();");
                    break;

                case ScriptAction.SetVal:
                    cef.GetMainFrame().ExecuteJavaScriptAsync(
                        $"document.querySelector('{scriptstep.Where}').value = \"{StrVarProc(scriptstep.What).Replace("\"", "\\\"")}\";");
                    break;

                case ScriptAction.Shift:
                    DatasetRow++;
                    if (DatasetRow >= Dataset.GetLength(0))
                    { 
                        StopProcess = true;
                        break;
                    }
                    Invoke((Action)delegate { pbar_dataset_current.Value = DatasetRow + 1; });
                    break;

                case ScriptAction.Sleep:
                    System.Threading.Thread.Sleep(scriptstep.Time * 1000);
                    break;

                case ScriptAction.LogStr:
                    Log.Log(Logger.GenerateLogString(
                        "Script Autor Log", "INFO", scriptstep.Message
                        , current, DatasetRow));
                    break;

                case ScriptAction.LogEval:
                    Log.Log(Logger.GenerateLogString(
                        "Script Autor Log", "INFO", 
                        $"{scriptstep.Message ?? ""} {await EvalJSAsync(scriptstep.What)}".TrimStart(' ')
                        , current, DatasetRow));
                    break;

                default:
                    throw new Exception();
            }

            return current + 1;
        }

        private async Task<object> EvalJSAsync(string script)
        {
            var res = await cef.GetMainFrame().EvaluateScriptAsync(script);
            if (!res.Success)
                return "Script Error: " + res.Message;
            else
                return res.Result;
        }

        private bool WaitingBtn = false;

        private void WaitUser()
        {
            if (InvokeRequired) Invoke((Action)delegate { WaitUserDisp(true); });

            do System.Threading.Thread.Sleep(500);
            while (!WaitingBtn);

            if (InvokeRequired) Invoke((Action)delegate { WaitUserDisp(false); });
        }

        private void WaitUserDisp(bool wait)
        {
            lbl_status.Text = wait ? "Waiting" : "Processing";

            btn_continue.Enabled = wait;
            WaitingBtn = !wait;
        }

        private string StrVarProc(string orig)
        {
            for (int i = 0; i < Dataset.GetLength(1); i++)
                orig = orig.Replace($"%%dataset:{i}%%", Dataset[DatasetRow, i]);
            
            return orig;
        }

        private void Go(Step scriptstep)
        {
            cef.Load(StrVarProc(scriptstep.Where));
            
            do System.Threading.Thread.Sleep(500);
            while (cef.IsLoading);
        }

        private bool BtnStartMode = true;

        private bool StopProcess = false;

        private void btn_continue_Click(object sender, EventArgs e)
        {
            if (BtnStartMode)
            {
                BtnStartMode = false;
                btn_continue.Enabled = false;
                btn_continue.Text = "Continue";
                pbar_dataset_current.Value = 0;
                Task.Run(async () =>
                {
                    for (int i = 0; i < ExecScr.Steps.Length;)
                    {
                        if (StopProcess)
                            break;
                        i = await RunScriptAsync(i, ExecScr.Steps[i]);
                    }

                    Invoke((Action)delegate { lbl_status.Text = "Finished"; });
                    Log.Log(Logger.GenerateLogString("Runner", "INFO", "Job finished"));
                });
            }
            WaitingBtn = true;
        }
    }
}
