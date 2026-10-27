using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_UnsafeMemory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FX_Core
{
    public struct FloatVariable
    {
        public static FloatVariable[] FromDictionary(Dictionary<nint, float> dict) 
        {
            List<FloatVariable> pList = new();
            foreach (var p in dict)
            {
                pList.Add(new FloatVariable(p.Key));
            }
            return pList.ToArray();
        }

        public nint Address;
        public float Value
        {
            get { return Engine.AttachedProcess.memory.ReadFloat(Address); }
            set { Engine.AttachedProcess.memory.WriteFloat(Address, value); }
        }

        public FloatVariable(nint address)
        {
            Address = address;
        }

        #region Implicit Conversion Operators
        // Permite usar FloatPointer como float directamente (ej: float v = myFloatPtr;)
        public static implicit operator float(FloatVariable ptr) => ptr.Value;
        #endregion

        #region Arithmetic Operators (FloatPointer & float)
        public static float operator +(FloatVariable a, float b) => a.Value + b;
        public static float operator +(float a, FloatVariable b) => a + b.Value;
        public static float operator +(FloatVariable a, FloatVariable b) => a.Value + b.Value;

        public static float operator -(FloatVariable a, float b) => a.Value - b;
        public static float operator -(float a, FloatVariable b) => a - b.Value;
        public static float operator -(FloatVariable a, FloatVariable b) => a.Value - b.Value;

        public static float operator *(FloatVariable a, float b) => a.Value * b;
        public static float operator *(float a, FloatVariable b) => a * b.Value;
        public static float operator *(FloatVariable a, FloatVariable b) => a.Value * b.Value;

        public static float operator /(FloatVariable a, float b) => a.Value / b;
        public static float operator /(float a, FloatVariable b) => a / b.Value;
        public static float operator /(FloatVariable a, FloatVariable b) => a.Value / b.Value;

        public static float operator %(FloatVariable a, float b) => a.Value % b;
        public static float operator %(float a, FloatVariable b) => a % b.Value;
        public static float operator %(FloatVariable a, FloatVariable b) => a.Value % b.Value;

        public static float operator -(FloatVariable a) => -a.Value;
        public static float operator +(FloatVariable a) => +a.Value;
        #endregion

        #region Increment / Decrement
        // Nota: Modifican el valor en la memoria remota y devuelven la estructura
        public static FloatVariable operator ++(FloatVariable a)
        {
            a.Value += 1f;
            return a;
        }

        public static FloatVariable operator --(FloatVariable a)
        {
            a.Value -= 1f;
            return a;
        }
        #endregion

        #region Comparison Operators
        public static bool operator ==(FloatVariable a, float b) => a.Value == b;
        public static bool operator ==(float a, FloatVariable b) => a == b.Value;
        public static bool operator ==(FloatVariable a, FloatVariable b) => a.Value == b.Value;

        public static bool operator !=(FloatVariable a, float b) => a.Value != b;
        public static bool operator !=(float a, FloatVariable b) => a != b.Value;
        public static bool operator !=(FloatVariable a, FloatVariable b) => a.Value != b.Value;

        public static bool operator >(FloatVariable a, float b) => a.Value > b;
        public static bool operator >(float a, FloatVariable b) => a > b.Value;
        public static bool operator >(FloatVariable a, FloatVariable b) => a.Value > b.Value;

        public static bool operator <(FloatVariable a, float b) => a.Value < b;
        public static bool operator <(float a, FloatVariable b) => a < b.Value;
        public static bool operator <(FloatVariable a, FloatVariable b) => a.Value < b.Value;

        public static bool operator >=(FloatVariable a, float b) => a.Value >= b;
        public static bool operator >=(float a, FloatVariable b) => a >= b.Value;
        public static bool operator >=(FloatVariable a, FloatVariable b) => a.Value >= b.Value;

        public static bool operator <=(FloatVariable a, float b) => a.Value <= b;
        public static bool operator <=(float a, FloatVariable b) => a <= b.Value;
        public static bool operator <=(FloatVariable a, FloatVariable b) => a.Value <= b.Value;
        #endregion

        #region Equals & GetHashCode Overrides
        public override bool Equals(object? obj)
        {
            if (obj is FloatVariable ptr) return Value == ptr.Value;
            if (obj is float f) return Value == f;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => $"Address: {Address.ToString()} | Value: {Value.ToString()}";
        #endregion
    }
}