using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Strings
{
    public static class ErrorMessages
    {
        public static Dictionary<string, string> Messages;

        static ErrorMessages()
        {
            Messages = new Dictionary<string, string>();
            try
            {
                using (StreamReader sr = new StreamReader("../../../errormessages.ini"))
                {
                    while (!sr.EndOfStream)
                    {
                        string msg = sr.ReadLine();
                        string[] parts = msg.Split('=');
                        Messages.Add(parts[0], parts[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Chyba v konfiguračním souboru errormessages.ini! " + ex.Message);
            }
        }
    }
}
