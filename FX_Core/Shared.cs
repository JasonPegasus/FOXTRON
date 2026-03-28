using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public class Shared
    {
        public static Random random = new Random();

        public enum LogType { Info, Warning, Error }
        public static void Log(string text, LogType type = LogType.Info)
        { Console.WriteLine($"[{DateTime.Now}] [{type.ToString().ToUpper()}] {text}"); }


        public static int FilterPointerDictionary(ref Dictionary<IntPtr, float> dictionary, Predicate<float> filter)
        {
            int removed = 0;
            foreach (var p in dictionary)
            {
                if (filter(p.Value))
                { dictionary.Remove(p.Key); removed++; }
            }
            return removed;
        }
    }
}
