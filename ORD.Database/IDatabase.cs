using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Database
{
    public interface IDatabase
    {
        bool Connect(String conString);

        /**
         * Connect.
         **/
        bool Connect();

        /**
         * Close.
         **/
        void Close();

        /**
         * Begin a transaction.
         **/
        void BeginTransaction();

        /**
         * End a transaction.
         **/
        void EndTransaction();

        /**
         * If a transaction is failed call it.
         **/
        void Rollback();

        /**
         * Insert a record encapulated in the command.
         **/
        int ExecuteNonQuery(DbCommand command);

        int ExecuteScalar(DbCommand command);

        /**
         * Create command.
         **/
        DbCommand CreateCommand(string strCommand);

        /**
         * Select encapulated in the command.
         **/
        DbDataReader Select(DbCommand command);

        string ProcedureWithMessage(DbCommand command);

        DbParameter CreateParameter(string name, string type, int length = 0);

        String Language
        {
            get;
            set;
        }
    }
}