using System.Windows.Forms;

namespace FX_Forms.Styles
{
    public class KatDataGridView : DataGridView
    {
        public KatDataGridView() { }

        protected override void OnHandleCreated(EventArgs e)
        {
            this.EnableHeadersVisualStyles = false;
            this.RowHeadersVisible = false;

            this.BackgroundColor = Color.FromArgb(48, 48, 48);
            this.DefaultCellStyle = KatCellStyle;
            this.ColumnHeadersDefaultCellStyle = KatCellStyle;
            this.BorderStyle = BorderStyle.None;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.AllowUserToResizeRows = false;
            this.AllowUserToAddRows = false;
            this.DefaultCellStyle.SelectionBackColor = Color.Gray;
            this.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Gray;
            this.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column is DataGridViewButtonColumn btn)
                {
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.DefaultCellStyle.BackColor = Color.FromArgb(32,32,32);
                    btn.DefaultCellStyle.ForeColor = Color.White;
                    btn.Text = "[•]";
                }
            }
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            foreach (var cell in this.Rows[e.RowIndex].Cells)
            {
                if (cell is DataGridViewButtonCell btn)
                {
                }
            }
        }

        DataGridViewCellStyle KatCellStyle = new DataGridViewCellStyle(){
                 BackColor = Color.FromArgb(32, 32, 32),
                 ForeColor = Color.White,
             };
    }
}
