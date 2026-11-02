using System.Drawing;

namespace FX_Core
{
    public class Shared
    {
        public static bool isBetween(float num, float min, float max) { return num >= min && num <= max; }

        public static Random random = new Random();

        public static string PtrStr(IntPtr ptr) { return "0x" + ptr.ToString("X"); }

        public static nint StringToPtr(string str) { return (nint)Convert.ToInt64(str, 16); }

        public static Exception FormatError(string text, Exception exception)
        { return new Exception($">>> {text}:\n         {exception.Message}\n\n"); }

        public static event Action<string, PrintType>? OnPrint;

        public static string CurrentDate()
        { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); }

        public static void Print(Exception ex) { Print(ex.Message, PrintType.Exception); }
        public static void Print(string text, PrintType pType = 0)
        {
            string fullLog = $"[{CurrentDate()}] {text}";
            OnPrint?.Invoke(fullLog, pType);
        }

        public enum PrintType { Log, Warn, Error, Exception, Success, Info }

        public static readonly Dictionary<PrintType, Color> PrintTypeColors = new()
        {
            { PrintType.Log, Color.White },
            { PrintType.Warn, Color.Gold },
            { PrintType.Error, Color.FromArgb(250, 100, 100) },
            { PrintType.Exception, Color.FromArgb(200, 0, 200) },
            { PrintType.Success, Color.LightGreen },
            { PrintType.Info, Color.Cyan },
        };
    }
}
