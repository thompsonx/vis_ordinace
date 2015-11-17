using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Examinations
{
    public class ExaminationPrice
    {
        private int id;
        private HealthInsurances.HealthInsurance insurance;
        private float price;
        public ExaminationPrice(HealthInsurances.HealthInsurance insurance, float price)
        {
            this.insurance = insurance;
            this.price = price;
        }

        public int Id 
        { 
            get { return this.id; } 
            set { this.id = value; } 
        }

        public HealthInsurances.HealthInsurance HealthInsurance {
            get { return this.insurance; }
        }

        public float Price 
        { 
            get { return this.price; } 
            set { this.price = value; } 
        }

    }
}
