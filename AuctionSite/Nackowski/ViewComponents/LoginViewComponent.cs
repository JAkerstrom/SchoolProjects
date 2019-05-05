using Microsoft.AspNetCore.Mvc;
using Nackowski.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
    }
}
