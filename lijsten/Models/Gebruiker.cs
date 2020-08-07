using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lijsten.Models
{
    public class Gebruiker
    {
        public long GebruikerID { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Gebruikersnaam { get; set; }

    }
}
