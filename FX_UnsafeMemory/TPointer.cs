using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FX_UnsafeMemory
{
    public struct TPointer<T> 
    {
        public TPointer(nint address, T value) { Address = address; Value = value; }
        public nint Address;
        public T Value;

        public static bool operator !=(TPointer<T> a, TPointer<T> b) => a.Address != b.Address;
        public static bool operator ==(TPointer<T> a, TPointer<T> b) => a.Address == b.Address;

        public override bool Equals(object? tptr)
        {
            return ((TPointer<object>)tptr).Address == Address;
        }
    }
}
