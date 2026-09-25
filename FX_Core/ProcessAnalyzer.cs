using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public sealed class ProcessAnalyzer
    {
        public Process process;

        internal ProcessAnalyzer(Process process)
        {
            this.process = process;
        }

        public bool isValid()
        { return ProcessManager.isProcessValid(process); }
    }
}
