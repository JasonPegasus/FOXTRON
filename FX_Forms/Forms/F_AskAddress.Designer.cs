namespace FX_Forms.Forms
{
    partial class F_AskAddress
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
            BT_Cancel = new Button();
            BT_Change = new Button();
            TB_AddressInput = new TextBox();
            SuspendLayout();
            // 
            // BT_Cancel
            // 
            BT_Cancel.BackColor = Color.FromArgb(48, 48, 48);
            BT_Cancel.FlatStyle = FlatStyle.Popup;
            BT_Cancel.ForeColor = Color.White;
            BT_Cancel.Location = new Point(12, 41);
            BT_Cancel.Name = "BT_Cancel";
            BT_Cancel.Size = new Size(91, 28);
            BT_Cancel.TabIndex = 5;
            BT_Cancel.Text = "Cancel";
            BT_Cancel.UseVisualStyleBackColor = false;
            // 
            // BT_Change
            // 
            BT_Change.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BT_Change.BackColor = Color.FromArgb(48, 48, 48);
            BT_Change.FlatStyle = FlatStyle.Popup;
            BT_Change.ForeColor = Color.White;
            BT_Change.Location = new Point(109, 41);
            BT_Change.Name = "BT_Change";
            BT_Change.Size = new Size(145, 28);
            BT_Change.TabIndex = 4;
            BT_Change.Text = "Confirm";
            BT_Change.UseVisualStyleBackColor = false;
            // 
            // TB_AddressInput
            // 
            TB_AddressInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TB_AddressInput.BackColor = Color.FromArgb(48, 48, 48);
            TB_AddressInput.ForeColor = Color.White;
            TB_AddressInput.Location = new Point(12, 12);
            TB_AddressInput.Name = "TB_AddressInput";
            TB_AddressInput.Size = new Size(242, 23);
            TB_AddressInput.TabIndex = 3;
            // 
            // F_AskAddress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(266, 77);
            Controls.Add(BT_Cancel);
            Controls.Add(BT_Change);
            Controls.Add(TB_AddressInput);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "F_AskAddress";
            Text = "F_AskAddress";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BT_Cancel;
        private Button BT_Change;
        private TextBox TB_AddressInput;
    }
}