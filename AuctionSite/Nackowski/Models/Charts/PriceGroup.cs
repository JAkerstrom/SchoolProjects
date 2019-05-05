using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.Charts
{
    public class PriceGroup
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public int SoldProducts { get; set; }
    }
}
