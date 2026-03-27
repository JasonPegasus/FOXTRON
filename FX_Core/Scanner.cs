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
        /////////////////////////////// PUBLIC STUFF ///////////////////////////////
        
        Memory MEM;

        public Scanner(Process process) { MEM = new Memory(process); }

        public bool isProcessValid() { return MEM != null && MEM.isProcessValid(); }
        
        public Process Process() { return MEM.getProcess(); }

        public Memory Memory() { return MEM; }

        /////////////////////////////// SCANNING METHODS ///////////////////////////////

        bool Between(float num, float min, float max) { return (num >= min && num <= max); }

        public List<IntPtr> Find360()
        {
            return MEM.ScanFloatFiltered(e => float.Round(e) != 0 && Between(e, -360, 360));
        }


    }
}