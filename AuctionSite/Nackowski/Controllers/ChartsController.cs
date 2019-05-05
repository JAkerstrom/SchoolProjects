using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Nackowski.BusinessLogic;
using Nackowski.Models.Charts;
using Microsoft.AspNetCore.Authorization;

namespace Nackowski.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChartsController : Controller
    {
        private ManageAuktion _auktionManager;
        private ManageCharts _chartsManager;
        private ManageUsers _users;

        public ChartsController(ManageCharts chartsManager, ManageUsers users, ManageAuktion auktionManager)
        {
            _auktionManager = auktionManager;
            _chartsManager = chartsManager;
            _users = users;
        }

        public IActionResult Index()
        {
            var auktions = _auktionManager.GetClosedAuktions();
            List<Month> model = _chartsManager.StackedDiagram(auktions);
      
            return View(model);
        }

        public IActionResult GetMonths(int startMonth, int amount)
        {
            var auktions = _auktionManager.GetClosedAuktions();
            var res = _chartsManager.StackedDiagram(auktions, amount);
            return PartialView("_StackedPartial", res);

        }

        public IActionResult GetUserMonths(int startMonth, int amount) {

            var auktions = _auktionManager.GetUserClosedAuktions(User.Identity.Name);
            var res = _chartsManager.UserStackedDiagram(auktions, startMonth, amount);
            return PartialView("_StackedPartial", res);
        }

    }
}