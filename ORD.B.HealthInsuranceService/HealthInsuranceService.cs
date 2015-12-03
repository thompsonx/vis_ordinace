using ORD.HealthInsurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.B.HealthInsuranceService
{
    public class HealthInsuranceService
    {
        public List<HealthInsurance> GetInsurances()
        {
            return HealthInsuranceMapper.GetInstance().SelectAll();
        }

        public HealthInsurance Find(int code)
        {
            return HealthInsuranceMapper.GetInstance().Find(code);
        }
    }
}
