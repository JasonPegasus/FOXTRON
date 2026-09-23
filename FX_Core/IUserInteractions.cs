using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public interface IUserInteractions
    {
        int SelectList(string question, IReadOnlyList<string> options);
    }
}
