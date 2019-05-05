using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowski.BusinessLogic;
using Nackowski.Models.API_ViewModels;
using System.Threading.Tasks;

namespace Nackowski.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ManageAuktion _auktioner;

        public AdminController(ManageAuktion ma)
        {
            _auktioner = ma;
        }

        public IActionResult PostAuktion()
        {          
            return View(_auktioner.EmptyVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAuktion(AuktionVM vm)
        {

            vm.SkapadAv = User.Identity.Name;
            if (ModelState.IsValid)
            {
                await _auktioner.PostAuktion(vm);
            }
            
            return RedirectToAction("Auktions", "Home");
        }

        public IActionResult UpdateAuktion(int id)
        {
            AuktionVM vm = _auktioner.GetAuktion(id.ToString());
            return View("UpdateAuktion", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AuktionVM vm)
        {
            //int Id = vm.AuktionID;

            if (ModelState.IsValid)
            {
                await _auktioner.PutAuktion(vm);
            }
            return RedirectToAction("Auktions", "Home");
        }

        public async Task<IActionResult> Delete(string auktionId) {

            string response = await _auktioner.DeleteAuktion(auktionId);
            return RedirectToAction("Auktions", "Home");
        }
    }
}