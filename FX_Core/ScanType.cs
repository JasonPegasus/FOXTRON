using System.Reflection;
using FX_UnsafeMemory;

namespace FX_Core
{
    public abstract class ScanType
    {
        public static Dictionary<string, Type> ScansList { get; protected set; } =
            Assembly.GetAssembly(typeof(ScanType)).GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ScanType)))
            .ToDictionary(c => ScanType.GetData(c).Name, c => c);

        protected ProcessAnalyzer aProc;
        protected Memory MEM;

        protected int frameMilliseconds;

        protected ScanType(ProcessAnalyzer analyzedProcess)
        {
            aProc = analyzedProcess;
            MEM = aProc.memory;
            frameMilliseconds = (int)(1000*(1d/aProc.settings.GameFPS));
        }

        public static ScanTypeDataAttribute GetData(Type scanType)
        { return scanType.GetCustomAttribute<ScanTypeDataAttribute>(); }

        protected void Wait(int frameCount)
        {
            Thread.Sleep(frameMilliseconds*frameCount);
        }

        protected void Pause(bool p) 
        {
            if (!aProc.settings.PauseWhileScanning) return;
            ProcessManager.SetPauseProcess(aProc.process, p); 
        }

        protected void Foreground()
        { ProcessManager.SetForegroundWindow(aProc.process.MainWindowHandle); }

        public abstract FloatVariable[] ReturnPossiblePointers();

        public event Action<int>? OnProgress;

        int _scanProgress = 0;
        protected int scanProgress
        {
            get { return _scanProgress; }
            set { _scanProgress = value; OnProgress?.Invoke(value); }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ScanTypeDataAttribute : Attribute
    {
        public string Name { get; }
        public Type[] RequiredScans { get; }

        public ScanTypeDataAttribute(string name, Type[] requiredScans = null)
        {
            Name = name;
            RequiredScans = requiredScans;
        }
    }
}
