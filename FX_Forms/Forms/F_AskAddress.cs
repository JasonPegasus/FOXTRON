using FX_Core;
using FX_Forms.Styles;

namespace FX_Forms.Forms
{
    public partial class F_AskAddress : Form
    {
        public nint returnAddr;
        public F_AskAddress(string variableName)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            this.Text = $"Input Address of: {variableName}";
            BT_Cancel.Click += (_, _) => this.Close();
            BT_Change.Click += (_, _) => Change();

            TB_AddressInput.KeyPress += (object? sender, KeyPressEventArgs e) => e.Handled = (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar) 
            && char.ToUpperInvariant(e.KeyChar) == 'X');
        }

        void Change()
        {
            try
            {
                returnAddr = Shared.StringToPtr(TB_AddressInput.Text);
                this.Close();
            }
            catch 
            {
                FormUtils.ShowMessage("Invalid address input (Format should be in base 16)");
            }
        }
    }
}
