using ORD.Database;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ORD.PatientCard.Requests
{
    class SampleRequestMapper
    {
        private static string sqlINSERT = "INSERT INTO SampleRequests(type, description, date) VALUES (@type, @description, @date)";
        private static string sqlUPDATE = "UPDATE SampleRequests SET type = @type, description = @description, " +
            "date = @date WHERE id = @id";
        private static string sqlADDRESULTS = "UPDATE SampleRequests SET results = @results, processed = @processed WHERE id = @id";
        private static string sqlDELETE = "DELETE FROM SampleRequests WHERE id = @id";
        private static string sqlDELETEPATIENT = "DELETE FROM SampleRequests WHERE person_id = @id";
        private static string sqlSELECT = "SELECT * FROM SampleRequests WHERE person_id = @id ORDER BY date DESC";
        private void PrepareCommand(IDatabase db, DbCommand cmd, SampleRequest er)
        {
            cmd.Parameters.Add(db.CreateParameter("@type", "varchar", er.Type.Length));
            cmd.Parameters["@type"].Value = er.Type;

            cmd.Parameters.Add(db.CreateParameter("@description", "varchar", er.Description.Length));
            cmd.Parameters["@description"].Value = er.Description;

            cmd.Parameters.Add(db.CreateParameter("@date", "datetime"));
            cmd.Parameters["@date"].Value = er.Created;

        }
        public void InsertRequest(string p_id, Request r)
        {
            SampleRequest er = (SampleRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlINSERT);
            PrepareCommand(db, command, er);
            er.Id = db.ExecuteScalar(command);

            db.Close();
        }

        public void UpdateRequest(Request r)
        {
            SampleRequest er = (SampleRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlUPDATE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = er.Id;
            PrepareCommand(db, command, er);
            db.ExecuteNonQuery(command);

            db.Close();
        }

        private void AddResults(int id, string results, DateTime processed, IDatabase db)
        {
            DbCommand command = db.CreateCommand(sqlADDRESULTS);
            
            command.Parameters.Add(db.CreateParameter("@results", "varchar", results.Length));
            command.Parameters["@results"].Value = results;

            command.Parameters.Add(db.CreateParameter("@processed", "datetime"));
            command.Parameters["@processed"].Value = processed;

            db.ExecuteNonQuery(command);

        }

        public void LoadAndAddResults()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Config.XML_request);
                IDatabase db = new MSSqlDatabase();
                db.Connect();

                db.BeginTransaction();

                XmlNodeList requests = xml.DocumentElement.SelectNodes("//samplerequest");

                foreach (XmlNode node in requests)
                {
                    int id;
                    if (!Int32.TryParse(node.ChildNodes[0].InnerText, out id))
                        throw new ApplicationException(ErrorMessages.REQ_S_xml_id + node.ChildNodes[0].InnerText);
                    string results = node.ChildNodes[4].InnerText;
                    DateTime processed;
                    if (!DateTime.TryParse(node.ChildNodes[5].InnerText, out processed))
                        throw new ApplicationException(ErrorMessages.REQ_S_xml_processed + node.ChildNodes[5].InnerText);
                    this.AddResults(id, results, processed, db);
                }
                db.EndTransaction();
                db.Close();
            }
            catch (Exception e)
            {
                throw new ApplicationException(ErrorMessages.REQ_S_xml + e.Message);
            }
        }

        public void DeleteRequest(Request r)
        {
            SampleRequest er = (SampleRequest)r;
            IDatabase db = new MSSqlDatabase();
            db.Connect();

            DbCommand command = db.CreateCommand(sqlDELETE);
            command.Parameters.Add(db.CreateParameter("@id", "int"));
            command.Parameters["@id"].Value = er.Id;

            db.ExecuteNonQuery(command);

            db.Close();
        }

        public void DeletePatientRequests(string p_id, IDatabase db = null)
        {
            IDatabase pdb = db;
            if (db == null)
            {
                db = new MSSqlDatabase();
                db.Connect();
            }

            DbCommand command = db.CreateCommand(sqlDELETEPATIENT);
            command.Parameters.Add(db.CreateParameter("@id", "char", p_id.Length));
            command.Parameters["@id"].Value = p_id;

            db.ExecuteNonQuery(command);

            if (pdb == null)
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
                SampleRequest er = new SampleRequest();
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
