using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FX_Forms
{
    internal abstract class FormStyle
    {
        public static T SetStyle<T>(Form form) where T : FormStyle
        { return (T)Activator.CreateInstance(typeof(T), form); }

        protected Form bForm;
        protected FormStyle(Form form)
        {
            this.bForm = form;
            typeof(Form).GetProperty("ResizeRedraw", BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(form, true);
            typeof(Form).GetProperty("DoubleBuffered", BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(form, true);
            ApplyStyle();

            OnWindowStyleChanged += (WindowStyle ws) => { SetWindowAccent(ws); };
        }

        protected abstract void ApplyStyle();

        protected void SetControlTypeValues<T>(Action<T> Function, Control parent = null) where T : Control
        {
            foreach (T c in (parent ?? bForm).Controls.OfType<T>())
            {
                Function(c);
                SetControlTypeValues(Function, c);
            }
        }


        #region Form Dark Mode

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)] private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public void SetControlsDarkMode(bool enabled, Control ctrl = null)
        {
            Control target = ctrl ?? bForm;

            SetWindowTheme(target.Handle, enabled ? "DarkMode_CFD" : "Explorer", null);
            foreach (Control c in target.Controls)
            {
                SetControlsDarkMode(enabled, c);
            }
        }
        #endregion

        #region Window Border Dark Mode

        [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public void SetWindowBorderDarkMode(bool enabled)
        {
            int val = enabled ? 1 : 0;
            DwmSetWindowAttribute(bForm.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref val, sizeof(int));
        }

        #endregion

        static WindowStyle _currentWindowStyle = WindowStyle.Default;
        public static WindowStyle CurrentWindowStyle
        {
            get { return _currentWindowStyle; }
            set { _currentWindowStyle = value; OnWindowStyleChanged?.Invoke(value); }
        }

        public static event Action<WindowStyle>? OnWindowStyleChanged;

        #region Accent & Window Frame Visuals

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        public enum WindowStyle
        {
            Default = 0,
            SolidColor = 1,
            SoftSolidColor = 2,
            Blur = 3,
            LaggedGradient = 4,
            Unused = 5,
            Transparent = 6
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public WindowStyle AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public nint Data;
            public int SizeOfData;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(
            nint hwnd,
            ref WindowCompositionAttributeData data);


        protected void SetWindowAccent(WindowStyle accentState) { SetWindowAccent(accentState, Color.Black); }
        protected void SetWindowAccent(WindowStyle accentState, Color color)
        {
            nint hwnd = bForm.Handle;
            AccentPolicy accent = new()
            {
                AccentState = accentState,
                AccentFlags = 0,
                GradientColor = ToAbgr(color),
                AnimationId = 0
            };

            int size = Marshal.SizeOf<AccentPolicy>();
            nint accentPtr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.StructureToPtr(accent, accentPtr, false);

                WindowCompositionAttributeData data = new()
                {
                    Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                    Data = accentPtr,
                    SizeOfData = size
                };

                SetWindowCompositionAttribute(hwnd, ref data);
            }
            finally
            {
                Marshal.FreeHGlobal(accentPtr);
            }
        }

        private int ToAbgr(Color color)
        {
            return color.A << 24 |
                   color.B << 16 |
                   color.G << 8 |
                   color.R;
        }

        #endregion
    }
}
