using FX_Forms.Styles;

namespace FX_Forms.Forms
{
    partial class F_ProcessSelector
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DG_ProcessList = new KatDataGridView();
            P_Img = new DataGridViewImageColumn();
            P_ID = new DataGridViewTextBoxColumn();
            P_Name = new DataGridViewTextBoxColumn();
            P_Path = new DataGridViewTextBoxColumn();
            BT_Cancel = new Button();
            BT_Select = new Button();
            TB_Search = new TextBox();
            ((System.ComponentModel.ISupportInitialize)DG_ProcessList).BeginInit();
            SuspendLayout();
            // 
            // DG_ProcessList
            // 
            DG_ProcessList.AllowUserToAddRows = false;
            DG_ProcessList.AllowUserToDeleteRows = false;
            DG_ProcessList.AllowUserToResizeRows = false;
            DG_ProcessList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DG_ProcessList.BackgroundColor = Color.FromArgb(48, 48, 48);
            DG_ProcessList.BorderStyle = BorderStyle.None;
            DG_ProcessList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle3.ForeColor = Color.White;
            DG_ProcessList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DG_ProcessList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DG_ProcessList.Columns.AddRange(new DataGridViewColumn[] { P_Img, P_ID, P_Name, P_Path });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            DG_ProcessList.DefaultCellStyle = dataGridViewCellStyle4;
            DG_ProcessList.EnableHeadersVisualStyles = false;
            DG_ProcessList.Location = new Point(12, 12);
            DG_ProcessList.MultiSelect = false;
            DG_ProcessList.Name = "DG_ProcessList";
            DG_ProcessList.ReadOnly = true;
            DG_ProcessList.RowHeadersVisible = false;
            DG_ProcessList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DG_ProcessList.Size = new Size(254, 514);
            DG_ProcessList.TabIndex = 0;
            // 
            // P_Img
            // 
            P_Img.HeaderText = "";
            P_Img.ImageLayout = DataGridViewImageCellLayout.Zoom;
            P_Img.Name = "P_Img";
            P_Img.ReadOnly = true;
            P_Img.Width = 30;
            // 
            // P_ID
            // 
            P_ID.HeaderText = "PID";
            P_ID.Name = "P_ID";
            P_ID.ReadOnly = true;
            P_ID.Width = 40;
            // 
            // P_Name
            // 
            P_Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            P_Name.HeaderText = "Name";
            P_Name.Name = "P_Name";
            P_Name.ReadOnly = true;
            P_Name.Width = 140;
            // 
            // P_Path
            // 
            P_Path.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            P_Path.HeaderText = "Path";
            P_Path.Name = "P_Path";
            P_Path.ReadOnly = true;
            // 
            // BT_Cancel
            // 
            BT_Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BT_Cancel.Location = new Point(12, 561);
            BT_Cancel.Name = "BT_Cancel";
            BT_Cancel.Size = new Size(125, 27);
            BT_Cancel.TabIndex = 2;
            BT_Cancel.Text = "Cancel";
            BT_Cancel.UseVisualStyleBackColor = true;
            // 
            // BT_Select
            // 
            BT_Select.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BT_Select.Location = new Point(141, 561);
            BT_Select.Name = "BT_Select";
            BT_Select.Size = new Size(125, 27);
            BT_Select.TabIndex = 1;
            BT_Select.Text = "Select";
            BT_Select.UseVisualStyleBackColor = true;
            // 
            // TB_Search
            // 
            TB_Search.BackColor = Color.FromArgb(32, 32, 32);
            TB_Search.ForeColor = Color.White;
            TB_Search.Location = new Point(12, 532);
            TB_Search.Name = "TB_Search";
            TB_Search.Size = new Size(254, 23);
            TB_Search.TabIndex = 3;
            // 
            // F_ProcessSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(278, 600);
            Controls.Add(TB_Search);
            Controls.Add(BT_Cancel);
            Controls.Add(BT_Select);
            Controls.Add(DG_ProcessList);
            Name = "F_ProcessSelector";
            Text = "F_ProcessSelector";
            ((System.ComponentModel.ISupportInitialize)DG_ProcessList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BT_Cancel;
        private Button BT_Select;
        private DataGridViewImageColumn P_Img;
        private DataGridViewTextBoxColumn P_ID;
        private DataGridViewTextBoxColumn P_Name;
        private DataGridViewTextBoxColumn P_Path;
        private KatDataGridView DG_ProcessList;
        private TextBox TB_Search;
    }
}