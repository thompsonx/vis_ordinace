using ORD.Database;
using ORD.Database.Mappers;
using ORD.PatientCard.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard
{
    public class PatientMapper : IDbMapper<Patient>
    {
        private static string sqlINSERT = "INSERT INTO Patient VALUES (@id, @surname, @name, @insurance, @street, @town, @zip, @phone)";
        private const string sqlUPDATE = "UPDATE Patient SET person_id = @id, surname = @surname, name = @name," +
            " insurance = @insurance, street = @street, town = @town, zip_code = @zip, phone_number = @phone WHERE person_id = @id";
        private const string sqlDELETE = "DELETE FROM \"Patient\" WHERE person_id = @pID";
        private const string sqlSELECTALL = "SELECT * FROM Patient ORDER BY surname, name ASC";

        public const int LEN_ID = 10;
        public const int LEN_SURNAME = 30;
        public const int LEN_NAME = 30;
        public const int LEN_STREET = 50;
        public const int LEN_TOWN = 50;

        private void PrepareCommand(IDatabase db, DbCommand cmd, Patient p)
        {
            cmd.Parameters.Add(db.CreateParameter("@id", "char", p.ID.Length));
            cmd.Parameters["@id"].Value = p.ID;

            cmd.Parameters.Add(db.CreateParameter("@surname", "varchar", p.Surname.Length));
            cmd.Parameters["@surname"].Value = p.Surname;

            cmd.Parameters.Add(db.CreateParameter("@name", "varchar", p.Name.Length));
            cmd.Parameters["@name"].Value = p.Name;

            cmd.Parameters.Add(db.CreateParameter("@insurance", "int"));
            cmd.Parameters["@insurance"].Value = p.Insurance.Code;

            cmd.Parameters.Add(db.CreateParameter("@street", "varchar", p.Street.Length));
            cmd.Parameters["@street"].Value = p.Street;

            cmd.Parameters.Add(db.CreateParameter("@town", "varchar", p.Town.Length));
            cmd.Parameters["@town"].Value = p.Town;

            cmd.Parameters.Add(db.CreateParameter("@zip", "int"));
            cmd.Parameters["@zip"].Value = p.ZipCode;

            cmd.Parameters.Add(db.CreateParameter("@phone", "int"));
            cmd.Parameters["@phone"].Value = p.PhoneNumber;
        }
        
        public void Insert(Patient subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, subject);
            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void Update(Patient subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();
            DbCommand command = db.CreateCommand(sqlUPDATE);
            PrepareCommand(db, command, subject);
            db.ExecuteNonQuery(command);
            db.Close();
        }
        public void Delete(Patient subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();
            DbCommand command = db.CreateCommand(sqlUPDATE);
            command.Parameters.Add(db.CreateParameter("@id", "char", subject.ID.Length));
            command.Parameters["@id"].Value = subject.ID;
            db.ExecuteNonQuery(command);
            db.Close();
        }

        public List<Patient> SelectAll()
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECTALL);

            DbDataReader reader = db.Select(command);

            List<Patient> p = this.Read(reader);

            reader.Close();
            db.Close();

            return p;
        }

        private List<Patient> Read(DbDataReader reader)
        {
            List<Patient> patients = new List<Patient>();

            while (reader.Read())
            {
                Patient p = new Patient();
                p.ID = reader.GetString(0);
                p.Surname = reader.GetString(1);
                p.Name = reader.GetString(2);
                p.Street = reader.GetString(4);
                p.Town = reader.GetString(5);
                p.ZipCode = reader.GetInt32(6);
                p.PhoneNumber = reader.GetInt32(7);

                //HealthInsurance hi = new HealthInsurance();
                //hi.Code = reader.GetInt32(3);
                //p.Insurance = hi;

                patients.Add(p);
            }

            return patients;
        }

        /**
         * Requests     
        **/

        public void InsertRequest(Patient p, Request r)
        {

        }

        public void UpdateRequest(Request r)
        {

        }

        public void DeleteRequest(Request r)
        {

        }

        public List<Request> SelectRequests(Patient p, string type = null)
        {
            //LAZY LOADING
            return null;
        }

    }
}
