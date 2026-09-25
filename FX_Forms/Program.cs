using FX_Core;

namespace FX_Forms
{
    internal static class Program
    {
        public static readonly string PRODUCT_NAME = "FOXTRON";

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new F_Main());
        }
    }
}