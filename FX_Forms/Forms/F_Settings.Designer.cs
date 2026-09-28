namespace FX_Forms.Forms
{
    partial class F_Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            katTabControl1 = new Controls.KatTabControl();
            tabPage1 = new TabPage();
            label1 = new Label();
            CM_WindowStyle = new ComboBox();
            tabPage2 = new TabPage();
            katTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // katTabControl1
            // 
            katTabControl1.Controls.Add(tabPage1);
            katTabControl1.Controls.Add(tabPage2);
            katTabControl1.Dock = DockStyle.Fill;
            katTabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            katTabControl1.Location = new Point(0, 0);
            katTabControl1.Name = "katTabControl1";
            katTabControl1.SelectedIndex = 0;
            katTabControl1.Size = new Size(800, 450);
            katTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(32, 32, 32);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(CM_WindowStyle);
            tabPage1.ForeColor = Color.White;
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 421);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Appearance";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 12);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 5;
            label1.Text = "Window Style";
            // 
            // CM_WindowStyle
            // 
            CM_WindowStyle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CM_WindowStyle.BackColor = Color.FromArgb(32, 32, 32);
            CM_WindowStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_WindowStyle.ForeColor = Color.White;
            CM_WindowStyle.FormattingEnabled = true;
            CM_WindowStyle.Location = new Point(8, 30);
            CM_WindowStyle.Name = "CM_WindowStyle";
            CM_WindowStyle.Size = new Size(138, 23);
            CM_WindowStyle.TabIndex = 4;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(32, 32, 32);
            tabPage2.ForeColor = Color.White;
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 421);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Controls & Hotkeys";
            // 
            // F_Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(katTabControl1);
            Name = "F_Settings";
            Text = "Settings";
            katTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.KatTabControl katTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ComboBox CM_WindowStyle;
        private Label label1;
    }
}