using ORD.Medicines;
using ORD.Strings;
using System;
using System.Collections;
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
    public partial class RequestForm : Form
    {

        private DateTime date;
        private MedicineSelection mForm;
        public RequestForm()
        {
            InitializeComponent();
            this.date = DateTime.Now;
            this.tbDate.Text = date.ToString();
        }

        public DateTime Date 
        {
            get { return this.date; }
        }

        public List<Medicine> Medicines
        {
            get 
            {
                List<Medicine> m = new List<Medicine>();
                foreach (object o in this.medicineBindingSource.List)
                {
                    m.Add((Medicine)o);
                }
                return m; 
            }
        }

        private void bAddMedicines_Click(object sender, EventArgs e)
        {
            this.mForm = new MedicineSelection();
            if (mForm.ShowDialog(this) == DialogResult.OK)
            {
                mForm.SelectedMedicines.ForEach(x => this.medicineBindingSource.Add(x));
            }
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            if (this.medicineBindingSource.Count == 0)
            {
                MessageBox.Show(ErrorMessages.GUI_WF_RF_empty);
            }
            else
            {
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
        }
    }
}
