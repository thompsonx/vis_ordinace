using ORD.Medicines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    public class Prescription : Request
    {
        //TODO: vytisk receptu

        private List<Medicine> mMedicines;

        public Prescription()
        {
            this.mMedicines = new List<Medicine>();
        }

        public List<Medicine> Medicines 
        {
            get { return this.mMedicines; }
            set { this.mMedicines = value; }
        }
    }
}
