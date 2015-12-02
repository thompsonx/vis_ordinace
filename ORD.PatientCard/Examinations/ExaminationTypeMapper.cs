using ORD.Database;
using ORD.Database.Mappers;
using ORD.HealthInsurances;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Examinations
{
    class ExaminationTypeMapper
    {
        private static string sqlINSERT = "INSERT INTO ExaminationTypes VALUES (@name, @description)";
        private static string sqlUPDATE = "UPDATE ExaminationTypes SET name = @name, description = @description " +
            " WHERE id = @id";
        private static string sqlDELETE = "DELETE FROM ExaminationTypes WHERE id = @id";
        private static string sqlSELECT = "SELECT * FROM ExaminationTypes ORDER BY name DESC";
        private static string sqlSELECTPRICES = "SELECT id, insurance, price FROM ExaminationPrices WHERE type_id = @id";
        private static string sqlFIND = "SELECT * FROM ExaminationTypes WHERE id = @id";
        private void PrepareCommand(IDatabase db, DbCommand cmd, ExaminationType er)
        {
            cmd.Parameters.Add(db.CreateParameter("@name", "varchar", er.Name.Length));
            cmd.Parameters["@name"].Value = er.Name;

            cmd.Parameters.Add(db.CreateParameter("@description", "varchar", er.Description.Length));
            cmd.Parameters["@description"].Value = er.Description;
        }
        public void Insert(string p_id, ExaminationType e)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, e);
            e.Id = db.ExecuteScalar(command);

            db.Close();
        }

        public void Update(ExaminationType e)
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

        public void Delete(ExaminationType e)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = e.Id;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public List<ExaminationType> SelectAll()
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECT);

            DbDataReader reader = db.Select(command);

            List<ExaminationType> e = this.Read(reader, db);

            reader.Close();
            db.Close();

            return e;
        }

        private List<ExaminationType> Read(DbDataReader reader, IDatabase db)
        {
            List<ExaminationType> types = new List<ExaminationType>();

            while (reader.Read())
            {
                ExaminationType e = new ExaminationType();
                e.Id = reader.GetInt32(0);
                e.Name = reader.GetString(1);
                e.Description = reader.GetString(2);
                e.Prices = this.SelectPrices(e.Id, db);


                types.Add(e);
            }

            return types;
        }

        public ExaminationType Find(int id)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlFIND);

            DbDataReader reader = db.Select(command);

            ExaminationType e = this.Read(reader, db)[0];

            reader.Close();
            db.Close();

            return e;
        }

        public List<ExaminationPrice> SelectPrices(int etype_id, IDatabase db = null)
        {
            IDatabase d = db;
            if (db == null)
            {
                db = new MSSqlDatabase();
                db.Connect();
            }

            DbCommand command = db.CreateCommand(sqlSELECTPRICES);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = etype_id;

            DbDataReader reader = db.Select(command);

            List<ExaminationPrice> e = this.ReadPrices(reader);

            reader.Close();

            if (d == null)
                db.Close();

            return e;
        }

        private List<ExaminationPrice> ReadPrices(DbDataReader reader)
        {
            List<ExaminationPrice> prices = new List<ExaminationPrice>();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                int insurance = reader.GetInt32(1);
                HealthInsuranceMapper him = HealthInsuranceMapper.GetInstance();
                HealthInsurance hi = him.Find(insurance);
                float price = reader.GetFloat(2);

                ExaminationPrice e = new ExaminationPrice(hi, price);
                e.Id = id;

                prices.Add(e);
            }

            return prices;
        }
    }
}
