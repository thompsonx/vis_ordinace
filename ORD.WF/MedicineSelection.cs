using ORD.B.MedicineService;
using ORD.Medicines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORD.WF
{
    public partial class MedicineSelection : Form
    {
        private IList<Medicine> all;
        private List<Medicine> selected;
        public MedicineSelection()
        {
            InitializeComponent();
            MedicineService ms = new MedicineService();
            this.all = ms.getMedicines();
            foreach (Medicine m in all)
            {
                this.medicineBindingSource.Add(m);
            }
            this.selected = new List<Medicine>();
        }

        public List<Medicine> SelectedMedicines 
        {
            get { return this.selected; }
        }

        private void bConfirm_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.medicinesGridView.Rows)
            {
                var checkbox = r.Cells[4].Value;
                if (checkbox != null)
                {
                    this.selected.Add(this.all[r.Index]);
                }
            }

            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void medicinesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var value = this.medicinesGridView.Rows[e.RowIndex].Cells[4].Value;
                if (value == null)
                    this.medicinesGridView.Rows[e.RowIndex].Cells[4].Value = true;
                else
                    this.medicinesGridView.Rows[e.RowIndex].Cells[4].Value = null;
            }
        }
    }
}
