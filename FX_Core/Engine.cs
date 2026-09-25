using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public static class Engine
    {
        public static event Action<bool>? OnAttachChange;

        public static ProcessAnalyzer? AttachedProcess { get; private set; }

        public static ProcessAnalyzer AttachToProcess(Process process)
        {
            AttachedProcess = new ProcessAnalyzer(process);
            OnAttachChange?.Invoke(true);
            Shared.Print($"Attached to {process.ProcessName}!", Shared.PrintType.Success);
            return AttachedProcess;
        }

        public static void Detach()
        {
            AttachedProcess = null;
            OnAttachChange?.Invoke(false);
        }
    }
}
