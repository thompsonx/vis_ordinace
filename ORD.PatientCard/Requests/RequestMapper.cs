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

            }
            else if (r is ExaminationRequest)
            {

            }
            else if (r is SampleRequest)
            {

            }
            else
            {
                throw new ApplicationException(ErrorMessages.REQ_type);
            }
        }

        public void UpdateRequest(Request r)
        {
            if (r is Prescription)
            {

            }
            else if (r is ExaminationRequest)
            {

            }
            else if (r is SampleRequest)
            {

            }
            else
            {
                throw new ApplicationException(ErrorMessages.REQ_type);
            }
        }

        public void DeleteRequest(Request r)
        {
            if (r is Prescription)
            {

            }
            else if (r is ExaminationRequest)
            {

            }
            else if (r is SampleRequest)
            {

            }
            else
            {
                throw new ApplicationException(ErrorMessages.REQ_type);
            }
        }

        public List<Request> SelectRequests(string p_id, string type = null)
        {
            switch (type)
            {
                case null:
                    return null;
                case "prescription":
                    return null;
                case "samplerequest":
                    return null;
                case "examinationrequest":
                    return null;
                default:
                    throw new ApplicationException(ErrorMessages.REQ_type);
            }
        }
    }
}
