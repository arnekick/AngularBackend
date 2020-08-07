using lijsten.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Lijsten.Models
{
    public class Item
    {
        public long ItemID { get; set; }
        public virtual Lijst Lijst { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public string Foto { get; set; }

    }
}
