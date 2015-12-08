using ORD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Medicines
{
    class MedicineRepository : IMedicineRepository
    {
        private IMedicineDataSource dataSource;
        public MedicineRepository(IMedicineDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        //public MedicineRepository(DSOURCE dataSource)
        //{
        //    if (dataSource == DSOURCE.DB)
        //    {
        //        this.dataSource = new SqlDbDataSource();
        //    }
        //    else if (dataSource == DSOURCE.XML)
        //    {
        //        this.dataSource = new XmlDataSource();
        //    }
        //}
        public IList<Medicine> Load()
        {
            return this.dataSource.LoadAsDict().Values.ToList();
        }

        public Dictionary<int, Medicine> LoadAsDict()
        {
            return this.dataSource.LoadAsDict();
        }

        public Medicine Find(int key)
        {
            return this.dataSource.Find(key);
        }

        public enum DSOURCE
        {
            XML,
            DB
        }
    }
}
