using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    internal class AttachedProcess
    {
        public Process PROCESS { get; private set; }

        public bool isProcessValid() { return PROCESS is not null && !PROCESS.HasExited; }

        public AttachedProcess(Process proc) 
        {
            PROCESS = proc;
        }

        public T Scan<T>() where T : Scanner, new()
        {
            return new T();
        }
    }
}
