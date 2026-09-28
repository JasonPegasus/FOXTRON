using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }



        DataGridViewCellStyle KatCellStyle = new DataGridViewCellStyle(){
                 BackColor = Color.FromArgb(32, 32, 32),
                 ForeColor = Color.White,
             };
    }
}
