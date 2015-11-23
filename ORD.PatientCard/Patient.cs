using ORD.HealthInsurances;
using ORD.PatientCard.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard
{
   public class Patient
    {
        private string mID;
        private string mSurname;
        private string mName;
        private HealthInsurance mInsurance;
        private string mStreet;
        private string mTown;
        private int mZipCode;
        private int mPhoneNumber;

        private List<Request> requests;

        public Patient()
        {
            this.requests = new List<Request>();
        }

        public string ID
        {
            get { return this.mID; }
            set { this.mID = value; }
        }

        public string Surname
        {
            get { return this.mSurname; }
            set { this.mSurname = value; }
        }

        public string Name
        {
            get { return this.mName; }
            set { this.mName = value; }
        }

        public HealthInsurance Insurance
        {
            get { return this.mInsurance; }
            set { this.mInsurance = value; }
        }

        public string Street
        {
            get { return this.mStreet; }
            set { this.mStreet = value; }
        }

        public string Town
        {
            get { return this.mTown; }
            set { this.mTown = value; }
        }

        public int ZipCode
        {
            get { return this.mZipCode; }
            set { this.mZipCode = value; }
        }

        public int PhoneNumber
        {
            get { return this.mPhoneNumber; }
            set { this.mPhoneNumber = value; }
        }

        public List<Request> Requests 
        {
            get { return this.requests; }
            set { this.requests = value; }
        }
    }
}
