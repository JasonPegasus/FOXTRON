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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            TopMenuStrip = new MenuStrip();
            Menu_File = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            Menu_Attach = new ToolStripMenuItem();
            Menu_View = new ToolStripMenuItem();
            Menu_Logs = new ToolStripMenuItem();
            Menu_Settings = new ToolStripMenuItem();
            CM_ScanSelection = new ComboBox();
            BT_Scan = new Button();
            label1 = new Label();
            label2 = new Label();
            CM_MemoryChunkSizes = new ComboBox();
            DG_Variables = new Styles.KatDataGridView();
            V_address = new DataGridViewTextBoxColumn();
            V_name = new DataGridViewTextBoxColumn();
            V_value = new DataGridViewTextBoxColumn();
            TopMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).BeginInit();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.BackColor = Color.FromArgb(32, 32, 32);
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { Menu_File, Menu_Attach, Menu_View, Menu_Settings });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Padding = new Padding(5, 1, 0, 1);
            TopMenuStrip.Size = new Size(580, 24);
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
            // Menu_View
            // 
            Menu_View.DropDownItems.AddRange(new ToolStripItem[] { Menu_Logs });
            Menu_View.ForeColor = Color.White;
            Menu_View.Name = "Menu_View";
            Menu_View.Size = new Size(44, 22);
            Menu_View.Text = "View";
            // 
            // Menu_Logs
            // 
            Menu_Logs.BackColor = Color.FromArgb(64, 64, 64);
            Menu_Logs.ForeColor = Color.White;
            Menu_Logs.Name = "Menu_Logs";
            Menu_Logs.Size = new Size(99, 22);
            Menu_Logs.Text = "Logs";
            // 
            // Menu_Settings
            // 
            Menu_Settings.ForeColor = Color.White;
            Menu_Settings.Name = "Menu_Settings";
            Menu_Settings.Size = new Size(61, 22);
            Menu_Settings.Text = "Settings";
            // 
            // CM_ScanSelection
            // 
            CM_ScanSelection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CM_ScanSelection.BackColor = Color.FromArgb(32, 32, 32);
            CM_ScanSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_ScanSelection.ForeColor = Color.White;
            CM_ScanSelection.FormattingEnabled = true;
            CM_ScanSelection.Location = new Point(398, 52);
            CM_ScanSelection.Name = "CM_ScanSelection";
            CM_ScanSelection.Size = new Size(170, 23);
            CM_ScanSelection.TabIndex = 3;
            // 
            // BT_Scan
            // 
            BT_Scan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BT_Scan.BackColor = Color.FromArgb(192, 0, 0);
            BT_Scan.FlatStyle = FlatStyle.Popup;
            BT_Scan.Location = new Point(398, 128);
            BT_Scan.Name = "BT_Scan";
            BT_Scan.Size = new Size(170, 38);
            BT_Scan.TabIndex = 5;
            BT_Scan.Text = "Scan";
            BT_Scan.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(398, 34);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 7;
            label1.Text = "Variable to Find";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(398, 81);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 11;
            label2.Text = "Memory Chunk Size";
            label2.Click += label2_Click;
            // 
            // CM_MemoryChunkSizes
            // 
            CM_MemoryChunkSizes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CM_MemoryChunkSizes.BackColor = Color.FromArgb(32, 32, 32);
            CM_MemoryChunkSizes.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_MemoryChunkSizes.ForeColor = Color.White;
            CM_MemoryChunkSizes.FormattingEnabled = true;
            CM_MemoryChunkSizes.Location = new Point(398, 99);
            CM_MemoryChunkSizes.Name = "CM_MemoryChunkSizes";
            CM_MemoryChunkSizes.Size = new Size(113, 23);
            CM_MemoryChunkSizes.TabIndex = 12;
            // 
            // DG_Variables
            // 
            DG_Variables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DG_Variables.BackgroundColor = Color.FromArgb(64, 64, 64);
            DG_Variables.BorderStyle = BorderStyle.None;
            DG_Variables.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle1.ForeColor = Color.White;
            DG_Variables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DG_Variables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DG_Variables.Columns.AddRange(new DataGridViewColumn[] { V_address, V_name, V_value });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DG_Variables.DefaultCellStyle = dataGridViewCellStyle2;
            DG_Variables.EnableHeadersVisualStyles = false;
            DG_Variables.GridColor = Color.DarkGray;
            DG_Variables.Location = new Point(12, 34);
            DG_Variables.Name = "DG_Variables";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            DG_Variables.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DG_Variables.RowHeadersVisible = false;
            DG_Variables.Size = new Size(380, 391);
            DG_Variables.TabIndex = 13;
            // 
            // V_address
            // 
            V_address.HeaderText = "Address";
            V_address.Name = "V_address";
            V_address.ReadOnly = true;
            V_address.Width = 60;
            // 
            // V_name
            // 
            V_name.HeaderText = "Name";
            V_name.Name = "V_name";
            V_name.ReadOnly = true;
            V_name.Width = 200;
            // 
            // V_value
            // 
            V_value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            V_value.HeaderText = "Value";
            V_value.Name = "V_value";
            // 
            // F_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(580, 437);
            Controls.Add(DG_Variables);
            Controls.Add(CM_MemoryChunkSizes);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BT_Scan);
            Controls.Add(CM_ScanSelection);
            Controls.Add(TopMenuStrip);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MainMenuStrip = TopMenuStrip;
            Name = "F_Main";
            Text = "Form1";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).EndInit();
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
        private ToolStripMenuItem Menu_View;
        private ToolStripMenuItem Menu_Logs;
        private ComboBox CM_ScanSelection;
        private Button BT_Scan;
        private Label label1;
        private Label label2;
        private ComboBox CM_MemoryChunkSizes;
        private ToolStripMenuItem Menu_Settings;
        private Styles.KatDataGridView DG_Variables;
        private DataGridViewTextBoxColumn V_address;
        private DataGridViewTextBoxColumn V_name;
        private DataGridViewTextBoxColumn V_value;
    }
}
