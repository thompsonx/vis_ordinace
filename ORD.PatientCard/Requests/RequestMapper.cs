using ORD.Database;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Requests
{
    class RequestMapper
    {
        public void InsertRequest(string p_id, Request r)
        {
            if (r is Prescription)
            {
                PrescriptionMapper pm = new PrescriptionMapper();
                pm.InsertRequest(p_id, r);
            }
            else if (r is ExaminationRequest)
            {
                ExaminationRequestMapper erm = new ExaminationRequestMapper();
                erm.InsertRequest(p_id, r);
            }
            else if (r is SampleRequest)
            {
                SampleRequestMapper srm = new SampleRequestMapper();
                srm.InsertRequest(p_id, r);
            }
            else
            {
                throw new ApplicationException(ErrorMessages.Messages["REQ_type"]);
            }
        }

        public void UpdateRequest(Request r)
        {
            if (r is Prescription)
            {
                PrescriptionMapper pm = new PrescriptionMapper();
                pm.UpdateRequest(r);
            }
            else if (r is ExaminationRequest)
            {
                ExaminationRequestMapper erm = new ExaminationRequestMapper();
                erm.UpdateRequest(r);
            }
            else if (r is SampleRequest)
            {
                SampleRequestMapper srm = new SampleRequestMapper();
                srm.UpdateRequest(r);
            }
            else
            {
                throw new ApplicationException(ErrorMessages.Messages["REQ_type"]);
            }
        }

        public void DeleteRequest(Request r)
        {
            if (r is Prescription)
            {
                PrescriptionMapper pm = new PrescriptionMapper();
                pm.DeleteRequest(r);
            }
            else if (r is ExaminationRequest)
            {
                ExaminationRequestMapper erm = new ExaminationRequestMapper();
                erm.DeleteRequest(r);
            }
            else if (r is SampleRequest)
            {
                SampleRequestMapper srm = new SampleRequestMapper();
                srm.DeleteRequest(r);
            }
            else
            {
                throw new ApplicationException(ErrorMessages.Messages["REQ_type"]);
            }
        }

        public void DeletePatientRequests(string p_id, IDatabase db = null)
        {
            new PrescriptionMapper().DeletePatientRequests(p_id, db);
            new ExaminationRequestMapper().DeletePatientRequests(p_id, db);
            new SampleRequestMapper().DeletePatientRequests(p_id, db);
        }

        public IList<Request> SelectRequests(string p_id, string type = null)
        {
            switch (type)
            {
                case null:
                    List<Request> pres = new PrescriptionMapper().SelectRequests(p_id);
                    List<Request> exams = new ExaminationRequestMapper().SelectRequests(p_id);
                    List<Request> samples = new SampleRequestMapper().SelectRequests(p_id);
                    List<Request> all = new List<Request>();
                    all.AddRange(pres);
                    all.AddRange(exams);
                    all.AddRange(samples);
                    return all;
                case "prescription":
                    return new PrescriptionMapper().SelectRequests(p_id);
                case "samplerequest":
                    return new SampleRequestMapper().SelectRequests(p_id);
                case "examinationrequest":
                    return new ExaminationRequestMapper().SelectRequests(p_id);
                default:
                    throw new ApplicationException(ErrorMessages.Messages["REQ_type"]);
            }
        }
    }
}
