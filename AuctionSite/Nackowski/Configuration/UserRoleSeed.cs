using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Nackowski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.Configuration
{
    public class UserRoleSeed
    {
        private readonly IApplicationBuilder _app;

        public UserRoleSeed(IApplicationBuilder app)
        {
            _app = app;
        }

        public async void Seed()
        {
            using (var serviceScope = _app.ApplicationServices.CreateScope())
            {
                var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


                if (_roleManager.FindByNameAsync("Member").Result == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
                }

                if ((await _roleManager.FindByNameAsync("Admin")) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }


                var result = _userManager.FindByNameAsync("Arne@Bla.com").Result;

                if (result == null)
                {
                    ApplicationUser user = new ApplicationUser();

                    user.Email = "Arne@Bla.com";
                    user.UserName = "Arne@Bla.com";
                    user.EmailConfirmed = true;
                    user.PhoneNumber = "";

                    IdentityResult res = _userManager.CreateAsync(user, "Qwerty123!").Result;

                    if (res.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, "Member").Wait();
                    }

                }

                var result2 = _userManager.FindByNameAsync("Lisa@Bla.com").Result;

                if (result2 == null)
                {
                    ApplicationUser user2 = new ApplicationUser();

                    user2.Email = "Lisa@Bla.com";
                    user2.UserName = "Lisa@Bla.com";
                    user2.EmailConfirmed = true;
                    user2.PhoneNumber = "";

                    IdentityResult res2 = _userManager.CreateAsync(user2, "Qwerty1234!").Result;

                    if (res2.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user2, "Admin").Wait();
                    }

                }

            }
        }
    }
}
