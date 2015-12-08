using ORD.Database;
using ORD.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Medicines
{
    class SqlDbDataSource : IMedicineDataSource
    {
        private const string sqlSELECTALL = "SELECT * FROM Medicines ORDER BY id";
        private const string sqlSELECTALLERGENS = "SELECT a.name FROM Allergens a WHERE a.id IN (SELECT allergen_id FROM \"Contains\" WHERE medicine_id = @id)";
        private const string sqlSELECT = "SELECT * FROM Medicines m WHERE m.id = @id";
        public Dictionary<int, Medicine> LoadAsDict()
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            DbCommand command = db.CreateCommand(sqlSELECTALL);

            DbDataReader reader = db.Select(command);

            Dictionary<int, Medicine> medicines = this.Read(reader, db);

            reader.Close();

            db.EndTransaction();
            db.Close();

            return medicines;
        }

        public Medicine Find(int key)
        {
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            db.BeginTransaction();

            DbCommand command = db.CreateCommand(sqlSELECT);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = key;

            DbDataReader reader = db.Select(command);

            Dictionary<int, Medicine> medicines = this.Read(reader, db);

            reader.Close();

            db.EndTransaction();
            db.Close();

            if (medicines.Count == 0)
                return null;
            else
                return medicines.Values.ToList()[0];
        }

        private Dictionary<int, Medicine> Read(DbDataReader reader, IDatabase db)
        {
            Dictionary<int, Medicine> medicines = new Dictionary<int, Medicine>();

            while (reader.Read())
            {
                Medicine m = new Medicine();

                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                m.Description = reader.GetString(2);
                m.PackageSize = reader.GetInt32(3);
                m.Price = Decimal.ToSingle(reader.GetDecimal(4));

                m.Allergens = this.ReadAllergens(m, db);

                medicines.Add(m.Id, m);
            }

            return medicines;
        }

        private List<string> ReadAllergens(Medicine m, IDatabase db)
        {
            DbCommand command = db.CreateCommand(sqlSELECTALLERGENS);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = m.Id;

            DbDataReader areader = db.Select(command);

            List<string> allergens = new List<string>();
            while (areader.Read())
            {
                allergens.Add(areader.GetString(0));
            }

            areader.Close();

            return allergens;
        }
    }
}
