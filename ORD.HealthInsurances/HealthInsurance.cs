using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.HealthInsurances
{
    public class HealthInsurance
    {
        private int mCode;
        private string mName;
        private string mStreet;
        private string mTown;
        private int mZipCode;
        private int mPhoneNumber;

        public const int LEN_NAME = 50;
        public const int LEN_STREET = 50;
        public const int LEN_TOWN = 50;

        public int Code
        {
            get { return this.mCode; }
            set { this.mCode = value; }
        }

        public string Name
        {
            get { return this.mName; }
            set { this.mName = value; }
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
