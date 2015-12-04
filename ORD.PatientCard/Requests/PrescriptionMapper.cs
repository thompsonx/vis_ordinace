using ORD.Database;
using ORD.Medicines;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    class PrescriptionMapper
    {
        private static string sqlINSERT = "INSERT INTO Prescriptions (date, person_id) VALUES (@date, @person)";
        private static string sqlINSERTMEDICINE = "INSERT INTO Prescribed VALUES (@id, @medicineid)";
        private static string sqlUPDATE = "UPDATE Prescriptions SET date = @date WHERE id = @id";
        private static string sqlDELETEMEDICINES = "DELETE FROM Prescribed WHERE id_p = @id";
        private static string sqlDELETE = "DELETE FROM Prescriptions WHERE id = @id";
        private static string sqlDELETEPATIENT1 = "DELETE FROM Prescribed WHERE id_p IN (SELECT id FROM Prescriptions WHERE person_id = @id)";
        private static string sqlDELETEPATIENT2 = "DELETE FROM Prescriptions WHERE person_id = @id";
        private static string sqlSELECT = "SELECT * FROM Prescriptions WHERE person_id = @id ORDER BY date DESC";
        private static string sqlSELECTMEDICINE = "SELECT id_medicine FROM Prescribed WHERE id_p = @id";
        private void PrepareCommand(IDatabase db, DbCommand cmd, Prescription er, string p_id)
        {
            cmd.Parameters.Add(db.CreateParameter("@date", "datetime"));
            cmd.Parameters["@date"].Value = er.Created;

            cmd.Parameters.Add(db.CreateParameter("@person", "char", p_id.Length));
            cmd.Parameters["@person"].Value = p_id;

        }
        public void InsertRequest(string p_id, Request r)
        {
            Prescription er = (Prescription)r;
            if (er.Medicines.Count == 0)
            {
                throw new ApplicationException(ErrorMessages.Messages["REQ_P_medicines"]);
            }

            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, er, p_id);
            er.Id = db.ExecuteScalar(command);

            int added = this.InsertMedicines(er, db);

            db.EndTransaction();

            if (added != er.Medicines.Count)
            {
                db.Rollback();
            }

            db.Close();
        }

        private int InsertMedicines(Prescription p, IDatabase db)
        {
            int added = 0;
            DbCommand command;

            foreach (Medicine m in p.Medicines)
            {
                command = db.CreateCommand(sqlINSERTMEDICINE);
                command.Parameters.Add(db.CreateParameter("@id", "int"));
                command.Parameters["@id"].Value = p.Id;

                command.Parameters.Add(db.CreateParameter("@medicineid", "int"));
                command.Parameters["@medicineid"].Value = m.Id;

                added += db.ExecuteNonQuery(command);
            }

            return added;
        }

        public void UpdateRequest(Request r)
        {
            Prescription er = (Prescription)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            DbCommand command = db.CreateCommand(sqlUPDATE);
            command.Parameters.Add(db.CreateParameter("@date", "datetime"));
            command.Parameters["@date"].Value = er.Created;
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = er.Id;
            db.ExecuteNonQuery(command);

            this.DeleteMedicines(er);
            int added = this.InsertMedicines(er, db);

            db.EndTransaction();

            if (added != er.Medicines.Count)
            {
                db.Rollback();
            }

            db.Close();
        }

        private void DeleteMedicines(Prescription p)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETEMEDICINES);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = p.Id;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void DeleteRequest(Request r)
        {
            Prescription er = (Prescription)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            this.DeleteMedicines(er);

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = er.Id;

            db.ExecuteNonQuery(command);

            db.EndTransaction();

            db.Close();
        }

        public void DeletePatientRequests(string p_id, IDatabase db = null)
        {
            IDatabase pdb = db;
            if (db == null)
            {
                db = new MSSqlDatabase();
                db.Connect();

                db.BeginTransaction();
            }

            DbCommand command = db.CreateCommand(sqlDELETEPATIENT1);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            db.ExecuteNonQuery(command);

            command = db.CreateCommand(sqlDELETEPATIENT2);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            db.ExecuteNonQuery(command);

            if (pdb == null)
            {
                db.EndTransaction();

                db.Close();
            }
        }

        public List<Request> SelectRequests(string p_id)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECT);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            DbDataReader reader = db.Select(command);

            List<Request> p = this.Read(reader, db);

            reader.Close();
            db.Close();

            return p;
        }

        private List<Request> Read(DbDataReader reader, IDatabase db)
        {
            List<Request> requests = new List<Request>();
            MedicineMapper medcat = MedicineMapper.GetInstance();

            while (reader.Read())
            {
                Prescription er = new Prescription();
                er.Id = reader.GetInt32(0);
                er.Created = reader.GetDateTime(1);

                DbCommand command = db.CreateCommand(sqlSELECTMEDICINE);
                command.Parameters.Add(db.CreateParameter("@id", "int"));
                command.Parameters["@id"].Value = er.Id;
                DbDataReader medicines = db.Select(command);
                er.Medicines = this.ReadMedicines(medicines, medcat);

                medicines.Close();

                requests.Add(er);
            }

            return requests;
        }

        private List<Medicine> ReadMedicines(DbDataReader reader, MedicineMapper catalogue)
        {
            List<Medicine> medicines = new List<Medicine>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                Medicine m = catalogue.Find(id);
                if (m == null)
                    throw new ApplicationException(ErrorMessages.Messages["REQ_P_unknownm"] + id.ToString());

                medicines.Add(m);
            }

            return medicines;
        }
    }
}
