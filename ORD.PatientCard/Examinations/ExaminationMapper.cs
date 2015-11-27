using ORD.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Examinations
{
    class ExaminationMapper
    {
        private static string sqlINSERT = "INSERT INTO Examinations VALUES (@examined, @diagnosis, @type, @paid)";
        private static string sqlUPDATE = "UPDATE Examinations SET examined = @examined, diagnosis = @diagnosis, type = @type " +
            "paid = @paid WHERE id = @id";
        private static string sqlDELETE = "DELETE FROM Examinations WHERE id = @id";
        private static string sqlSELECT = "SELECT * FROM Examinations WHERE person_id = @id ORDER BY examined DESC";
        private void PrepareCommand(IDatabase db, DbCommand cmd, Examination er)
        {
            cmd.Parameters.Add(db.CreateParameter("@examined", "datetime"));
            cmd.Parameters["@examined"].Value = er.Examined;

            cmd.Parameters.Add(db.CreateParameter("@diagnosis", "varchar", er.Diagnosis.Length));
            cmd.Parameters["@diagnosis"].Value = er.Diagnosis;

            cmd.Parameters.Add(db.CreateParameter("@type", "int"));
            cmd.Parameters["@type"].Value = er.Type.Id;

            cmd.Parameters.Add(db.CreateParameter("@paid", "char", 1));
            cmd.Parameters["@paid"].Value = er.Paid;
        }
        public void Insert(string p_id, Examination e)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, e);
            e.Id = db.ExecuteScalar(command);

            db.Close();
        }

        public void Update(Examination e)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlUPDATE);
            PrepareCommand(db, command, e);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = e.Id;
            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void Delete(Examination e)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = e.Id;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public List<Examination> Select(string p_id)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECT);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            DbDataReader reader = db.Select(command);

            List<Examination> e = this.Read(reader);

            reader.Close();
            db.Close();

            return e;
        }

        private List<Examination> Read(DbDataReader reader)
        {
            List<Examination> examinations = new List<Examination>();

            while (reader.Read())
            {
                Examination e = new Examination();
                e.Id = reader.GetInt32(0);
                e.Examined = reader.GetDateTime(1);
                e.Diagnosis = reader.GetString(2);
                int type = reader.GetInt32(3);
                ExaminationTypeMapper etm = new ExaminationTypeMapper();
                e.Type = etm.Find(type);
                e.Paid = reader.GetChar(4);

                examinations.Add(e);
            }

            return examinations;
        }
    }
}
