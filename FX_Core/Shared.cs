using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public class Shared
    {
        public static Random random = new Random();

        public static string PtrStr(IntPtr ptr) { return "0x" + ptr.ToString("X"); }

        //public static int FilterPointerDictionary(ref Dictionary<IntPtr, float> dictionary, Predicate<float> filter)
        //{
        //    int removed = 0;
        //    foreach (var p in dictionary)
        //    {
        //        if (filter(p.Value))
        //        { dictionary.Remove(p.Key); removed++; }
        //    }
        //    return removed;
        //}


        public static event Action<string, PrintType>? OnPrint;

        public static string CurrentDate()
        { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); }

        public static void Print(string text, PrintType pType = 0)
        {
            string fullLog = $"[{CurrentDate()}] {text}";
            OnPrint?.Invoke(fullLog, pType);
        }

        public enum PrintType { Log, Warn, Error, FatalError, Success }

        public static readonly Dictionary<PrintType, Color> PrintTypeColors = new()
        {
            { PrintType.Log, Color.White },
            { PrintType.Warn, Color.Gold },
            { PrintType.Error, Color.FromArgb(250, 100, 100) },
            { PrintType.FatalError, Color.FromArgb(200, 0, 200) },
            { PrintType.Success, Color.Green },
        };
    }
}
