using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    public class SampleRequest : Request
    {
        private string mType;
        private string mDescription;
        private string results;

        public SampleRequest(string type, string description)
        {
            this.results = null;
            this.mDescription = description;
            this.mType = type;
        }

        public string Type
        {
            get { return this.mType; }
            set { this.mType = value; }
        }

        public string Description 
        {
            get { return this.mDescription; }
            set { this.mDescription = value; }
        }

        public string Results
        {
            get { return this.results; }
            set { this.results = value; }
        }

    }
}
