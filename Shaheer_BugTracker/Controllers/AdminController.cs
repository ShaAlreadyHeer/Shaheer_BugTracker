using Shaheer_BugTracker.Helpers;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shaheer_BugTracker.Controllers
{
   
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper userRoles = new UserRolesHelper();
        //GET: Admin
        public ActionResult ManageRoles()
        {
            var users1 = db.Users; 
            ViewBag.UserIds = new MultiSelectList(users1, "Id", "Email");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");
            var users = new List<ManageRoleViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageRoleViewModel
                {
                    UserName = $"{user.LastName},{user.FirstName}",
                    RoleName = userRoles.ListUserRoles(user.Id).FirstOrDefault()
                });
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string role)
        {
            foreach (var userId in userIds)
            {
                var userRole = userRoles.ListUserRoles(userId).FirstOrDefault();
                if (userRoles != null)
                {
                    userRoles.RemoveUserFromRole(userId, userRole);
                }
            }

            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    userRoles.AddUserToRole(userId, role);
                }
            }
            return RedirectToAction("Dashboard", "Home");
        }
    }
}