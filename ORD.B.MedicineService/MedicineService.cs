using ORD.Medicines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.B.MedicineService
{
    public class MedicineService
    {
        public IList<Medicine> getMedicines()
        {
            MedicineMapper mm = MedicineMapper.GetInstance();
            return mm.Medicines;
        }
    }
}
