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
            CM_MemoryPageSizes = new ComboBox();
            DG_Variables = new Styles.KatDataGridView();
            V_address = new DataGridViewTextBoxColumn();
            V_name = new DataGridViewTextBoxColumn();
            V_value = new DataGridViewTextBoxColumn();
            GB_Scanning = new GroupBox();
            button1 = new Button();
            GB_Variables = new GroupBox();
            button2 = new Button();
            comboBox1 = new ComboBox();
            TopMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).BeginInit();
            GB_Scanning.SuspendLayout();
            GB_Variables.SuspendLayout();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.BackColor = Color.FromArgb(32, 32, 32);
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { Menu_File, Menu_Attach, Menu_View, Menu_Settings });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Padding = new Padding(5, 1, 0, 1);
            TopMenuStrip.Size = new Size(557, 24);
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
            CM_ScanSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CM_ScanSelection.BackColor = Color.FromArgb(32, 32, 32);
            CM_ScanSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_ScanSelection.ForeColor = Color.White;
            CM_ScanSelection.FormattingEnabled = true;
            CM_ScanSelection.Location = new Point(6, 37);
            CM_ScanSelection.Name = "CM_ScanSelection";
            CM_ScanSelection.Size = new Size(162, 23);
            CM_ScanSelection.TabIndex = 3;
            // 
            // BT_Scan
            // 
            BT_Scan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BT_Scan.BackColor = Color.FromArgb(192, 0, 0);
            BT_Scan.FlatStyle = FlatStyle.Popup;
            BT_Scan.Location = new Point(6, 117);
            BT_Scan.Name = "BT_Scan";
            BT_Scan.Size = new Size(162, 35);
            BT_Scan.TabIndex = 5;
            BT_Scan.Text = "Scan";
            BT_Scan.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(88, 15);
            label1.TabIndex = 7;
            label1.Text = "Variable to Find";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 11;
            label2.Text = "Memory Page Size";
            label2.Click += label2_Click;
            // 
            // CM_MemoryPageSizes
            // 
            CM_MemoryPageSizes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CM_MemoryPageSizes.BackColor = Color.FromArgb(32, 32, 32);
            CM_MemoryPageSizes.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_MemoryPageSizes.ForeColor = Color.White;
            CM_MemoryPageSizes.FormattingEnabled = true;
            CM_MemoryPageSizes.Location = new Point(6, 81);
            CM_MemoryPageSizes.Name = "CM_MemoryPageSizes";
            CM_MemoryPageSizes.Size = new Size(113, 23);
            CM_MemoryPageSizes.TabIndex = 12;
            // 
            // DG_Variables
            // 
            DG_Variables.AllowUserToAddRows = false;
            DG_Variables.AllowUserToResizeRows = false;
            DG_Variables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DG_Variables.BackgroundColor = Color.FromArgb(48, 48, 48);
            DG_Variables.BorderStyle = BorderStyle.None;
            DG_Variables.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
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
            DG_Variables.Size = new Size(337, 443);
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
            // GB_Scanning
            // 
            GB_Scanning.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GB_Scanning.Controls.Add(label1);
            GB_Scanning.Controls.Add(CM_ScanSelection);
            GB_Scanning.Controls.Add(CM_MemoryPageSizes);
            GB_Scanning.Controls.Add(label2);
            GB_Scanning.Controls.Add(BT_Scan);
            GB_Scanning.ForeColor = Color.White;
            GB_Scanning.Location = new Point(355, 34);
            GB_Scanning.Name = "GB_Scanning";
            GB_Scanning.Size = new Size(190, 159);
            GB_Scanning.TabIndex = 15;
            GB_Scanning.TabStop = false;
            GB_Scanning.Text = "Scanning";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.BackColor = Color.FromArgb(48, 48, 48);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(6, 19);
            button1.Name = "button1";
            button1.Size = new Size(162, 37);
            button1.TabIndex = 15;
            button1.Text = "Add Value Manually";
            button1.UseVisualStyleBackColor = false;
            // 
            // GB_Variables
            // 
            GB_Variables.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GB_Variables.Controls.Add(button2);
            GB_Variables.Controls.Add(comboBox1);
            GB_Variables.ForeColor = Color.White;
            GB_Variables.Location = new Point(12, 483);
            GB_Variables.Name = "GB_Variables";
            GB_Variables.Size = new Size(337, 58);
            GB_Variables.TabIndex = 16;
            GB_Variables.TabStop = false;
            GB_Variables.Text = "Variables";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button2.BackColor = Color.FromArgb(48, 48, 48);
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(174, 22);
            button2.Name = "button2";
            button2.Size = new Size(152, 23);
            button2.TabIndex = 6;
            button2.Text = "Add Variable Manually";
            button2.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            comboBox1.BackColor = Color.FromArgb(32, 32, 32);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.ForeColor = Color.White;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(8, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(160, 23);
            comboBox1.TabIndex = 4;
            // 
            // F_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(557, 578);
            Controls.Add(GB_Variables);
            Controls.Add(GB_Scanning);
            Controls.Add(DG_Variables);
            Controls.Add(TopMenuStrip);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MainMenuStrip = TopMenuStrip;
            Name = "F_Main";
            Text = "``";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).EndInit();
            GB_Scanning.ResumeLayout(false);
            GB_Scanning.PerformLayout();
            GB_Variables.ResumeLayout(false);
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
        private ComboBox CM_MemoryPageSizes;
        private ToolStripMenuItem Menu_Settings;
        private Styles.KatDataGridView DG_Variables;
        private DataGridViewTextBoxColumn V_address;
        private DataGridViewTextBoxColumn V_name;
        private DataGridViewTextBoxColumn V_value;
        private GroupBox GB_Scanning;
        private Button button1;
        private GroupBox GB_Variables;
        private Button button2;
        private ComboBox comboBox1;
    }
}
