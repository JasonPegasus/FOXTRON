using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FX_Core;
using FX_Forms.Forms;

namespace FX_Forms
{
    internal class FormUtils
    {
        internal static Process? OpenProcessSelectionDialog(Form parent = null, string title = null)
        {
            F_ProcessSelector fSelect = new(title);
            fSelect.Location = (parent ?? fSelect).Location;
            Process? sProc = (fSelect.ShowDialog() == DialogResult.OK) ? ProcessManager.TryGetProcessByID(fSelect.ReturnPID) : null;
            fSelect.Dispose();
            return sProc;
        }

        internal static FloatVariable OpenAddressSelectionDialog(FloatVariable[] fv, Form parent = null, string title = null)
        {
            F_AddressList fSelect = new(fv, title);
            fSelect.Location = (parent ?? fSelect).Location;
            fSelect.ShowDialog();
            FloatVariable sAddr = fSelect.returnFV;
            fSelect.Dispose();
            return sAddr;
        }

        internal static void OpenChangeValueDialog(ref FloatVariable fv, Form parent = null)
        {
            using (F_ChangeValue fSelect = new(fv))
            {
                fSelect.Location = (parent ?? fSelect).Location;
                fSelect.ShowDialog();
            }
        }

        #region -- COLOR --

        internal static Color AddColors(Color color1, Color color2)
        {
            return Color.FromArgb(
                Math.Clamp(color1.A + color2.A, 0, 255),
                Math.Clamp(color1.R + color2.R, 0, 255),
                Math.Clamp(color1.G + color2.G, 0, 255),
                Math.Clamp(color1.B + color2.B, 0, 255)
            );
        }

        // Gracias claude, me daba mucha pereza hacer esto a mano. Por que .NET no lo trae por defecto???
        internal static Color ColorLerp(Color[] colors, float A, bool shortestPath = true)
        {
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("Se necesita al menos un color.", nameof(colors));
            if (colors.Length == 1)
                return colors[0];

            A = Math.Clamp(A, 0f, 1f);

            // Qué tramo del array le toca a A, y qué tan avanzado está dentro del tramo
            float scaled = A * (colors.Length - 1);
            int i = Math.Min((int)scaled, colors.Length - 2);
            float t = scaled - i;

            return LerpPair(colors[i], colors[i + 1], t, shortestPath);
        }

        private static Color LerpPair(Color c1, Color c2, float t, bool shortestPath)
        {
            float h1 = c1.GetHue(), h2 = c2.GetHue();         // 0..360
            float s1 = c1.GetSaturation(), s2 = c2.GetSaturation();  // 0..1
            float l1 = c1.GetBrightness(), l2 = c2.GetBrightness();  // 0..1 (luminosidad)

            // Un color sin saturación (gris/blanco/negro) no tiene hue real:
            // tomamos el del otro para que no "pase" por el rojo (hue 0) sin sentido.
            if (s1 < 1e-4f) h1 = h2;
            else if (s2 < 1e-4f) h2 = h1;

            float d = h2 - h1; // -360..360
            if (shortestPath)
            {
                if (d > 180f) d -= 360f;
                else if (d < -180f) d += 360f;
            }
            else // camino largo: la vuelta opuesta del círculo
            {
                if (d > 0f && d <= 180f) d -= 360f;
                else if (d < 0f && d >= -180f) d += 360f;
            }

            float h = (h1 + d * t) % 360f;
            if (h < 0f) h += 360f;

            float s = s1 + (s2 - s1) * t;
            float l = l1 + (l2 - l1) * t;
            int a = (int)MathF.Round(c1.A + (c2.A - c1.A) * t);

            return FromHsl(a, h, s, l);
        }

        private static Color FromHsl(int alpha, float h, float s, float l)
        {
            float c = (1f - MathF.Abs(2f * l - 1f)) * s;
            float hp = h / 60f;
            float x = c * (1f - MathF.Abs(hp % 2f - 1f));

            float r = 0, g = 0, b = 0;
            if (hp < 1f) { r = c; g = x; }
            else if (hp < 2f) { r = x; g = c; }
            else if (hp < 3f) { g = c; b = x; }
            else if (hp < 4f) { g = x; b = c; }
            else if (hp < 5f) { r = x; b = c; }
            else { r = c; b = x; }

            float m = l - c / 2f;
            static int B(float v) => Math.Clamp((int)MathF.Round(v * 255f), 0, 255);

            return Color.FromArgb(alpha, B(r + m), B(g + m), B(b + m));
        }

        #endregion
    }
}
