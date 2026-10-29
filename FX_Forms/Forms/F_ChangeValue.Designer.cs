namespace FX_Forms.Forms
{
    partial class F_ChangeValue
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
            TB_NewValue = new TextBox();
            BT_Change = new Button();
            BT_Cancel = new Button();
            BT_Reset = new Button();
            SuspendLayout();
            // 
            // TB_NewValue
            // 
            TB_NewValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TB_NewValue.BackColor = Color.FromArgb(48, 48, 48);
            TB_NewValue.ForeColor = Color.White;
            TB_NewValue.Location = new Point(12, 12);
            TB_NewValue.Name = "TB_NewValue";
            TB_NewValue.Size = new Size(347, 23);
            TB_NewValue.TabIndex = 0;
            // 
            // BT_Change
            // 
            BT_Change.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BT_Change.BackColor = Color.FromArgb(48, 48, 48);
            BT_Change.FlatStyle = FlatStyle.Popup;
            BT_Change.ForeColor = Color.White;
            BT_Change.Location = new Point(206, 41);
            BT_Change.Name = "BT_Change";
            BT_Change.Size = new Size(153, 28);
            BT_Change.TabIndex = 1;
            BT_Change.Text = "Confirm";
            BT_Change.UseVisualStyleBackColor = false;
            // 
            // BT_Cancel
            // 
            BT_Cancel.BackColor = Color.FromArgb(48, 48, 48);
            BT_Cancel.FlatStyle = FlatStyle.Popup;
            BT_Cancel.ForeColor = Color.White;
            BT_Cancel.Location = new Point(12, 41);
            BT_Cancel.Name = "BT_Cancel";
            BT_Cancel.Size = new Size(91, 28);
            BT_Cancel.TabIndex = 2;
            BT_Cancel.Text = "Cancel";
            BT_Cancel.UseVisualStyleBackColor = false;
            // 
            // BT_Reset
            // 
            BT_Reset.BackColor = Color.FromArgb(48, 48, 48);
            BT_Reset.FlatStyle = FlatStyle.Popup;
            BT_Reset.ForeColor = Color.White;
            BT_Reset.Location = new Point(109, 41);
            BT_Reset.Name = "BT_Reset";
            BT_Reset.Size = new Size(91, 28);
            BT_Reset.TabIndex = 3;
            BT_Reset.Text = "Reset";
            BT_Reset.UseVisualStyleBackColor = false;
            // 
            // F_ChangeValue
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(371, 81);
            Controls.Add(BT_Reset);
            Controls.Add(BT_Cancel);
            Controls.Add(BT_Change);
            Controls.Add(TB_NewValue);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "F_ChangeValue";
            Text = "F_VariableChange";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TB_NewValue;
        private Button BT_Change;
        private Button BT_Cancel;
        private Button BT_Reset;
    }
}