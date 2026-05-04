using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public static class InputManager
    {
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT { public int dx; public int dy; public uint mouseData; public uint dwFlags; public uint time; public IntPtr dwExtraInfo; }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion { [FieldOffset(0)] public MOUSEINPUT mi; }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT { public int type; public InputUnion U; }

        const int INPUT_MOUSE = 0;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void MoveMouse(int dx, int dy)
        {
            INPUT input = new INPUT
            {
                type = INPUT_MOUSE,
                U = new InputUnion { mi = new MOUSEINPUT { dx = dx, dy = dy, mouseData = 0, dwFlags = MOUSEEVENTF_MOVE, time = 0, dwExtraInfo = IntPtr.Zero } }
            };

            SendInput(1, new INPUT[] { input }, Marshal.SizeOf(typeof(INPUT)));
        }


        public static void MoveMouseRepeat(int dx, int dy, int rep, int sleep = 100)
        {
            INPUT input = new INPUT
            {
                type = INPUT_MOUSE,
                U = new InputUnion{ mi = new MOUSEINPUT{ dx = dx, dy = dy, mouseData = 0, dwFlags = MOUSEEVENTF_MOVE, time = 0, dwExtraInfo = IntPtr.Zero } }
            };

            for (int i = 0; i < rep; i++)
            {
                SendInput(1, new INPUT[] { input }, Marshal.SizeOf(typeof(INPUT)));
                Thread.Sleep(sleep);
            }
        }
    }
}
