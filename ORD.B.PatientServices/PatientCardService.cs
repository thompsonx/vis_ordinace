using ORD.PatientCard;
using ORD.PatientCard.Requests;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.B.PatientServices
{
    public class PatientCardService
    {

        private PatientMapper pm;

        public PatientCardService()
        {
            this.pm = new PatientMapper();
        }
        public List<Patient> GetPatients()
        {
            return this.pm.SelectAll();
        }

        public void RegisterPatient(Patient p)
        {
            this.pm.Insert(p);
        }
        public List<Prescription> GetPatientPrescriptions(Patient p)
        {
            List<Prescription> prs = new List<Prescription>();
            foreach (Request r in pm.SelectRequests(p, "prescription"))
            {
                prs.Add((Prescription)r);
            }
            return prs;
        }

        public void AddPrescription(Patient p, Prescription pres)
        {
            pm.InsertRequest(p, pres);
        }

        public void DeletePatient(Patient p)
        {
            this.pm.Delete(p);
        }

        public bool isId(string id)
        {
            if (id.Length != 10)
                return false;
            foreach (char c in id)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public DateTime CreateBirthDateFromId (Patient p)
        {
            StringBuilder birth = new StringBuilder();
            DateTime birthdate;

            string errmsg = String.Format(" ID {0}: {1} {2}", p.ID, p.Surname, p.Name);

            if (p.ID.Length < 9 || p.ID.Length > 10)
                throw new ApplicationException(ErrorMessages.SERVICE_P_id_format + errmsg);

            //Day
            birth.Append(p.ID.Substring(4, 2)).Append('.');

            //Month
            int gender;
            if (Int32.TryParse(p.ID.Substring(2, 1), out gender))
            {
                if (gender == 5)
                {
                    birth.Append(p.ID.Substring(3, 1));
                }
                else if (gender == 6)
                {
                    birth.Append('1').Append(p.ID.Substring(3, 1));
                }
                else
                {
                    birth.Append(p.ID.Substring(2, 2));
                }
            }
            else
            {
                throw new ApplicationException(ErrorMessages.SERVICE_P_id_month + errmsg);
            }
            birth.Append(".");

            //Year
            string year = p.ID.Substring(0, 2);
            int y;
            if (!Int32.TryParse(year, out y))
            {
                throw new ApplicationException(ErrorMessages.SERVICE_P_id_year + errmsg);
            }
            if (p.ID.Length == 9 || (p.ID.Length == 10 && y >= 54))
            {
                birth.Append("19").Append(year);
            }
            else
            {
                birth.Append("20").Append(year);
            }

            if (!DateTime.TryParse(birth.ToString(), out birthdate))
            {
                throw new ApplicationException(ErrorMessages.SERVICE_P_id_date + errmsg);
            }
            return birthdate;
        }
    }
}
