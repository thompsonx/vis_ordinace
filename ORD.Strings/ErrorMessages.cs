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
        public const string REQ_P_unknownm = "Recept obsahuje neznámé ID léku! ID léku: ";

        public const string REQ_S_xml_id = "Čtení XML souboru s výsledky odběrů se nezdařilo! Neplatné ID: ";
        public const string REQ_S_xml_processed = "Čtení XML souboru s výsledky odběrů se nezdařilo! Neplatný formát data: ";
        public const string REQ_S_xml = "Čtení XML souboru s výsledky odběrů se nezdařilo! Chyba: ";

        public const string MED_S_xml_id = "Čtení XML souboru s katalogem léků se nezdařilo! Neplatné ID: ";
        public const string MED_S_xml_package = "Čtení XML souboru s katalogem léků se nezdařilo! Neplatná velikost balení: ";
        public const string MED_S_xml_price = "Čtení XML souboru s katalogem léků se nezdařilo! Neplatná cena: ";
        public const string MED_S_xml = "Čtení XML souboru s katalogem léků se nezdařilo! Chyba: ";

        public const string GUI_WF_RF_empty = "Nevybrali jste žádné léky! Chcete-li vytvořit recept, musíte přidat léky. Pokud chcete recept zrušit, zavřete okno.";
    }
}
