namespace FX_Forms
{
    partial class F_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TopMenuStrip = new MenuStrip();
            Menu_File = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            Menu_Attach = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            Menu_Logs = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            TopMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.BackColor = Color.FromArgb(32, 32, 32);
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { Menu_File, Menu_Attach, viewToolStripMenuItem, toolStripMenuItem1 });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Padding = new Padding(5, 1, 0, 1);
            TopMenuStrip.Size = new Size(533, 24);
            TopMenuStrip.TabIndex = 0;
            TopMenuStrip.Text = "Top Menu Strip";
            // 
            // Menu_File
            // 
            Menu_File.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            Menu_File.ForeColor = Color.White;
            Menu_File.Name = "Menu_File";
            Menu_File.Size = new Size(37, 22);
            Menu_File.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(114, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(114, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(114, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            // 
            // Menu_Attach
            // 
            Menu_Attach.ForeColor = Color.White;
            Menu_Attach.Name = "Menu_Attach";
            Menu_Attach.Size = new Size(54, 22);
            Menu_Attach.Text = "Attach";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Menu_Logs });
            viewToolStripMenuItem.ForeColor = Color.White;
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 22);
            viewToolStripMenuItem.Text = "View";
            // 
            // Menu_Logs
            // 
            Menu_Logs.BackColor = Color.FromArgb(64, 64, 64);
            Menu_Logs.ForeColor = Color.White;
            Menu_Logs.Name = "Menu_Logs";
            Menu_Logs.Size = new Size(180, 22);
            Menu_Logs.Text = "Logs";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 22);
            // 
            // F_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(533, 387);
            Controls.Add(TopMenuStrip);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MainMenuStrip = TopMenuStrip;
            Name = "F_Main";
            Text = "Form1";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TopMenuStrip;
        private ToolStripMenuItem Menu_File;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem Menu_Attach;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem Menu_Logs;
        private ToolStripMenuItem toolStripMenuItem1;
    }
}
