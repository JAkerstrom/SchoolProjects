using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nackowski.BusinessLogic;
using Nackowski.Models;
using Nackowski.Models.API_ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Controllers
{
    [Authorize(Roles = "Member, Admin")]
    public class HomeController : Controller
    {
        private ManageAuktion _auktioner;
        private ManageBud _bud;
        private Calculator _calculator;

        public HomeController(ManageAuktion ma, ManageBud mb, Calculator calc)
        {
            _auktioner = ma;
            _bud = mb;
            _calculator = calc;

        }

        public IActionResult Search(string searchString)
        {
            var res = _auktioner.Search(searchString);
            return PartialView("_AuktionsResultView", res); 
        }

        public IActionResult Auktions()
        {
            List<AuktionVM> auktioner = _auktioner.CacheGetSetOpenAuktions();
            return View(auktioner);
        }

        public IActionResult SortedAuktions(string method)
        {           
            List<AuktionVM> sorted = _auktioner.Sort(method);
            return PartialView("_AuktionsResultView", sorted);        
        }

        public IActionResult AuktionDetails(int Id)
        {
            AuktionVM vm = _auktioner.GetAuktion(Id.ToString());         
            return View(vm);
        }

        public IActionResult PostBud(int id)
        {
            BudVM vm = new BudVM();
            vm.AuktionID = id;
            vm.Budgivare = User.Identity.Name;
            if (!(_auktioner.GetAuktion(vm.AuktionID.ToString()).Bud.Count() == 0))
            {
                ViewBag.highestBid = (_auktioner.GetAuktion(vm.AuktionID.ToString())).Bud.Max(b => b.Summa) + "kr";
            }
            else
            {
                ViewBag.highestBid = "Inga bud";
            }
            
            return View("PostBud",vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(int auktionId, int summa)
        {
            var auktion = _auktioner.GetAuktion(auktionId.ToString());
            var vm = new BudVM();
            vm.Budgivare = User.Identity.Name;
            vm.Summa = summa;
            vm.AuktionID = auktionId;

            int highestBid = 0;
            
            bool hasBids = _calculator.HasBids(auktion.Bud.Count());

            if (hasBids)
            {
                List<int> sums = auktion.Bud.Select(b => b.Summa).ToList();
                highestBid = _calculator.HighestBid(sums);
            }

            if (vm.Summa > highestBid)
            {
                await _bud.PostBud(vm);
            }
            else
            {
                ViewBag.Message = "Budet var för lågt";
                return RedirectToAction("AuktionDetails", vm.AuktionID.ToString());
            }

            ViewBag.Message = "Budet är sparat";
            return RedirectToAction("AuktionDetails", vm.AuktionID.ToString());
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<AuktionVM> vm = _auktioner.CacheGetSetOpenAuktions();
            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }
   
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }       
    }
}
