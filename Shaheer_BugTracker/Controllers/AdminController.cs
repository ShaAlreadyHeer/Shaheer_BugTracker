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
        private HelperClass helperRoles = new HelperClass();
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
                if (userRole != null)
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
            return RedirectToAction("manageroles", "admin");
        }

        [Authorize(Roles = "Admin, Project_Manager")]
        public ActionResult ManageProjectUsers()
        {
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(userRoles.UsersInRole("Developer"), "Id", "Email").ToList();
            ViewBag.Submitters = new MultiSelectList(userRoles.UsersInRole("Submitter"), "Id", "Email").ToList();
            ViewBag.VBUsers = new SelectList(db.Users, "Id", "Email");

            if(User.IsInRole("Admin"))
            {
                ViewBag.ProjectManagerId = new SelectList(userRoles.UsersInRole("Project_Manager"), "Id", "Email");
            }

            //Create View Model for User's Projects
            var myData = new List<UserProjectListViewModel>();
            UserProjectListViewModel userVm = null;
            foreach (var user in db.Users.ToList())
            {
                userVm = new UserProjectListViewModel
                {
                    Name = $"{user.LastName},{user.FirstName}",
                    ProjectNames = helperRoles.ListUserProjects(user.Id).Select(p => p.ProjectName).ToList()
                };

                if (userVm.ProjectNames.Count() == 0)
                    userVm.ProjectNames.Add("N/A");

                myData.Add(userVm);
            }
            return View(myData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(List<int> projects, string ProjectManagerId, List<string> developers, List<string> submitters)
    {
        //Remove users from selected projects
        if(projects != null)
        {
            foreach (var projectId in projects)
            {
                foreach (var user in helperRoles.UsersOnProject(projectId).ToList())
                {
                    helperRoles.RemoveUserFromProject(user.Id, projectId);
                }

                if(!string.IsNullOrEmpty(ProjectManagerId))
                {
                    helperRoles.AddUserToProject(ProjectManagerId, projectId);
                }
                
                if(developers != null)
                {
                    foreach(var developerId in developers)
                    {
                        helperRoles.AddUserToProject(developerId, projectId);
                    }
                }
                
                if(submitters != null)
                {
                    foreach(var submittedId in submitters)
                    {
                        helperRoles.AddUserToProject(submittedId, projectId);
                    }
                }
            }
        }
        return RedirectToAction("ManageProjectUsers");
    }
    }
}