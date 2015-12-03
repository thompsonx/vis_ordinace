using ORD.PatientCard;
using ORD.PatientCard.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.B.PatientServices
{
    public class PatientCardService
    {
        public List<Prescription> GetPatientPrescriptions(Patient p)
        {
            PatientMapper pm = new PatientMapper();
            List<Prescription> prs = new List<Prescription>();
            foreach (Request r in pm.SelectRequests(p, "prescription"))
            {
                prs.Add((Prescription)r);
            }
            return prs;
        }

        public void AddPrescription(Patient p, Prescription pres)
        {
            PatientMapper pm = new PatientMapper();
            pm.InsertRequest(p, pres);
        }
    }
}
