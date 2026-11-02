using System.Drawing.Drawing2D;

namespace FX_Forms.Styles
{
    internal class KatStyle : FormStyle
    {
        public KatStyle(Form form) : base(form) { }


        //private readonly System.Windows.Forms.Timer timer = new() { Interval = 16 }; // ~60 fps

        protected override void ApplyStyle()
        {
            bForm.Paint += OnPaint;
            SetWindowAccent(CurrentWindowStyle, Color.FromArgb(32, 16, 16));
            SetWindowBorderDarkMode(true);
            SetControlsDarkMode(true);
            SetupIndividualControls();
            bForm.BackColor = bgBaseColor;

            //timer.Tick += (_, _) => bForm.Invalidate();
            //timer.Start();
            upperFrame = bForm.Controls.OfType<MenuStrip>().FirstOrDefault()?.Height ?? 0;
        }

        readonly Color bgBaseColor = Color.FromArgb(40, 40, 40);
        readonly Color bgGradientColor = Color.FromArgb(30, 30, 30);
        readonly Color borderBaseColor = Color.FromArgb(64, 64, 64);
        readonly Color borderGradientColor = Color.FromArgb(96, 96, 96);

        readonly int borderSize = 4;

        readonly Font font = new Font("Consolas", 8);

        long time = 0;

        int upperFrame = 0;

        void OnPaint(object? sender, PaintEventArgs e)
        {
            time++;
            Rectangle fullArea = new Rectangle(bForm.ClientRectangle.X, bForm.ClientRectangle.Y + upperFrame, bForm.ClientRectangle.Width, bForm.ClientRectangle.Height - upperFrame);
            Rectangle centerArea = new Rectangle(fullArea.X + borderSize, fullArea.Y + borderSize, 
                (int)(fullArea.Width - borderSize*2), (int)(fullArea.Height - borderSize*2));

            //Color rainbow = FormUtils.ColorLerp([Color.Red, Color.Green, Color.Blue, Color.Red], (float)(time % 500) / 500, false);


            //Color[] bgFull = [
            //    FormUtils.ColorLerp([bgBaseColor, FormUtils.AddColors(bgBaseColor, Color.FromArgb(40, 0, 0)), bgBaseColor], (float)(time % 500) / 500, true),
            //    bgGradientColor,
            //    //FormUtils.ColorLerp([bgBaseColor, bgGradientColor, bgBaseColor], (float)((time+250) % 500) / 500, true)
            //];

            Color[] borderFull = [
                FormUtils.ColorLerp([borderBaseColor, borderGradientColor, borderBaseColor], (float)(time % 500) / 500, true),
                FormUtils.ColorLerp([borderBaseColor, FormUtils.AddColors(borderGradientColor, Color.FromArgb(100, 0, 0)), borderBaseColor], (float)((time+250) % 500) / 500, true)
            ];

            // BORDER
            if (centerArea.Width * centerArea.Height <= 0) { return; }
            using (LinearGradientBrush brush = new(fullArea, borderFull[0], borderFull[1], LinearGradientMode.ForwardDiagonal))
            { e.Graphics.FillRectangle(brush, fullArea); }

            // CENTER
            //using (LinearGradientBrush brush = new(new Rectangle(centerArea.X, centerArea.Y, centerArea.Width, 
            //    (int)(centerArea.Height * (4 + Math.Sin((double)time/100)*1))), bgBaseColor, bgGradientColor, LinearGradientMode.BackwardDiagonal))
            //{ e.Graphics.FillRectangle(brush, centerArea); }
            using (LinearGradientBrush brush = new(new Rectangle(centerArea.X, centerArea.Y, centerArea.Width, centerArea.Height),
                bgBaseColor, bgGradientColor, LinearGradientMode.BackwardDiagonal))
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