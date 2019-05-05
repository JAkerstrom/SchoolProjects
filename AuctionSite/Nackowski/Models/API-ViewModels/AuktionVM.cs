using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.API_ViewModels
{
    public class AuktionVM
    {
        public int AuktionID { get; set; }

        [Required(ErrorMessage = "Det måste finnas en titel")]
        [StringLength(40, ErrorMessage = "Titeln får vara max 40 tecken")]
        [DisplayName("Titel")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Det måste finnas en Beskrivning")]
        [StringLength(300, ErrorMessage = "Beskrivning får vara max 300 tecken")]
        [DisplayName("Beskrivning")]
        public string Beskrivning { get; set; }

        [Required(ErrorMessage = "Det måste finnas ett startdatum")]
        [DisplayName("Startdatum")]
        public DateTime StartDatum { get; set; }

        [Required(ErrorMessage = "Det måste finnas ett slutdatum")]
        [DisplayName("Slutdatum")]
        public DateTime SlutDatum { get; set; }

        [Range(1, 10000000)]
        [Required(ErrorMessage = "Det måste finnas ett utropspris")]
        [DisplayName("Utropspris")]
        public int Utropspris { get; set; }

        public string SkapadAv { get; set; }
        public List<BudVM> Bud { get; set; }
    }
}
