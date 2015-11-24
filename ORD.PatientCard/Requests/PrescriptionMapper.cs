﻿using ORD.Database;
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
        private static string sqlINSERT = "INSERT INTO Prescriptions VALUES (@date)";
        private static string sqlINSERTMEDICINE = "INSERT INTO Prescribed VALUES (@id, @medicineid)";
        private static string sqlUPDATE = "UPDATE Prescriptions SET date = @date WHERE id = @id";
        private static string sqlDELETEMEDICINES = "DELETE FROM Prescribed WHERE id_p = @id";
        private static string sqlDELETE = "DELETE FROM Prescriptions WHERE id_p = @id";
        private static string sqlSELECT = "SELECT * FROM Prescriptions WHERE person_id = @id ORDER BY date DESC";
        private static string sqlSELECTMEDICINE = "SELECT id_medicine FROM Prescribed WHERE id_p = @id";
        private void PrepareCommand(IDatabase db, DbCommand cmd, Prescription er)
        {
            cmd.Parameters.Add(db.CreateParameter("@date", "datetime"));
            cmd.Parameters["@date"].Value = er.Created;

        }
        public void InsertRequest(string p_id, Request r)
        {
            Prescription er = (Prescription)r;
            if (er.Medicines.Count == 0)
            {
                throw new ApplicationException(ErrorMessages.REQ_P_medicines);
            }

            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, er);
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
            PrepareCommand(db, command, er);
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
            command.Parameters.Add(db.CreateParameter("@id_p", "int"));
            command.Parameters["@id_p"].Value = p.Id;

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

        public List<Request> SelectRequests(string p_id, string type = null)
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
                Prescription er = new Prescription();
                er.Id = reader.GetInt32(0);
                er.Created = reader.GetDateTime(1);

                requests.Add(er);
            }

            return requests;
        }
    }
}