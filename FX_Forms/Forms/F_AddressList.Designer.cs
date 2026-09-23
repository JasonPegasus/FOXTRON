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
            DG_AddressList = new DataGridView();
            address = new DataGridViewTextBoxColumn();
            ogValue = new DataGridViewTextBoxColumn();
            value = new DataGridViewTextBoxColumn();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)DG_AddressList).BeginInit();
            SuspendLayout();
            // 
            // DG_AddressList
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle1.ForeColor = Color.White;
            DG_AddressList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DG_AddressList.BackgroundColor = Color.FromArgb(30, 30, 30);
            DG_AddressList.BorderStyle = BorderStyle.None;
            DG_AddressList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DG_AddressList.Columns.AddRange(new DataGridViewColumn[] { address, ogValue, value });
            DG_AddressList.Location = new Point(12, 12);
            DG_AddressList.Name = "DG_AddressList";
            DG_AddressList.RowHeadersVisible = false;
            DG_AddressList.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            DG_AddressList.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
            DG_AddressList.ScrollBars = ScrollBars.Vertical;
            DG_AddressList.Size = new Size(376, 506);
            DG_AddressList.TabIndex = 0;
            // 
            // address
            // 
            address.HeaderText = "Address";
            address.Name = "address";
            address.ReadOnly = true;
            // 
            // ogValue
            // 
            ogValue.HeaderText = "OG Value";
            ogValue.Name = "ogValue";
            ogValue.ReadOnly = true;
            ogValue.Width = 110;
            // 
            // value
            // 
            value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            value.HeaderText = "Value";
            value.Name = "value";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(30, 30, 30);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(12, 524);
            button1.Name = "button1";
            button1.Size = new Size(86, 33);
            button1.TabIndex = 1;
            button1.Text = "Select";
            button1.UseVisualStyleBackColor = false;
            // 
            // F_AddressList
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(394, 567);
            Controls.Add(button1);
            Controls.Add(DG_AddressList);
            Font = new Font("Segoe UI", 13F);
            ForeColor = Color.White;
            Margin = new Padding(4, 5, 4, 5);
            Name = "F_AddressList";
            Text = "F_AddressList";
            ((System.ComponentModel.ISupportInitialize)DG_AddressList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DG_AddressList;
        private DataGridViewTextBoxColumn address;
        private DataGridViewTextBoxColumn ogValue;
        private DataGridViewTextBoxColumn value;
        private Button button1;
    }
}