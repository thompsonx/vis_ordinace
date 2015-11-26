using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Strings
{
    public class ErrorMessages
    {
        public const string DB_conn = "Nelze se připojit k databázi.";
        public const string DB_close = "Chyba ukončení spojení databáze.";
        public const string DB_param = "Neznámý DB datový typ.";

        public const string REQ_type = "Neznámý typ žádanky!";
        public const string REQ_P_medicines = "Nepřidali jste žádný lék! Nelze přidat prázdný recept!";

        public const string REQ_S_xml_id = "Reading of XML with requests' results failed! Invalid ID: ";
        public const string REQ_S_xml_processed = "Reading of XML with requests' results failed! Invalid date format: ";
        public const string REQ_S_xml = "Reading of XML with requests' results failed! Error: ";

        public const string MED_S_xml_id = "Reading of XML with medicines failed! Invalid ID: ";
        public const string MED_S_xml_package = "Reading of XML with medicines failed! Invalid package size: ";
        public const string MED_S_xml_price = "Reading of XML with medicines failed! Invalid cena: ";
        public const string MED_S_xml = "Reading of XML with medicines failed! Error: ";
    }
}
