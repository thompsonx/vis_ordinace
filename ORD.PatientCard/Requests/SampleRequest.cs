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
        private DateTime mProcessedDate;

        public SampleRequest()
        {
            this.results = null;
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

        public DateTime ProcessedDate 
        {
            get { return this.mProcessedDate; }
            set { this.mProcessedDate = value; }
        }

    }
}
