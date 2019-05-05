using Microsoft.AspNetCore.Identity;
using Nackowski.Data;
using Nackowski.Models;
using Nackowski.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowski.BusinessLogic
{
    public class ManageUsers
    {
        private ApplicationDbContext _userContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _userContext = context;
        }

        private List<string> GetUserRoles(string userName)
        {
            ApplicationUser user = _userContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            List<string> userRoleIds = _userContext.UserRoles.Where(r => r.UserId == user.Id).Select(s => s.RoleId).ToList();
            List<string> userRoles = _userContext.Roles.Where(r => userRoleIds.Any(s => r.Id == s)).Select(s => s.Name).ToList();
            return userRoles;
        }

        public List<string> GetAvailableRoles()
        {
            return _userContext.Roles.Select(r => r.Name).ToList();
        }

        public List<UserVM> GetAllUsers()
        {
            List<ApplicationUser> appUsers = _userContext.Users.ToList();
            List<UserVM> userVMs = new List<UserVM>();
            foreach (var user in appUsers)
            {
                UserVM vm = new UserVM()
                {
                    UserName = user.UserName,
                    Roles = GetUserRoles(user.UserName),
                    UserID = user.Id,
                    SecurityStamp = user.SecurityStamp
                };

                userVMs.Add(vm);             
            }
            return userVMs;
        }

        public UserVM GetUser(string userName)
        {
            ApplicationUser user = _userContext.Users.FirstOrDefault(u => u.UserName == userName);

            UserVM vm = new UserVM()
            {
                UserName = user.UserName,
                Roles = GetUserRoles(user.UserName),
                UserID = user.Id,
                SecurityStamp = user.SecurityStamp
            };
            return vm;
        }

        public void EditUser(EditRolesVM vm)
        {
            ApplicationUser user = _userContext.Users.FirstOrDefault(u => u.UserName == vm.user.UserName);
            List<string> availableRoles = GetAvailableRoles();
            if (vm.userRoles != null)
            {
                foreach (var role in vm.userRoles)
                {
                    if (_userManager.IsInRoleAsync(user, role).Result == false)
                    {
                        _userManager.AddToRoleAsync(user, role).Wait();
                    }                    
                }
                foreach (var availableRole in availableRoles) {

                    var keepRole = vm.userRoles.Contains(availableRole);

                    if ((_userManager.IsInRoleAsync(user, availableRole).Result == true) && keepRole == false)
                    {
                        _userManager.RemoveFromRoleAsync(user, availableRole).Wait();
                    }
                }
            }
        }

        public void DeleteUser(string userName)
        {
            ApplicationUser user = _userContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }
    }
}
