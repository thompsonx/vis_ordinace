using ORD.B.PatientServices;
using ORD.PatientCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Console
{
    class PatientManager
    {
        private PatientCardService pcs;
        public PatientManager()
        {
            this.pcs = new PatientCardService();
            foreach (Patient p in pcs.GetPatients())
            {

            }
        }
    }
}
