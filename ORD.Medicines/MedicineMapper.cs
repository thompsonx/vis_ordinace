using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ORD.Medicines
{
    public class MedicineMapper
    {
        private static MedicineMapper mapper = new MedicineMapper();

        private List<Medicine> medicines;

        private MedicineMapper()
        {
            this.medicines = new List<Medicine>();
        }

        public void Add(Medicine m)
        {
            this.medicines.Add(m);
        }

        public List<Medicine> MedicinesList 
        {
            get
            {
                return this.medicines;
            }
            private set; 
        }

        public static void LoadMedicines()
        {
            if (mapper.MedicinesList.Count == 0)
                return;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Config.XML_medicines);
                XmlNodeList meds = xml.DocumentElement.SelectNodes("//medicine");

                foreach (XmlNode node in meds)
                {
                    int id;
                    if (!Int32.TryParse(node.ChildNodes[0].InnerText, out id))
                        throw new ApplicationException(ErrorMessages.MED_S_xml_id + node.ChildNodes[0].InnerText);
                    string name = node.ChildNodes[1].InnerText;
                    string description = node.ChildNodes[2].InnerText;
                    List<string> allergens = new List<string>();
                    foreach (XmlNode a in node.ChildNodes[3].ChildNodes)
                    {
                        allergens.Add(a.InnerText);
                    }
                    int package;
                    if (!Int32.TryParse(node.ChildNodes[4].InnerText, out package))
                        throw new ApplicationException(ErrorMessages.MED_S_xml_package + node.ChildNodes[4].InnerText);
                    float price;
                    if (!Single.TryParse(node.ChildNodes[5].InnerText, out price))
                        throw new ApplicationException(ErrorMessages.MED_S_xml_price + node.ChildNodes[5].InnerText);

                    Medicine m = new Medicine();
                    m.Id = id;
                    m.Name = name;
                    m.Description = description;
                    m.Allergens = allergens;
                    m.PackageSize = package;
                    m.Price = price;
                    mapper.Add(m);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(ErrorMessages.MED_S_xml + e.Message);
            }
        }

        public static List<Medicine> Medicines 
        {
            get
            {
                return mapper.MedicinesList;
            } 
            private set; 
        }
    }
}
