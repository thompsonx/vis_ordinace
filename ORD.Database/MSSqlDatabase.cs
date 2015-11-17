using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Database
{
    public class MSSqlDatabase : Database
    {
        public MSSqlDatabase()
        {
            this.mConnection = new SqlConnection();
            this.CONNECTION_STRING = "server=localhost;database=VIS;user=vis;password=vis2015;";
        }

        public override int ExecuteScalar(System.Data.Common.DbCommand command)
        {
            command.CommandText = command.CommandText + "; SELECT CONVERT(INT, SCOPE_IDENTITY())";

            int lastid = 0;

            try
            {
                command.Prepare();
                lastid = (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }

            return lastid;
        }

        public override System.Data.Common.DbCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, (SqlConnection)mConnection);

            if (mSqlTransaction != null)
            {
                command.Transaction = (SqlTransaction)mSqlTransaction;
            }
            return command;
        }

        public override string ProcedureWithMessage(System.Data.Common.DbCommand command)
        {
            this.message = "";
            SqlConnection c = (SqlConnection)this.mConnection;
            c.InfoMessage += this.CatchMessage;

            command.CommandType = CommandType.StoredProcedure;

            int ret = this.ExecuteNonQuery(command);

            c.InfoMessage -= this.CatchMessage;

            return this.message;
        }

        private void CatchMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.message += e.Message + Environment.NewLine;
        }
    }
}
