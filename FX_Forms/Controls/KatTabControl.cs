using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_Forms.Styles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FX_Forms.Controls
{
    public class KatTabControl : TabControl
    {
        public KatTabControl()
        {
            DrawMode = TabDrawMode.OwnerDrawFixed;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            Invalidate();
            base.OnSelectedIndexChanged(e);
        }

        Color lastBgColor = Color.FromArgb(64, 64, 64);
        Color bgColor = Color.FromArgb(32,32,32);
        Color tabColor = Color.FromArgb(48, 48, 48);
        Color selectedTabColor = Color.FromArgb(24,24,24);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(lastBgColor), this.Bounds);
            for (int i = 0; i < TabCount; i++)
            {
                DrawTab(e.Graphics, i);
            }
        }

        private void DrawTab(Graphics g, int index)
        {
            TabPage tab = this.TabPages[index];
            Rectangle defRect = GetTabRect(index);
            Rectangle rect = new Rectangle(defRect.X + 2, defRect.Y + 1, defRect.Width, defRect.Height + 1);
            tab.BackColor = bgColor;

            bool selected = index == SelectedIndex;

            //using var bg = new LinearGradientBrush(rect, tabColor, selected ? selectedTabColor : tabColor, LinearGradientMode.ForwardDiagonal);
            using var bg = new SolidBrush(selected ? selectedTabColor : tabColor);
            using var fg = new SolidBrush(Color.White);

            g.FillRectangle(bg, rect);

            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center})
            {
                g.DrawString(TabPages[index].Text, Font, fg, rect, format);
            }
        }
    }
}
