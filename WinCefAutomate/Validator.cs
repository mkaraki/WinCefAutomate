using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCefAutomate
{
    internal class Validator
    {
        public static bool CheckRequireOption(string opt, bool useException = false)
        {
            if (opt == null)
            { 
                if (useException) throw new Exception();
                else return false;
            }
            else
                return true;
        }

        public static bool CheckRequireOption(int opt, int? min, int? max, bool useExeption = false)
        {
            if (min == null) min = int.MinValue;
            if (max == null) max = int.MaxValue;

            if (opt >= min && opt <= max)
                return true;
            else
            { 
                if (useExeption) throw new Exception();
                else return false;
            }
        }
    }
}
