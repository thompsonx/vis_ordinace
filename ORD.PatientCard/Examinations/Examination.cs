using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.PatientCard.Examinations
{
    public class Examination
    {
        private int id;
        private DateTime mExamined;
        private string mDiagnosis;
        private ExaminationType mType;
        private char paid;

        public const char FEE_PAID = '1';
        public const char FEE_UNPAID = '0';
        public const char FEE_TO_BE_PAID = 'T';
        public const char FEE_ABSOLUTION = 'A';

        public const int LEN_DIAGNOSIS = 200;
        public const int LEN_FEE = 1;

        public Examination()
        {
            this.paid = FEE_ABSOLUTION;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public DateTime Examined
        {
            get { return this.mExamined; }
            set { this.mExamined = value; }
        }
        public string Diagnosis
        {
            get { return this.mDiagnosis; }
            set { this.mDiagnosis = value; }
        }
        public char Paid
        {
            get { return this.paid; }
            set { this.paid = value; }
        }

        public ExaminationType Type 
        {
            get { return this.mType; }
            set { this.mType = value; }
        }
    }
}
