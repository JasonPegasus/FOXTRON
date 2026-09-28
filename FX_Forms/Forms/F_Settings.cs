using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FX_Forms.Styles;

namespace FX_Forms.Forms
{
    public partial class F_Settings : Form
    {
        F_Main mainForm;

        public F_Settings(F_Main mainForm)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            this.mainForm = mainForm;
            this.Location = mainForm.Location;

            CM_WindowStyle.Items.AddRange(windowStyleList.Keys.ToArray());
            CM_WindowStyle.SelectedIndexChanged += (_, _) => OnWindowStyleSelected();
            CM_WindowStyle.SelectedIndex = 0;
        }

        void OnWindowStyleSelected()
        {
            FormStyle.CurrentWindowStyle = windowStyleList[CM_WindowStyle.SelectedItem.ToString()];
        }

        Dictionary<string, FormStyle.WindowStyle> windowStyleList = new()
        {
            { "Default", FormStyle.WindowStyle.Default },
            { "Blur/Aero", FormStyle.WindowStyle.Blur },
            { "Transparent", FormStyle.WindowStyle.Transparent },
            { "Gradient (Laggy)", FormStyle.WindowStyle.LaggedGradient },
        };
    }
}
