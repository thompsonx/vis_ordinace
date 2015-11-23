using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    public abstract class Request
    {
        private int id;
        private DateTime mCreated;

        public DateTime Created 
        {
            get { return this.mCreated; }
            set { this.mCreated = value; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
