using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCefAutomate
{
    internal class ScriptProcessor
    {
        public ScriptProcessor(Script scr)
        {
            script = scr;
            parsedSteps = new Step[script.Steps.Length];
        }
        
        public Script script { get; private set; }

        public string[,] Dataset { get; private set; }

        private Step[] parsedSteps;

        public void ProcessScriptLine(int l)
        {
            var ns = script.Steps[l];

            if (ns.IsParsed) goto End;

            ns.EnumAction = ConvertToScriptAction(ns.Action.ToLower().Trim(' ')) ?? throw new ScriptException($"No Action {ns.Action} available");

            // Convert Script Goto Id to Actual script line number.
            if (ns.EnumAction == ScriptAction.Goto || ns.EnumAction == ScriptAction.EvalGoto)
            {
                if (!Validator.CheckRequireOption(ns.Id, 0, null))
                    throw new ScriptException($"Goto id is not valid");
                ns.Id = GetGotoActualLine(ns.Id);
            }

            switch (ns.EnumAction)
            {
                case ScriptAction.EvalGoto:
                    if (!Validator.CheckRequireOption(ns.What)) throw new ScriptException("`what` is not valid");
                    break;

                case ScriptAction.Go:
                case ScriptAction.Click:
                    if (!Validator.CheckRequireOption(ns.Where)) throw new ScriptException("`where` is not valid");
                    break;

                case ScriptAction.SetVal:
                    if (!Validator.CheckRequireOption(ns.Where)) throw new ScriptException("`where` is not valid");
                    if (!Validator.CheckRequireOption(ns.What)) throw new ScriptException("`what` is not valid");
                    break;

                case ScriptAction.Sleep:
                    if (!Validator.CheckRequireOption(ns.Time, 1, null)) throw new ScriptException("`time` is missing or not valid (1 <= time)");
                    break;

                case ScriptAction.LogStr:
                    if (!Validator.CheckRequireOption(ns.Message)) throw new ScriptException("`message` is not valid");
                    break;

                case ScriptAction.LogEval:
                    if (!Validator.CheckRequireOption(ns.What)) throw new ScriptException("`what` is not valid");
                    break;
            }

            ns.IsParsed = true;

        End:
            parsedSteps[l] = ns;
        }

        public async Task ReadDatasetAsync()
        {
            script.Dataset.File = "scripts/" + script.Dataset.File;

            if (script.Dataset.File != null && System.IO.File.Exists(script.Dataset.File))
            {
                if (script.Dataset.Type == "plain")
                {
                    string[] d = await Task.Run (() => System.IO.File.ReadAllLines(script.Dataset.File));
                    Dataset = new string[d.Length, 1];
                    for (int i = 0; i < d.Length; i++)
                    {
                        Dataset[i, 0] = d[i];
                    }
                }
            }
        }

        private int GetGotoActualLine(int gotoid)
        {
            // Search Step which has same GotoId
            var targ = script.Steps.Where(v => v.Gotoid == gotoid).FirstOrDefault();

            // If not exists, throw exception.
            if (targ == null)
                throw new ScriptException($"Goto id {gotoid} not found");

            // Convert detected item to index id.
            int ind = Array.IndexOf(script.Steps, targ);
            return ind;
        }

        private static ScriptAction? ConvertToScriptAction(string stringAction)
        {
            switch (stringAction)
            {
                case "go":
                    return ScriptAction.Go;

                case "waituser":
                    return ScriptAction.WaitUser;

                case "goto":
                    return ScriptAction.Goto;

                case "evalgoto":
                    return ScriptAction.EvalGoto;

                case "click":
                    return ScriptAction.Click;

                case "setval":
                    return ScriptAction.SetVal;

                case "shift":
                    return ScriptAction.Shift;

                case "sleep":
                    return ScriptAction.Sleep;

                case "logstr":
                    return ScriptAction.LogStr;

                case "logeval":
                    return ScriptAction.LogEval;

                default:
                    return null;
            }
        }
    }

    public class ScriptException : Exception
    {
        public ScriptException() : base() { }

        public ScriptException(string message) : base(message) { }
    }
}
