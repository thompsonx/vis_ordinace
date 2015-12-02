using ORD.B.PatientServices;
using ORD.PatientCard;
using ORD.PatientCard.Requests;
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
    public partial class RequestList : Form
    {
        public RequestList()
        {
            InitializeComponent();
            PatientCardService pcs = new PatientCardService();
            Patient p = new Patient();
            p.ID = "7509223123";
            List<Prescription> prs = pcs.getPatientPrescriptions(p);
            prs.ForEach(x => prescriptionBindingSource.Add(x));
            //MessageBox.Show(this.prescriptionList.Rows.Count.ToString());
            this.prescriptionList.RowsAdded += prescriptionList_RowsAdded;
            //for (int i = 0; i < prs.Count; i++)
            //{
            //    this.prescriptionList.Rows[i].Cells[1].Value = prs[i].MedicinesToString();
            //}
        }

        void prescriptionList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Prescription p = (Prescription)this.prescriptionBindingSource[e.RowIndex];
            this.prescriptionList.Rows[e.RowIndex].Cells[1].Value = p.MedicinesToString();
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
        }
    }
}
