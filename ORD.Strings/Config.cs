using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.Strings
{
    public static class Config
    {
        public static Dictionary<string, string> Settings;

        static Config()
        {
            Settings = new Dictionary<string, string>();
            try
            {
                using (StreamReader sr = new StreamReader("../../../config.ini"))
                {
                    while (!sr.EndOfStream)
                    {
                        string msg = sr.ReadLine();
                        string[] parts = msg.Split('~');
                        Settings.Add(parts[0], parts[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Chyba v konfiguračním souboru config.ini! " + ex.Message);
            }
        }
    }
}
