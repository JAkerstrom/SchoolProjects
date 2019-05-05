using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.APIModels
{
    public class Auktion
    {
        public int AuktionID { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public string StartDatum { get; set; }
        public string SlutDatum { get; set; }
        public int Gruppkod { get; set; }
        public int Utropspris { get; set; }
        public string SkapadAv { get; set; }
    }
}
