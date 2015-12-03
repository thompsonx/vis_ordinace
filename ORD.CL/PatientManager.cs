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

                }
                else if (cmd.Contains("P"))
                {

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
                    pcs.CreateBirthDateFromId(p),
                    p.Insurance.Code,
                    p.Town,
                    p.PhoneNumber,
                    i++);
            }
            Console.Write(Strings.PM_commands);
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

            Console.WriteLine("Shrnutí údajů");
            Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
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
