using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Strings
{
    public class ErrorMessages
    {
        public const string DB_conn = "Database connection error";
        public const string DB_close = "Database close error";
        public const string DB_param = "Unknown DB datatype";

        public const string REQ_type = "Unknown type (descendant) of Request";
        public const string REQ_P_medicines = "No medicines! Cannot insert empty prescription!";
    }
}
