using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_Core;

namespace FX_Console
{
    internal class IConsoleInteractions : IUserInteractions
    {
        public int SelectList(string question, IReadOnlyList<string> options)
        {
            //ConsUtils.print(question, ConsUtils.subtitleColor);
            //for (int i = 0; i < options.Count - 1; i++) 
            //{
            //    ConsUtils.print($"{i}: ");
            //}
            return 0;
        }
    }
}
