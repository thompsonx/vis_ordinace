using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Database.Mappers
{
    public interface IDbMapper<T>
    {
        void Insert(T subject);
        void Update(T subject);
        void Delete(T subject);
        List<T> SelectAll();
    }
}
