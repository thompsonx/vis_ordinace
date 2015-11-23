using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    public class ExaminationRequest : Request
    {
        private string mSpecialistType;
        private string mDescription;

        public string Type
        {
            get { return this.mSpecialistType; }
            set { this.mSpecialistType = value; }
        }

        public string Description 
        {
            get { return this.mDescription; }
            set { this.mDescription = value; }
        }
    }
}
