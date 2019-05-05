using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Models.Roles
{
    public class EditRolesVM
    {
        public UserVM user { get; set; }
        public List<string> userRoles { get; set; }

        public SelectList AvailableRolesSelect { get; set; }
        public SelectList CurrentUserRolesSelect { get; set; }
    }
}
