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
        private RequestForm rForm;
        private Patient patient;
        private PatientCardService service;
        public RequestList()
        {
            InitializeComponent();
            this.service = new PatientCardService();
            Patient p = new Patient();
            p.ID = "7509223123";
            this.patient = p;
            List<Prescription> prs;
            try
            {
                prs = service.GetPatientPrescriptions(patient);
                prs.ForEach(x => prescriptionBindingSource.Add(x));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            this.prescriptionList.RowsAdded += prescriptionList_RowsAdded;
            this.prescriptionList.UserDeletingRow += prescriptionList_UserDeletingRow;
        }

        void prescriptionList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //MessageBox.Show(((Prescription)this.prescriptionBindingSource[e.Row.Index]).MedicinesToString());
            this.service.RemovePrescription((Prescription)this.prescriptionBindingSource[e.Row.Index]);
        }

        void prescriptionList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                Prescription p = (Prescription)this.prescriptionBindingSource[i];
                this.prescriptionList.Rows[i].Cells[1].Value = p.MedicinesToString();
            }
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            this.rForm = new RequestForm();
            if (rForm.ShowDialog(this) == DialogResult.OK)
            {
                Prescription p = new Prescription();
                p.Created = rForm.Date;
                rForm.Medicines.ForEach(m => p.Add(m));
                try
                {
                    this.service.AddPrescription(this.patient, p);
                    this.prescriptionBindingSource.Insert(0, p);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
