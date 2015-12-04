using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORD.CL
{
    static class Strings
    {
        public const string PM_headline = "Správa pacientů - Seznam pacientů\nID RČ\tPříjmení\tJméno\tNarození\tPojišťovna\tMěsto\tTelefon";
        public const string PM_commands = "Příkazy:\n\tRegistrace pacienta - REG\n\tUpravit pacienta - EDIT [id]\n\tSmazat pacienta: DEL [id]\n\tDetail pacienta: P [id]\n\tKonec: EXIT\nAkce: ";
        public const string PM_command = "Neplatný příkaz!";
        public const string PM_uknown_id = "Neplatné id!";
        public const string PM_DEL_confirm = "Opravdu chcete vymazat pacienta (A/N): ";
        public const string PM_DEL_success = "Pacient byl úspěšné smazán!";
    }
}
