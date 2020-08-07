using Lijsten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lijsten.Models
{
    public class Lijst
    {
        public long LijstID { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public Boolean Actief { get; set; }
        public virtual Gebruiker Eigenaar { get; set; }
    }
}
