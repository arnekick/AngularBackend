using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lijsten.Models
{
    public class Stem
    {
        public long StemID { get; set; }
        public virtual Item Item { get; set; }
        public virtual Gebruiker Gebruiker { get; set; }

    }
}
