using ORD.Database;
using ORD.Database.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.HealthInsurances
{
    public class HealthInsuranceMapper : IDbMapper<HealthInsurance>
    {
        private const string sqlINSERT = "INSERT INTO Health_insurance VALUES(@code, @name, @street, @town, @zip, @phone)";
        private const string sqlUPDATE = "UPDATE Health_insurance SET name = @name, street = @street, town = @town, " +
            "zip_code = @zip, phone_number = @phone WHERE code = @code";
        private const string sqlDELETE = "DELETE FROM Health_insurance WHERE code = @code";
        private const string sqlSELECT = "SELECT * FROM Health_insurance ORDER BY code";

        private static HealthInsuranceMapper insurance = null;

        public static HealthInsuranceMapper GetInstance()
        {
            if (insurance == null)
            {
                insurance = new HealthInsuranceMapper();
            }
            return insurance;
        }

        private Dictionary<int, HealthInsurance> insurances = null;

        private HealthInsuranceMapper() {

        }

        public void Insert(HealthInsurance subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, subject);

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void Update(HealthInsurance subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlUPDATE);
            PrepareCommand(db, command, subject);

            db.ExecuteNonQuery(command);

            db.Close();

            if (this.insurances.ContainsKey(subject.Code))
                this.insurances.Remove(subject.Code);
        }

        public void Delete(HealthInsurance subject)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@code", "int"));
            command.Parameters["@code"].Value = subject.Code;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        private void PrepareCommand(IDatabase db, DbCommand cmd, HealthInsurance hi)
        {
            cmd.Parameters.Add(db.CreateParameter("@code", "int"));
            cmd.Parameters["@code"].Value = hi.Code;

            cmd.Parameters.Add(db.CreateParameter("@name", "varchar", hi.Name.Length));
            cmd.Parameters["@name"].Value = hi.Name;

            cmd.Parameters.Add(db.CreateParameter("@street", "varchar", hi.Street.Length));
            cmd.Parameters["@street"].Value = hi.Street;

            cmd.Parameters.Add(db.CreateParameter("@town", "varchar", hi.Town.Length));
            cmd.Parameters["@town"].Value = hi.Town;

            cmd.Parameters.Add(db.CreateParameter("@zip", "int"));
            cmd.Parameters["@zip"].Value = hi.ZipCode;

            cmd.Parameters.Add(db.CreateParameter("@phone", "int"));
            cmd.Parameters["@phone"].Value = hi.PhoneNumber;
        }

        public IList<HealthInsurance> SelectAll()
        {
            if (this.insurances != null)
            {
                return this.insurances.Values.ToList();
            }

            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlSELECT);

            DbDataReader reader = db.Select(command);

            this.insurances = this.Read(reader);

            reader.Close();
            db.Close();

            return this.insurances.Values.ToList();
        }

        private Dictionary<int, HealthInsurance> Read(DbDataReader reader)
        {
            Dictionary<int, HealthInsurance> insurances = new Dictionary<int, HealthInsurance>();

            while (reader.Read())
            {
                HealthInsurance hi = new HealthInsurance();

                hi.Code = reader.GetInt32(0);
                hi.Name = reader.GetString(1);
                hi.Street = reader.GetString(2);
                hi.Town = reader.GetString(3);
                hi.ZipCode = reader.GetInt32(4);
                hi.PhoneNumber = reader.GetInt32(5);

                insurances.Add(hi.Code, hi);
            }

            return insurances;
        }

        public HealthInsurance Find(int id)
        {
            this.SelectAll();
            if (this.insurances.ContainsKey(id))
            {
                return this.insurances[id];
            }
            else
            {
                return null;
            }
        }
    }
}
