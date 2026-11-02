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
            TMS_TopMenu = new MenuStrip();
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
            GB_Scanning = new GroupBox();
            CB_PauseScanning = new CheckBox();
            NUD_AverageFPS = new NumericUpDown();
            label3 = new Label();
            button1 = new Button();
            GB_Variables = new GroupBox();
            BT_AddManualAddress = new Button();
            V_address = new DataGridViewTextBoxColumn();
            V_name = new DataGridViewTextBoxColumn();
            V_value = new DataGridViewTextBoxColumn();
            V_BT_name = new DataGridViewButtonColumn();
            TMS_TopMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).BeginInit();
            GB_Scanning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_AverageFPS).BeginInit();
            GB_Variables.SuspendLayout();
            SuspendLayout();
            // 
            // TMS_TopMenu
            // 
            TMS_TopMenu.BackColor = Color.FromArgb(32, 32, 32);
            TMS_TopMenu.Items.AddRange(new ToolStripItem[] { Menu_File, Menu_Attach, Menu_View, Menu_Settings });
            TMS_TopMenu.Location = new Point(0, 0);
            TMS_TopMenu.Name = "TMS_TopMenu";
            TMS_TopMenu.Padding = new Padding(5, 1, 0, 1);
            TMS_TopMenu.Size = new Size(618, 24);
            TMS_TopMenu.TabIndex = 0;
            TMS_TopMenu.Text = "Top Menu Strip";
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
            BT_Scan.Location = new Point(6, 121);
            BT_Scan.Name = "BT_Scan";
            BT_Scan.Size = new Size(162, 39);
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
            label1.Size = new Size(95, 15);
            label1.TabIndex = 7;
            label1.Text = "Selected Variable";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(6, 20);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 11;
            label2.Text = "Memory Page Size";
            // 
            // CM_MemoryPageSizes
            // 
            CM_MemoryPageSizes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CM_MemoryPageSizes.BackColor = Color.FromArgb(32, 32, 32);
            CM_MemoryPageSizes.DropDownStyle = ComboBoxStyle.DropDownList;
            CM_MemoryPageSizes.ForeColor = Color.White;
            CM_MemoryPageSizes.FormattingEnabled = true;
            CM_MemoryPageSizes.Location = new Point(6, 38);
            CM_MemoryPageSizes.Name = "CM_MemoryPageSizes";
            CM_MemoryPageSizes.Size = new Size(104, 23);
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
            DG_Variables.Columns.AddRange(new DataGridViewColumn[] { V_address, V_name, V_value, V_BT_name });
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
            dataGridViewCellStyle3.SelectionBackColor = Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            DG_Variables.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DG_Variables.RowHeadersVisible = false;
            DG_Variables.Size = new Size(398, 532);
            DG_Variables.TabIndex = 13;
            // 
            // GB_Scanning
            // 
            GB_Scanning.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GB_Scanning.Controls.Add(CB_PauseScanning);
            GB_Scanning.Controls.Add(NUD_AverageFPS);
            GB_Scanning.Controls.Add(label3);
            GB_Scanning.Controls.Add(CM_MemoryPageSizes);
            GB_Scanning.Controls.Add(label2);
            GB_Scanning.Controls.Add(BT_Scan);
            GB_Scanning.ForeColor = Color.White;
            GB_Scanning.Location = new Point(416, 140);
            GB_Scanning.Name = "GB_Scanning";
            GB_Scanning.Size = new Size(190, 166);
            GB_Scanning.TabIndex = 15;
            GB_Scanning.TabStop = false;
            GB_Scanning.Text = "Scanning";
            // 
            // CB_PauseScanning
            // 
            CB_PauseScanning.AutoSize = true;
            CB_PauseScanning.Location = new Point(6, 96);
            CB_PauseScanning.Name = "CB_PauseScanning";
            CB_PauseScanning.Size = new Size(144, 19);
            CB_PauseScanning.TabIndex = 20;
            CB_PauseScanning.Text = "Freeze While Scanning";
            CB_PauseScanning.UseVisualStyleBackColor = true;
            // 
            // NUD_AverageFPS
            // 
            NUD_AverageFPS.BackColor = Color.FromArgb(48, 48, 48);
            NUD_AverageFPS.ForeColor = Color.White;
            NUD_AverageFPS.Location = new Point(6, 67);
            NUD_AverageFPS.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            NUD_AverageFPS.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NUD_AverageFPS.Name = "NUD_AverageFPS";
            NUD_AverageFPS.RightToLeft = RightToLeft.No;
            NUD_AverageFPS.Size = new Size(56, 23);
            NUD_AverageFPS.TabIndex = 19;
            NUD_AverageFPS.TextAlign = HorizontalAlignment.Center;
            NUD_AverageFPS.Value = new decimal(new int[] { 120, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(68, 69);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 18;
            label3.Text = "Average Game FPS";
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
            GB_Variables.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GB_Variables.Controls.Add(BT_AddManualAddress);
            GB_Variables.Controls.Add(CM_ScanSelection);
            GB_Variables.Controls.Add(label1);
            GB_Variables.ForeColor = Color.White;
            GB_Variables.Location = new Point(416, 34);
            GB_Variables.Name = "GB_Variables";
            GB_Variables.Size = new Size(190, 100);
            GB_Variables.TabIndex = 16;
            GB_Variables.TabStop = false;
            GB_Variables.Text = "Variables";
            // 
            // BT_AddManualAddress
            // 
            BT_AddManualAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BT_AddManualAddress.BackColor = Color.FromArgb(48, 48, 48);
            BT_AddManualAddress.Enabled = false;
            BT_AddManualAddress.FlatStyle = FlatStyle.Popup;
            BT_AddManualAddress.Location = new Point(6, 66);
            BT_AddManualAddress.Name = "BT_AddManualAddress";
            BT_AddManualAddress.Size = new Size(162, 23);
            BT_AddManualAddress.TabIndex = 6;
            BT_AddManualAddress.Text = "Add/Set Variable Manually";
            BT_AddManualAddress.UseVisualStyleBackColor = false;
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
            // V_BT_name
            // 
            V_BT_name.HeaderText = "Edit";
            V_BT_name.Name = "V_BT_name";
            V_BT_name.Width = 50;
            // 
            // F_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(618, 578);
            Controls.Add(GB_Variables);
            Controls.Add(GB_Scanning);
            Controls.Add(DG_Variables);
            Controls.Add(TMS_TopMenu);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.White;
            MainMenuStrip = TMS_TopMenu;
            Name = "F_Main";
            Text = "``";
            TMS_TopMenu.ResumeLayout(false);
            TMS_TopMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DG_Variables).EndInit();
            GB_Scanning.ResumeLayout(false);
            GB_Scanning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NUD_AverageFPS).EndInit();
            GB_Variables.ResumeLayout(false);
            GB_Variables.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TMS_TopMenu;
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
        private GroupBox GB_Scanning;
        private Button button1;
        private GroupBox GB_Variables;
        private Button BT_AddManualAddress;
        private Label label3;
        private NumericUpDown NUD_AverageFPS;
        private CheckBox CB_PauseScanning;
        private DataGridViewTextBoxColumn V_address;
        private DataGridViewTextBoxColumn V_name;
        private DataGridViewTextBoxColumn V_value;
        private DataGridViewButtonColumn V_BT_name;
    }
}
