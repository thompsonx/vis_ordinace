using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Examinations
{
    public class ExaminationType
    {
        private int id;
        private List<ExaminationPrice> prices;
        private string mName;
        private string mDescription;

        public ExaminationType()
        {
            this.prices = new List<ExaminationPrice>();
            this.mName = null;
            this.mDescription = null;
        }

        public int Id 
        { 
            get { return this.id; } 
            set { this.id = value; } 
        }

        public string Name
        {
            get { return this.mName; }
            set { this.mName = value; }
        }

        public string Description
        {
            get { return this.mDescription; }
            set { this.mDescription = value; }
        }

        public List<ExaminationPrice> Prices
        {
            get { return this.prices; }
            set { this.prices = value; }
        }
    }
}
