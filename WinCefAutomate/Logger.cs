using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinCefAutomate
{
    internal class Logger
    {
        public string LogFilePath { get; private set; }

        public Logger(string logfilename, string basedir = null)
        {
            if (basedir == null) basedir = Path.Combine(Environment.CurrentDirectory, "log");

            if (!Directory.Exists(basedir))
            {
                try
                {
                    Directory.CreateDirectory(basedir);
                }
                catch
                {
                    throw new Exception("Failed to prepare log directory");
                }
            }

            LogFilePath = Path.Combine(basedir, logfilename);

            File.WriteAllText(LogFilePath, GenerateLogString("WinCefAutomate-Logger", "INFO", "Logging service started"));
        }

        public void Log(string message)
        {
            File.AppendAllText(LogFilePath, Environment.NewLine + message);
        }

        private static string ConvertLineDataIntoStringNumber(int line)
        {
            if (line == 0)
                return "-";
            else
                return line.ToString();
        }

        public static string GenerateLogString(string application, string loglevel, string message, int line = -1, int datasetline = -1)
        {
            line++; datasetline++;
            return $"[l{ConvertLineDataIntoStringNumber(line)} d{ConvertLineDataIntoStringNumber(datasetline)}]{application}: [{loglevel}] {message.Replace("\n", "\\n").Replace("\r", "\\r")}";
        }

        public static string GenerateDefaultLogName(string scriptname = "default")
        {
            string nowstr = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"{nowstr}-{scriptname}.log";
        }
    }
}
