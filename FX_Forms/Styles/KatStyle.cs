using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_Core;

namespace FX_Forms.Styles
{
    internal class KatStyle : FormStyle
    {
        public KatStyle(Form form) : base(form) { }

        protected override void ApplyStyle()
        {
            bForm.Paint += OnPaint;
            SetWindowAccent(CurrentWindowStyle, Color.FromArgb(32, 16, 16));
            SetWindowBorderDarkMode(true);
            SetControlsDarkMode(true);
            SetupIndividualControls();
        }

        static public Color bgBaseColor = Color.FromArgb(64, 64, 64);
        static public Color bgGradientColor = Color.FromArgb(48, 48, 48);

        static public Color borderBaseColor = Color.FromArgb(64, 64, 64);
        static public Color borderGradientColor = Color.FromArgb(96, 96, 96);

        static int borderSize = 4;

        static Font font = new Font("Consolas", 8);

        void OnPaint(object? sender, PaintEventArgs e)
        {
            Rectangle fullArea = bForm.ClientRectangle;
            Rectangle centerArea = new Rectangle(borderSize, borderSize, (int)(bForm.ClientRectangle.Width - borderSize*2), (int)(bForm.ClientRectangle.Height - borderSize*2));

            if (centerArea.Width * centerArea.Height <= 0) { return; }

            using (LinearGradientBrush brush = new(fullArea, borderGradientColor, borderBaseColor, LinearGradientMode.ForwardDiagonal))
            { e.Graphics.FillRectangle(brush, fullArea); }
            using (LinearGradientBrush brush = new(centerArea, bgGradientColor, bgBaseColor, LinearGradientMode.BackwardDiagonal))
            { e.Graphics.FillRectangle(brush, centerArea); }

        }

        #region Individual Controls

        void SetupIndividualControls()
        {
            SetControlTypeValues<Control>((Control t) => {
                t.Font = font;
            });
        }

        #endregion
    }
}
