using ORD.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    class ExaminationRequestMapper
    {
        private static string sqlINSERT = "INSERT INTO ExaminationRequests VALUES (@id, @type, @description, @date)";
        private static string sqlUPDATE = "UPDATE ExaminationRequests SET type = @type, description = @description, " +
            "date = @date WHERE id = @id";
        private static string sqlDELETE = "DELETE FROM ExaminationRequests WHERE id = @id";
        private static string sqlSELECT = "SELECT * FROM ExaminationRequests WHERE person_id = @id ORDER BY date DESC";
        private void PrepareCommand(IDatabase db, DbCommand cmd, ExaminationRequest er)
        {
            cmd.Parameters.Add(db.CreateParameter("@id", "int"));
            cmd.Parameters["@id"].Value = er.Id;

            cmd.Parameters.Add(db.CreateParameter("@type", "varchar", er.Type.Length));
            cmd.Parameters["@type"].Value = er.Type;

            cmd.Parameters.Add(db.CreateParameter("@description", "varchar", er.Description.Length));
            cmd.Parameters["@description"].Value = er.Description;

            cmd.Parameters.Add(db.CreateParameter("@date", "datetime"));
            cmd.Parameters["@date"].Value = er.Created;

        }
        public void InsertRequest(string p_id, Request r)
        {
            ExaminationRequest er = (ExaminationRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, er);
            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void UpdateRequest(Request r)
        {
            ExaminationRequest er = (ExaminationRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlUPDATE);
            PrepareCommand(db, command, er);
            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void DeleteRequest(Request r)
        {
            ExaminationRequest er = (ExaminationRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = er.Id;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public List<Request> SelectRequests(string p_id)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECT);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            DbDataReader reader = db.Select(command);

            List<Request> p = this.Read(reader);

            reader.Close();
            db.Close();

            return p;
        }

        private List<Request> Read(DbDataReader reader)
        {
            List<Request> requests = new List<Request>();

            while (reader.Read())
            {
                ExaminationRequest er = new ExaminationRequest();
                er.Id = reader.GetInt32(0);
                er.Type = reader.GetString(1);
                er.Description = reader.GetString(2);
                er.Created = reader.GetDateTime(3);

                requests.Add(er);
            }

            return requests;
        }
    }
}
