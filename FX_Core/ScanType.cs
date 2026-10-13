using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        protected ScanType(ProcessAnalyzer analyzedProcess)
        {
            aProc = analyzedProcess;
            MEM = aProc.memory;
        }

        public static ScanTypeDataAttribute GetData(Type scanType)
        { return scanType.GetCustomAttribute<ScanTypeDataAttribute>(); }

        protected void Pause(bool p) { ProcessManager.SetPauseProcess(aProc.process, p); }

        public abstract FloatVariable[] ReturnPossiblePointers();

        public event Action<int>? OnProgress;

        int _scanProgress = 0;
        protected int scanProgress
        {
            get { return _scanProgress; }
            set { _scanProgress = value; OnProgress?.Invoke(value); }
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
}
