namespace FX_Forms
{
    partial class F_AddressList
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            BT_Select = new Button();
            DG_AddressList = new Styles.KatDataGridView();
            c_address = new DataGridViewTextBoxColumn();
            c_ogvalue = new DataGridViewTextBoxColumn();
            c_currentValue = new DataGridViewTextBoxColumn();
            c_BTN_changeValue = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)DG_AddressList).BeginInit();
            SuspendLayout();
            // 
            // BT_Select
            // 
            BT_Select.BackColor = Color.FromArgb(30, 30, 30);
            BT_Select.FlatStyle = FlatStyle.Popup;
            BT_Select.Location = new Point(12, 524);
            BT_Select.Name = "BT_Select";
            BT_Select.Size = new Size(86, 33);
            BT_Select.TabIndex = 1;
            BT_Select.Text = "Select";
            BT_Select.UseVisualStyleBackColor = false;
            // 
            // DG_AddressList
            // 
            DG_AddressList.AllowUserToAddRows = false;
            DG_AddressList.AllowUserToDeleteRows = false;
            DG_AddressList.AllowUserToResizeRows = false;
            DG_AddressList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DG_AddressList.BackgroundColor = Color.FromArgb(48, 48, 48);
            DG_AddressList.BorderStyle = BorderStyle.None;
            DG_AddressList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle1.ForeColor = Color.White;
            DG_AddressList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DG_AddressList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DG_AddressList.Columns.AddRange(new DataGridViewColumn[] { c_address, c_ogvalue, c_currentValue, c_BTN_changeValue });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(32, 32, 32);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 13F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DG_AddressList.DefaultCellStyle = dataGridViewCellStyle2;
            DG_AddressList.EnableHeadersVisualStyles = false;
            DG_AddressList.Location = new Point(12, 12);
            DG_AddressList.MultiSelect = false;
            DG_AddressList.Name = "DG_AddressList";
            DG_AddressList.RowHeadersVisible = false;
            DG_AddressList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DG_AddressList.Size = new Size(403, 506);
            DG_AddressList.TabIndex = 2;
            // 
            // c_address
            // 
            c_address.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c_address.HeaderText = "Address";
            c_address.Name = "c_address";
            c_address.ReadOnly = true;
            // 
            // c_ogvalue
            // 
            c_ogvalue.HeaderText = "OG Value";
            c_ogvalue.Name = "c_ogvalue";
            c_ogvalue.ReadOnly = true;
            // 
            // c_currentValue
            // 
            c_currentValue.HeaderText = "Current Value";
            c_currentValue.Name = "c_currentValue";
            c_currentValue.ReadOnly = true;
            c_currentValue.Width = 150;
            // 
            // c_BTN_changeValue
            // 
            c_BTN_changeValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            c_BTN_changeValue.HeaderText = "Edit";
            c_BTN_changeValue.Name = "c_BTN_changeValue";
            c_BTN_changeValue.Width = 50;
            // 
            // F_AddressList
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(427, 570);
            Controls.Add(DG_AddressList);
            Controls.Add(BT_Select);
            Font = new Font("Segoe UI", 13F);
            ForeColor = Color.White;
            Margin = new Padding(4, 5, 4, 5);
            Name = "F_AddressList";
            Text = "F_AddressList";
            ((System.ComponentModel.ISupportInitialize)DG_AddressList).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button BT_Select;
        private Styles.KatDataGridView DG_AddressList;
        private DataGridViewTextBoxColumn c_address;
        private DataGridViewTextBoxColumn c_ogvalue;
        private DataGridViewTextBoxColumn c_currentValue;
        private DataGridViewButtonColumn c_BTN_changeValue;
    }
}