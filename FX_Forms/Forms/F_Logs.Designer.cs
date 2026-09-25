namespace FX_Forms.Forms
{
    partial class F_Logs
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
            CH_AutoScroll = new CheckBox();
            RTB_Console = new RichTextBox();
            SuspendLayout();
            // 
            // CH_AutoScroll
            // 
            CH_AutoScroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CH_AutoScroll.AutoSize = true;
            CH_AutoScroll.Checked = true;
            CH_AutoScroll.CheckState = CheckState.Checked;
            CH_AutoScroll.ForeColor = Color.White;
            CH_AutoScroll.Location = new Point(317, 499);
            CH_AutoScroll.Margin = new Padding(2);
            CH_AutoScroll.Name = "CH_AutoScroll";
            CH_AutoScroll.Size = new Size(84, 19);
            CH_AutoScroll.TabIndex = 4;
            CH_AutoScroll.Text = "Auto Scroll";
            CH_AutoScroll.UseVisualStyleBackColor = true;
            // 
            // RTB_Console
            // 
            RTB_Console.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RTB_Console.BackColor = Color.Black;
            RTB_Console.ForeColor = Color.White;
            RTB_Console.Location = new Point(11, 11);
            RTB_Console.Margin = new Padding(2);
            RTB_Console.Name = "RTB_Console";
            RTB_Console.Size = new Size(390, 484);
            RTB_Console.TabIndex = 3;
            RTB_Console.Text = "";
            // 
            // F_Logs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(412, 529);
            Controls.Add(CH_AutoScroll);
            Controls.Add(RTB_Console);
            Name = "F_Logs";
            Text = "Logs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CH_AutoScroll;
        private RichTextBox RTB_Console;
    }
}