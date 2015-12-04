using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Database
{
    public abstract class Database : IDatabase
    {
        protected DbConnection mConnection;
        protected DbTransaction mSqlTransaction = null;
        private String mLanguage = "en";
        protected string CONNECTION_STRING = "";
        protected string message = "";

        public bool Connect(string conString)
        {
            if (mConnection.State != System.Data.ConnectionState.Open)
            {
                mConnection.ConnectionString = conString;
                try
                {
                    mConnection.Open();
                }
                catch (Exception e)
                {
                    throw new ApplicationException(ErrorMessages.Messages["DB_conn"] + " : " + e.Message);
                }
            }
            return true;
        }

        public bool Connect()
        {
            bool ret = true;

            if (mConnection.State != System.Data.ConnectionState.Open)
            {
                ret = Connect(CONNECTION_STRING);
            }

            return ret;
        }

        public void Close()
        {
            try
            {
                mConnection.Close();
            }
            catch (Exception e)
            {
                throw new ApplicationException(ErrorMessages.Messages["DB_close"] + " : " + e.Message);
            }
        }

        public void BeginTransaction()
        {
            mSqlTransaction = mConnection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void EndTransaction()
        {
            mSqlTransaction.Commit();
            Close();
        }

        public void Rollback()
        {
            mSqlTransaction.Rollback();
        }

        public int ExecuteNonQuery(System.Data.Common.DbCommand command)
        {
            int rowNumber = 0;
            try
            {
                command.Prepare();
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return rowNumber;
        }

        public abstract int ExecuteScalar(System.Data.Common.DbCommand command);

        public abstract System.Data.Common.DbCommand CreateCommand(string strCommand);

        public System.Data.Common.DbDataReader Select(System.Data.Common.DbCommand command)
        {
            command.Prepare();
            DbDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }

        public abstract string ProcedureWithMessage(System.Data.Common.DbCommand command);

        public abstract DbParameter CreateParameter(string name, string type, int length = 0);

        public string Language
        {
            get
            {
                return this.mLanguage;
            }
            set
            {
                this.mLanguage = value;
            }
        }
    }
}
