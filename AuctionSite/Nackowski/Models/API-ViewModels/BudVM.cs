using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.API_ViewModels
{
    public class BudVM
    {
        public int BudID { get; set; }

        [Range(1, 10000000)]
        [Required(ErrorMessage = "Du måste ange en summa")]
        [DisplayName("Summa")]
        public int Summa { get; set; }

        public int AuktionID { get; set; }
        [DisplayName("Budgivare")]
        public string Budgivare { get; set; }
    }
}
