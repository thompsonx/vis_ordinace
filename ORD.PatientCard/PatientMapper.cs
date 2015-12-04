using ORD.Database;
using ORD.Database.Mappers;
using ORD.HealthInsurances;
using ORD.PatientCard.Examinations;
using ORD.PatientCard.Requests;
using System;
using System.Collections;
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
        private const string sqlUPDATE = "UPDATE Patient SET surname = @surname, name = @name," +
            " insurance = @insurance, street = @street, town = @town, zip_code = @zip, phone_number = @phone WHERE person_id = @id";
        private const string sqlDELETE = "DELETE FROM \"Patient\" WHERE person_id = @id";
        private const string sqlSELECTALL = "SELECT * FROM Patient ORDER BY surname, name ASC";
        private const string sqlFIND = "SELECT * FROM Patient WHERE person_id = @id";

        private Dictionary<string, Patient> patientMap;

        public PatientMapper()
        {
            this.patientMap = new Dictionary<string, Patient>();
        }

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

            this.patientMap.Add(subject.ID, subject);
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

            db.BeginTransaction();

            new RequestMapper().DeletePatientRequests(subject.ID, db);
            new ExaminationMapper().DeletePatient(subject, db);

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "char", subject.ID.Length));
            command.Parameters["@id"].Value = subject.ID;
            db.ExecuteNonQuery(command);

            db.EndTransaction();

            db.Close();
            
            if (this.patientMap.ContainsKey(subject.ID))
                patientMap.Remove(subject.ID);
        }

        public Patient Find(string id)
        {
            if (this.patientMap.ContainsKey(id))
                return this.patientMap[id];

            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlFIND);

            DbDataReader reader = db.Select(command);

            Patient p = null;
            if (reader.Read())
            {
                p = new Patient();
                p.ID = reader.GetString(0);
                p.Surname = reader.GetString(1);
                p.Name = reader.GetString(2);
                p.Street = reader.GetString(4);
                p.Town = reader.GetString(5);
                p.ZipCode = reader.GetInt32(6);
                p.PhoneNumber = reader.GetInt32(7);

                HealthInsuranceMapper him = HealthInsuranceMapper.GetInstance();
                p.Insurance = him.Find(reader.GetInt32(3));

                p.Examinations = new VirtualList<Examination>(new ExaminationLoader(p.ID));
                p.Requests = new VirtualList<Request>(new RequestLoader(p.ID));
                this.patientMap.Add(p.ID, p);
            }

            reader.Close();
            db.Close();

            return p;
        }

        public IList<Patient> SelectAll()
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
            Dictionary<string, Patient> patients = new Dictionary<string, Patient>();

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

                HealthInsuranceMapper him = HealthInsuranceMapper.GetInstance();
                p.Insurance = him.Find(reader.GetInt32(3));

                p.Examinations = new VirtualList<Examination>(new ExaminationLoader(p.ID));
                p.Requests = new VirtualList<Request>(new RequestLoader(p.ID));

                patients.Add(p.ID, p);
            }

            this.patientMap = patients;
            return patients.Values.ToList();
        }

        /**
         * Requests     
        **/

        public void InsertRequest(Patient p, Request r)
        {
            RequestMapper mapper = new RequestMapper();
            mapper.InsertRequest(p.ID, r);
        }

        public void UpdateRequest(Request r)
        {
            RequestMapper mapper = new RequestMapper();
            mapper.UpdateRequest(r);
        }

        public void DeleteRequest(Request r)
        {
            RequestMapper mapper = new RequestMapper();
            mapper.DeleteRequest(r);
        }

        public List<Request> SelectRequests(Patient p, string type = null)
        {
            return new RequestMapper().SelectRequests(p.ID, type);
        }

        /**
         * Examinations    
        **/

        public void InsertExamination(Patient p, Examination e)
        {
            ExaminationMapper em = new ExaminationMapper();
            em.Insert(p.ID, e);
        }

        public void UpdateExamination(Examination e)
        {
            ExaminationMapper em = new ExaminationMapper();
            em.Update(e);
        }

        public void DeleteExamination(Examination e)
        {
            ExaminationMapper em = new ExaminationMapper();
            em.Delete(e);
        }

        public List<Examination> SelectExaminations(Patient p)
        {
            return new ExaminationMapper().Select(p.ID);
        }

        public class ExaminationLoader : VirtualListLoader<Examination>
        {
            private string patient;

            public ExaminationLoader(string p_id)
            {
                this.patient = p_id;
            }
            public IList<Examination> Load()
            {
                return new ExaminationMapper().Select(this.patient);
            }
        }

        public class RequestLoader : VirtualListLoader<Request>
        {
            private string patient;

            public RequestLoader(string p_id)
            {
                this.patient = p_id;
            }
            public IList<Request> Load()
            {
                return new RequestMapper().SelectRequests(patient);
            }
        }

    }
}
