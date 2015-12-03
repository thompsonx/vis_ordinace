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
        public const string PM_commands = "Příkazy:\nRegistrace pacienta - REG\nUpravit pacienta - EDIT [id]\nSmazat pacienta: DEL [id]\nDetail pacienta: P [id]\nAkce: ";
        public const string PM_command = "Neplatný příkaz!";
        public const string PM_DEL_confirm = "Opravdu chcete vymazat pacienta (A/N): ";
        public const string PM_DEL_success = "Pacient byl úspěšné smazán!";
    }
}
