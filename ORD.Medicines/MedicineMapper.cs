using ORD.Repository;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ORD.Medicines
{
    public class MedicineMapper
    {
        private static MedicineMapper mapper = null;

        public static MedicineMapper GetInstance()
        {
            if (mapper == null)
                mapper = new MedicineMapper();
            return mapper;
        }

        private Dictionary<int, Medicine> medicines;
        private IMedicineRepository repository;

        private MedicineMapper()
        {
            this.medicines = new Dictionary<int,Medicine>();
            //this.repository = new MedicineRepository(MedicineRepository.DSOURCE.XML);
            this.repository = new MedicineRepository(new XmlDataSource());
        }

        private void Add(Medicine m)
        {
            this.medicines.Add(m.Id, m);
        }

        public IList<Medicine> Medicines
        {
            get
            {
                return this.medicines.Values.ToList();
            }
        }

        public IList<Medicine> LoadMedicines()
        {
            this.medicines = this.repository.LoadAsDict();
            return this.medicines.Values.ToList();
        }

        public Medicine Find(int id)
        {
            if (this.medicines.ContainsKey(id))
                return this.medicines[id];
            else
                return this.repository.Find(id);
        }
    }
}
