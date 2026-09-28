using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_UnsafeMemory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FX_Core
{
    public struct FloatPointer
    {
        public static FloatPointer[] FromDictionary(Dictionary<nint, float> dict) 
        {
            List<FloatPointer> pList = new();
            foreach (var p in dict)
            {
                pList.Add(new FloatPointer(p.Key));
            }
            return pList.ToArray();
        }

        public nint Address;
        public float Value
        {
            get { return Engine.AttachedProcess.memory.ReadFloat(Address); }
            set { Engine.AttachedProcess.memory.WriteFloat(Address, value); }
        }

        public FloatPointer(nint address)
        {
            Address = address;
        }

        #region Implicit Conversion Operators
        // Permite usar FloatPointer como float directamente (ej: float v = myFloatPtr;)
        public static implicit operator float(FloatPointer ptr) => ptr.Value;
        #endregion

        #region Arithmetic Operators (FloatPointer & float)
        public static float operator +(FloatPointer a, float b) => a.Value + b;
        public static float operator +(float a, FloatPointer b) => a + b.Value;
        public static float operator +(FloatPointer a, FloatPointer b) => a.Value + b.Value;

        public static float operator -(FloatPointer a, float b) => a.Value - b;
        public static float operator -(float a, FloatPointer b) => a - b.Value;
        public static float operator -(FloatPointer a, FloatPointer b) => a.Value - b.Value;

        public static float operator *(FloatPointer a, float b) => a.Value * b;
        public static float operator *(float a, FloatPointer b) => a * b.Value;
        public static float operator *(FloatPointer a, FloatPointer b) => a.Value * b.Value;

        public static float operator /(FloatPointer a, float b) => a.Value / b;
        public static float operator /(float a, FloatPointer b) => a / b.Value;
        public static float operator /(FloatPointer a, FloatPointer b) => a.Value / b.Value;

        public static float operator %(FloatPointer a, float b) => a.Value % b;
        public static float operator %(float a, FloatPointer b) => a % b.Value;
        public static float operator %(FloatPointer a, FloatPointer b) => a.Value % b.Value;

        public static float operator -(FloatPointer a) => -a.Value;
        public static float operator +(FloatPointer a) => +a.Value;
        #endregion

        #region Increment / Decrement
        // Nota: Modifican el valor en la memoria remota y devuelven la estructura
        public static FloatPointer operator ++(FloatPointer a)
        {
            a.Value += 1f;
            return a;
        }

        public static FloatPointer operator --(FloatPointer a)
        {
            a.Value -= 1f;
            return a;
        }
        #endregion

        #region Comparison Operators
        public static bool operator ==(FloatPointer a, float b) => a.Value == b;
        public static bool operator ==(float a, FloatPointer b) => a == b.Value;
        public static bool operator ==(FloatPointer a, FloatPointer b) => a.Value == b.Value;

        public static bool operator !=(FloatPointer a, float b) => a.Value != b;
        public static bool operator !=(float a, FloatPointer b) => a != b.Value;
        public static bool operator !=(FloatPointer a, FloatPointer b) => a.Value != b.Value;

        public static bool operator >(FloatPointer a, float b) => a.Value > b;
        public static bool operator >(float a, FloatPointer b) => a > b.Value;
        public static bool operator >(FloatPointer a, FloatPointer b) => a.Value > b.Value;

        public static bool operator <(FloatPointer a, float b) => a.Value < b;
        public static bool operator <(float a, FloatPointer b) => a < b.Value;
        public static bool operator <(FloatPointer a, FloatPointer b) => a.Value < b.Value;

        public static bool operator >=(FloatPointer a, float b) => a.Value >= b;
        public static bool operator >=(float a, FloatPointer b) => a >= b.Value;
        public static bool operator >=(FloatPointer a, FloatPointer b) => a.Value >= b.Value;

        public static bool operator <=(FloatPointer a, float b) => a.Value <= b;
        public static bool operator <=(float a, FloatPointer b) => a <= b.Value;
        public static bool operator <=(FloatPointer a, FloatPointer b) => a.Value <= b.Value;
        #endregion

        #region Equals & GetHashCode Overrides
        public override bool Equals(object? obj)
        {
            if (obj is FloatPointer ptr) return Value == ptr.Value;
            if (obj is float f) return Value == f;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
        #endregion
    }
}
