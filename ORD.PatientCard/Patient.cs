using ORD.HealthInsurances;
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

        public const int LEN_ID = 10;
        public const int LEN_SURNAME = 30;
        public const int LEN_NAME = 30;
        public const int LEN_STREET = 50;
        public const int LEN_TOWN = 50;

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
    }
}
