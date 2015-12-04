using ORD.B.HealthInsuranceService;
using ORD.B.PatientServices;
using ORD.HealthInsurances;
using ORD.PatientCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.CL
{
    class PatientManager
    {
        private PatientCardService pcs;
        private List<Patient> patients;
        public PatientManager()
        {
            this.pcs = new PatientCardService();
            this.patients = pcs.GetPatients();

            this.PrintScreen();

            string cmd;

            while (!(cmd = Console.ReadLine()).Equals("EXIT"))
            {
                if (cmd.Contains("REG"))
                {
                    this.Register();
                }
                else if (cmd.Contains("DEL"))
                {
                    this.Delete(cmd);
                }
                else if (cmd.Contains("EDIT"))
                {
                    this.Edit(cmd);
                }
                else if (cmd.Contains("P"))
                {
                    this.Detail(cmd);
                    Console.Write("Akce: ");
                }
                else
                {
                    Console.WriteLine(Strings.PM_command);
                    Console.Write("Akce: ");
                }
            }

        }

        private void PrintScreen(string msg = null)
        {
            Console.Clear();
            if (msg != null)
                Console.WriteLine(msg);
            Console.WriteLine(Strings.PM_headline);
            int i = 1;
            foreach (Patient p in patients)
            {
                Console.WriteLine("{7} {0} {1} {2} {3} {4} {5} {6}",
                    p.ID,
                    p.Surname,
                    p.Name,
                    pcs.CreateBirthDateFromId(p).Date.ToString("d"),
                    p.Insurance.Code,
                    p.Town,
                    p.PhoneNumber,
                    i++);
            }
            Console.Write(Strings.PM_commands);
        }

        private void Detail(string cmd)
        {
            string[] parts = cmd.Split(' ');
            int pos;
            if (!Int32.TryParse(parts[1], out pos))
            {
                Console.WriteLine(Strings.PM_command);
                return;
            }

            Patient p;
            try
            {
                p = this.patients[pos - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine(Strings.PM_uknown_id);
                return;
            }

            Console.WriteLine(" RČ: {0}\n Příjmení: {1}\n Jméno: {2}\n Zdravotní pojišťovna: {3}\n Ulice: {4}\n Město: {5}\n PSČ: {6}\n Telefon: {7}",
                    p.ID,
                    p.Surname,
                    p.Name,
                    p.Insurance.Code,
                    p.Street,
                    p.Town,
                    p.ZipCode,
                    p.PhoneNumber);
            Console.Write("B - Zpět, E - Upravit údaje. Akce: ");
            string decision = Console.ReadLine();
            while (!decision.Equals("B"))
            {
                if (decision.Equals("E"))
                {
                    this.Edit("EDIT " + pos);
                    return;
                }
                Console.WriteLine(Strings.PM_command);
                Console.Write("B - Zpět, E - Upravit údaje. Akce: ");
                decision = Console.ReadLine();
            }
        }

        private void Edit(string cmd)
        {
            string[] parts = cmd.Split(' ');
            int pos;
            if (!Int32.TryParse(parts[1], out pos))
            {
                Console.WriteLine(Strings.PM_command);
                return;
            }

            Patient p;
            try
            {
                p = this.patients[pos - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine(Strings.PM_uknown_id);
                return;
            }

            Console.WriteLine("POKUD CHCETE ZACHOVAT PŮVODNÍ HODNOTU, NECHTE POLE PRÁZDNÉ A STIKNĚTE ENTER.");
            Console.WriteLine("Rodné číslo: {0}", p.ID);
            Console.Write("Příjmení ({0}): ", p.Surname);
            string tmp;
            p.Surname = (tmp = Console.ReadLine()).Equals("") ? p.Surname : tmp;
            Console.Write("Jméno ({0}): ", p.Name);
            p.Name = (tmp = Console.ReadLine()).Equals("") ? p.Name : tmp;

            Console.WriteLine("Zdravotní pojišťovny:");
            HealthInsuranceService his = new HealthInsuranceService();
            foreach (HealthInsurance hi in his.GetInsurances())
            {
                Console.WriteLine("{0} - {1}", hi.Code, hi.Name);
            }
            Console.Write("Zdravotní pojišťovna ({0}): ", p.Insurance.Code);
            string code = Console.ReadLine();
            if (!code.Equals(""))
            {
                int code_int;
                bool test = true;
                while (test)
                {
                    while (!Int32.TryParse(code, out code_int))
                    {
                        Console.Write("Neplatný kód! Zadejte správný: ");
                        code = Console.ReadLine();
                    }

                    HealthInsurance insurance = his.Find(code_int);
                    if (insurance == null)
                    {
                        Console.Write("Neexistující pojišťovna! Zadejte jiný kód: ");
                        code = Console.ReadLine();
                    }
                    else
                    {
                        test = false;
                        p.Insurance = insurance;
                    }
                }
            }

            Console.Write("Ulice ({0}): ", p.Street);
            p.Street = (tmp = Console.ReadLine()).Equals("") ? p.Street : tmp;
            Console.Write("Město ({0}): ", p.Town);
            p.Town = (tmp = Console.ReadLine()).Equals("") ? p.Town : tmp;
            Console.Write("PSČ ({0}): ", p.ZipCode);
            string zip = Console.ReadLine();
            if (!zip.Equals(""))
            {
                int zipCode;
                while (!Int32.TryParse(zip, out zipCode))
                {
                    Console.Write("Neplatné PSČ! Zadejte znovu: ");
                    zip = Console.ReadLine();
                }
                p.ZipCode = zipCode;
            }

            Console.Write("Telefon ({0}): ", p.PhoneNumber);
            string phonenumber = Console.ReadLine();
            if (!phonenumber.Equals(""))
            {
                int phone;
                while (!Int32.TryParse(phonenumber, out phone))
                {
                    Console.Write("Neplatné telefonní číslo! Zadejte znovu: ");
                    phonenumber = Console.ReadLine();
                }
                p.PhoneNumber = phone;
            }

            Console.WriteLine("\nSHRNUTÍ ÚDAJŮ");
            Console.WriteLine(" RČ: {0}\n Příjmení: {1}\n Jméno: {2}\n Zdravotní pojišťovna: {3}\n Ulice: {4}\n Město: {5}\n PSČ: {6}\n Telefon: {7}",
                    p.ID,
                    p.Surname,
                    p.Name,
                    p.Insurance.Code,
                    p.Street,
                    p.Town,
                    p.ZipCode,
                    p.PhoneNumber);
            Console.Write("A - Potvrdit, N - Zadat vše znovu, E - Zrušit změny. Akce: ");
            string decision = Console.ReadLine();
            while (!decision.Equals("A"))
            {
                if (decision.Equals("N"))
                {
                    this.Edit(cmd);
                    return;
                }
                if (decision.Equals("E"))
                {
                    this.PrintScreen();
                    return;
                }
                Console.WriteLine(Strings.PM_command);
                Console.Write("A - Potvrdit, N - Zadat vše znovu, E - Zrušit změny. Akce: ");
                decision = Console.ReadLine();
            }
            try
            {
                this.pcs.EditPatient(p);
                this.PrintScreen("Změny byly provedeny!");
            }
            catch (Exception ex)
            {
                this.PrintScreen(ex.Message);
            }
        }

        private void Delete(string cmd)
        {
            string[] parts = cmd.Split(' ');
            int pos;
            if (!Int32.TryParse(parts[1], out pos))
            {
                Console.WriteLine(Strings.PM_command);
                return;
            }
            try
            {
                Patient p = this.patients[pos - 1];
                Console.WriteLine("{0} {1} {2} {3}", Strings.PM_DEL_confirm, p.ID, p.Surname, p.Name);
                string confirm = Console.ReadLine();
                if (confirm.Equals("N") || !confirm.Equals("A"))
                    return;
                pcs.DeletePatient(this.patients[pos - 1]);
                this.patients.RemoveAt(pos - 1);
                this.PrintScreen(Strings.PM_DEL_success);
                Console.Write("Akce: ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Register()
        {
            Patient p = new Patient();
            Console.Write("Rodné číslo: ");
            string id = Console.ReadLine();
            while (!this.pcs.isId(id))
            {
                Console.Write("Neplatné rodné číslo! Zadejte znovu: ");
                id = Console.ReadLine();
            }
            p.ID = id;
            Console.Write("Příjmení: ");
            p.Surname = Console.ReadLine();
            Console.Write("Jméno: ");
            p.Name = Console.ReadLine();

            Console.WriteLine("Zdravotní pojišťovny:");
            HealthInsuranceService his = new HealthInsuranceService();
            foreach (HealthInsurance hi in his.GetInsurances())
            {
                Console.WriteLine("{0} - {1}", hi.Code, hi.Name);
            }
            Console.Write("Zdravotní pojišťovna: ");
            string code = Console.ReadLine();
            int code_int;
            bool test = true;
            while (test)
            {
                while (!Int32.TryParse(code, out code_int))
                {
                    Console.Write("Neplatný kód! Zadejte správný: ");
                    code = Console.ReadLine();
                }

                HealthInsurance insurance = his.Find(code_int);
                if (insurance == null)
                {
                    Console.Write("Neexistující pojišťovna! Zadejte jiný kód: ");
                    code = Console.ReadLine();
                }
                else
                {
                    test = false;
                    p.Insurance = insurance;
                }
            }

            Console.Write("Ulice: ");
            p.Street = Console.ReadLine();
            Console.Write("Město: ");
            p.Town = Console.ReadLine();
            Console.Write("PSČ: ");
            int zipCode;
            while (!Int32.TryParse(Console.ReadLine(), out zipCode))
            {
                Console.Write("Neplatné PSČ! Zadejte znovu: ");
            }
            p.ZipCode = zipCode;
            Console.Write("Telefon: ");
            int phone;
            while (!Int32.TryParse(Console.ReadLine(), out phone))
            {
                Console.Write("Neplatné telefonní číslo! Zadejte znovu: ");
            }
            p.PhoneNumber = phone;

            Console.WriteLine("\nSHRNUTÍ ÚDAJŮ");
            Console.WriteLine(" RČ: {0}\n Příjmení: {1}\n Jméno: {2}\n Zdravotní pojišťovna: {3}\n Ulice: {4}\n Město: {5}\n PSČ: {6}\n Telefon: {7}",
                    p.ID,
                    p.Surname,
                    p.Name,
                    p.Insurance.Code,
                    p.Street,
                    p.Town,
                    p.ZipCode,
                    p.PhoneNumber);
            Console.Write("A - Potvrdit, N - Zadat vše znovu. Akce: ");
            string decision = Console.ReadLine();
            while (!decision.Equals("A"))
            {
                if (decision.Equals("N"))
                {
                    this.Register();
                    return;
                }
                Console.WriteLine(Strings.PM_command);
                Console.Write("A - Potvrdit, N - Zadat vše znovu. Akce: ");
                decision = Console.ReadLine();
            }
            try
            {
                this.pcs.RegisterPatient(p);
                this.patients.Add(p);
                this.PrintScreen("Pacient byl registrován!");
            }
            catch (Exception ex)
            {
                this.PrintScreen(ex.Message);
            }
        }
    }
}
