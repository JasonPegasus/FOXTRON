using FX_Core;
using FX_Forms.Styles;

namespace FX_Forms.Forms
{
    public partial class F_ChangeValue : Form
    {
        FloatVariable fv;
        public F_ChangeValue(FloatVariable fv)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            this.fv = fv;
            this.Text = $"Change Value of {Shared.PtrStr(fv.Address)} from {fv.Value} to...";
            BT_Cancel.Click += (_, _) => this.Close();
            BT_Reset.Click += (_, _) => Reset();
            BT_Change.Click += (_, _) => Change();

            TB_NewValue.KeyPress += (object? sender, KeyPressEventArgs e) => e.Handled = (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar));

            Reset();
        }

        void Reset()
        {
            TB_NewValue.Text = fv.Value.ToString();
        }

        void Change()
        {
            if (!float.TryParse(TB_NewValue.Text, out float val)) { return; }
            fv.Value = val;
            this.Close();
        }
    }
}
