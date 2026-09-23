using FX_Core;

namespace FX_Forms
{
    internal static class Program
    {
        static Core core = new Core();
        
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new F_Main());
        }
    }
}