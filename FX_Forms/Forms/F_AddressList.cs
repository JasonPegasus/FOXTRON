using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FX_Core;
using FX_Forms.Styles;

namespace FX_Forms
{
    public partial class F_AddressList : Form
    {
        public FloatVariable returnFV;

        public F_AddressList(FloatVariable[] fv, string windowTitle)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            this.Text = windowTitle ?? "Process Selection";
            BT_Select.Click += (_, _) => Select();
        }

        void SelectAddress()
        {
            
        }
    }
}