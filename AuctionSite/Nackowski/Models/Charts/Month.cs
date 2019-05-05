using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.Charts
{
    public class Month
    {
        public string MonthName { get; set; }
        public List<PriceGroup> PriceGroups { get; set; }
    }
}
