using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public class Scanner
    {
        Memory MEM;

        public Scanner(Process process) { MEM = new Memory(process); }

        public bool isProcessValid() { return MEM != null && MEM.isProcessValid(); }
        
        public Process Process() { return MEM.getProcess(); }

        public Memory Memory() { return MEM; }

        public List<IntPtr> Find360()
        {
            return MEM.ScanFloatRange(-360, 360);
        }

    }
}