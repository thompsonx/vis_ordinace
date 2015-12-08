using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Medicines
{
    interface IMedicineDataSource
    {
        Dictionary<int, Medicine> LoadAsDict();
        Medicine Find(int id);
    }
}
