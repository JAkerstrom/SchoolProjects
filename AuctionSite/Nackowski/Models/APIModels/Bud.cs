using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.APIModels
{
    public class Bud
    {
        public int BudID { get; set; }
        public int Summa { get; set; }
        public int AuktionID { get; set; }
        public string Budgivare { get; set; }
    }
}
