using ORD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Medicines
{
    interface IMedicineRepository : IRepository<Medicine, int>
    {
        Dictionary<int, Medicine> LoadAsDict();
    }
}
