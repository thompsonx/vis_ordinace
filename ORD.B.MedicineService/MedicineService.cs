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
        private IList<Medicine> medicines;

        public MedicineService()
        {
            this.medicines = null;
        }
        public IList<Medicine> getMedicines()
        {
            MedicineMapper mm = MedicineMapper.GetInstance();
            if (medicines == null)
            {
                mm.LoadMedicines();
                this.medicines = mm.Medicines;
            }
            
            return this.medicines;
        }
    }
}
