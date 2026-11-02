using FX_Core;
using FX_Forms.Styles;

namespace FX_Forms
{
    public partial class F_AddressList : Form
    {
        public FloatVariable returnFV;
        List<FloatVariable> varList;

        private readonly System.Windows.Forms.Timer dgUpdateTimer = new() { Interval = 16 }; // ~60 fps

        public F_AddressList(FloatVariable[] fv, string windowTitle)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            this.Text = windowTitle ?? "Process Selection";
            varList = fv.ToList();
            BT_Select.Click += (_, _) => SelectAddress();
            DG_AddressList.KeyDown += (object s, KeyEventArgs e) => { if (e.KeyCode == Keys.Enter) SelectAddress(); };
            DG_AddressList.CellContentClick += OnDGClicked;

            FillDataGrid();
            dgUpdateTimer.Tick += (_, _) => UpdateDataGrid();
            dgUpdateTimer.Start();

            //this.FormClosing += (object sender, FormClosingEventArgs e) => { e.Cancel = e.CloseReason == CloseReason.UserClosing; };
        }

        void OnDGClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex == 0)
            {
                FloatVariable tempfv = GetFVFromCell(DG_AddressList.Rows[e.RowIndex].Cells[0]);
                FormUtils.OpenChangeValueDialog(ref tempfv, this);
                UpdateDataGrid();
            }
        }

        void FillDataGrid()
        {
            foreach (FloatVariable fv in varList)
            { DG_AddressList.Rows.Add(Shared.PtrStr(fv.Address), fv.Value, fv.Value); }
        }

        void UpdateDataGrid()
        {
            foreach(DataGridViewRow row in DG_AddressList.Rows)
            { row.Cells[2].Value = GetFVFromCell(row.Cells[0]).Value; }
        }

        void SelectAddress()
        {
            returnFV = GetFVFromCell(DG_AddressList.SelectedCells[0]);
            this.Close();
        }

        FloatVariable GetFVFromCell(DataGridViewCell cell)
        { return varList.Find(v => v.Address == Shared.StringToPtr((string)cell.Value)); }
    }
}