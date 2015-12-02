using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Medicines
{
    public class Medicine
    {

        private int mId;
        private string mName;
        private string mDescription;
        private List<string> mAllergens;
        private int mPackageSize;
        private float mPrice;

        public Medicine()
        {
            this.mAllergens = new List<string>();
        }


        public int Id
        { 
            get { return this.mId; }
            set { this.mId = value; }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                this.mName = value;
            }
        }

        public string Description 
        {
            get { return this.mDescription; }
            set { this.mDescription = value; }
        }

        public List<string> Allergens 
        {
            get { return this.mAllergens; }
            set { this.mAllergens = value; }
        }

        public int PackageSize
        {
            get { return this.mPackageSize; }
            set { this.mPackageSize = value; }
        }

        public float Price 
        {
            get { return this.mPrice; }
            set { this.mPrice = value; }
        }

        public override string ToString()
        {
            return this.mName;
        }
    }
}
