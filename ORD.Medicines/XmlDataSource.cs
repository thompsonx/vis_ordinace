using ORD.Repository;
using ORD.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ORD.Medicines
{
    class XmlDataSource : IMedicineDataSource
    {

        public Medicine Find(int key)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Config.Settings["XML_medicines"]);
                XmlNodeList meds = xml.DocumentElement.SelectNodes(string.Format("//medicine[id[text()={0}]]", key.ToString()));

                if (meds.Count == 0)
                    return null;

                XmlNode node = meds[0];
                int id;
                if (!Int32.TryParse(node.ChildNodes[0].InnerText, out id))
                    throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_id"] + node.ChildNodes[0].InnerText);
                string name = node.ChildNodes[1].InnerText;
                string description = node.ChildNodes[2].InnerText;
                List<string> allergens = new List<string>();
                foreach (XmlNode a in node.ChildNodes[3].ChildNodes)
                {
                    allergens.Add(a.InnerText);
                }
                int package;
                if (!Int32.TryParse(node.ChildNodes[4].InnerText, out package))
                    throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_package"] + node.ChildNodes[4].InnerText);
                float price;
                if (!Single.TryParse(node.ChildNodes[5].InnerText, out price))
                    throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_price"] + node.ChildNodes[5].InnerText);

                Medicine m = new Medicine();
                m.Id = id;
                m.Name = name;
                m.Description = description;
                m.Allergens = allergens;
                m.PackageSize = package;
                m.Price = price;

                return m;
            }
            catch (Exception e)
            {
                throw new ApplicationException(ErrorMessages.Messages["MED_S_xml"] + e.Message);
            }
        }

        public Dictionary<int, Medicine> LoadAsDict()
        {
            try
            {
                Dictionary<int, Medicine> result = new Dictionary<int, Medicine>();
                XmlDocument xml = new XmlDocument();
                xml.Load(Config.Settings["XML_medicines"]);
                XmlNodeList meds = xml.DocumentElement.SelectNodes("//medicine");

                foreach (XmlNode node in meds)
                {
                    int id;
                    if (!Int32.TryParse(node.ChildNodes[0].InnerText, out id))
                        throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_id"] + node.ChildNodes[0].InnerText);
                    string name = node.ChildNodes[1].InnerText;
                    string description = node.ChildNodes[2].InnerText;
                    List<string> allergens = new List<string>();
                    foreach (XmlNode a in node.ChildNodes[3].ChildNodes)
                    {
                        allergens.Add(a.InnerText);
                    }
                    int package;
                    if (!Int32.TryParse(node.ChildNodes[4].InnerText, out package))
                        throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_package"] + node.ChildNodes[4].InnerText);
                    float price;
                    if (!Single.TryParse(node.ChildNodes[5].InnerText, out price))
                        throw new ApplicationException(ErrorMessages.Messages["MED_S_xml_price"] + node.ChildNodes[5].InnerText);

                    Medicine m = new Medicine();
                    m.Id = id;
                    m.Name = name;
                    m.Description = description;
                    m.Allergens = allergens;
                    m.PackageSize = package;
                    m.Price = price;
                    result.Add(m.Id, m);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new ApplicationException(ErrorMessages.Messages["MED_S_xml"] + e.Message);
            }
        }
    }
}
