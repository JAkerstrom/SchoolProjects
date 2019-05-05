using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.Roles
{
    public class UserVM
    {
        public List<string> Roles { get; set; }
        [DisplayName("Användarnamn")]
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string SecurityStamp { get; set; }
    }
}
