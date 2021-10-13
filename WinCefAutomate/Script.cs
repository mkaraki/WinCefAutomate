using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCefAutomate
{
    public class Script
    {
        public string FileName { get; set; }

        public string Name { get; set; } = "Untitled";

        public Dataset Dataset { get; set; }

        public Step[] Steps { get; set; }
    }

    public class Dataset
    {
        public string File { get; set; }

        public string Type { get; set; }
    }

    public class Step
    {
        public bool IsParsed { get; set; } = false;

        public ScriptAction EnumAction { get; set; }

        public string Name { get; set; } = "Untitled";

        public int Gotoid { get; set; } = -1;

        public int Id { get; set; } = -1;

        public string Action { get; set; } = null;

        public string Where { get; set; } = null;

        public string What { get; set; } = null;

        public int Time { get; set; } = -1;

        public string Message { get; set; } = null;

        public bool Logres { get; set; } = false;
    }

    public enum ScriptAction { 
        Go,
        WaitUser,
        Goto,
        EvalGoto,
        Click,
        SetVal,
        Shift,
        Sleep,
        LogStr,
        LogEval,
    }
}
