using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Repository
{
    public interface IRepository<T,K>
    {
        IList<T> Load();
        T Find(K key);
    }
}
