using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FX_UnsafeMemory;

namespace FX_Core
{
    internal abstract class Scanner
    {
        /////////////////////////////// PUBLIC STUFF ///////////////////////////////
        
        protected Memory MEM;
        protected bool pauseScan;

        public Scanner(Process process, bool pauseWhileScanning = false)
        { 
            pauseScan = pauseWhileScanning;
            MEM = new Memory(process); 
        }

        public float GetFloat(IntPtr ptr) { return MEM.ReadFloat(ptr); }
        public void SetFloat(IntPtr ptr, float value) { MEM.WriteFloat(ptr, value); }

        public bool isProcessValid() { return MEM != null && MEM.isProcessValid(); }
        
        public Process Process() { return MEM.getProcess(); }

        public Memory Memory() { return MEM; }

        protected bool Between(float num, float min, float max) { return num >= min && num <= max; }

        protected void Pause(bool p) 
        {
            if (!pauseScan) { return; }
            ProcessManager.SetPauseProcess(Process(), p); 
        }

        public abstract List<IntPtr> GetPossibleValues();
    }
}