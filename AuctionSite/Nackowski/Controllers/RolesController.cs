using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nackowski.BusinessLogic;
using Nackowski.Models.Roles;
using System.Collections.Generic;

namespace Nackowski.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ManageUsers _manager;

        public RolesController(ManageUsers mu)
        {
            _manager = mu;
        }

        public IActionResult Index() 
        {
            return View(_manager.GetAllUsers());
        }

        [HttpPost]
        public IActionResult EditRoles(string userName)
        {
            UserVM user = _manager.GetUser(userName);
            EditRolesVM vm = new EditRolesVM();

            vm.CurrentUserRolesSelect = new SelectList(user.Roles);
            vm.AvailableRolesSelect = new SelectList(_manager.GetAvailableRoles());
            vm.userRoles = new List<string>();
            vm.user = user;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditRolesVM vm)
        {
            _manager.EditUser(vm);            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(string userName)
        {
            _manager.DeleteUser(userName);
            return RedirectToAction("Index");
        }
    }
}