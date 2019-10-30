using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shaheer_BugTracker.Helpers
{
    public class ProjectsHelper
    {
        

        ////GET: EditUser
        //public ActionResult EditUser(string id)
        //{
        //    var user = db.Users.Find(id);
        //    AdminUserViewModel AdminModel = new AdminUserViewModel();
        //    UsersRolesHelper helper = new UsersRolesHelper();
        //    var selected = helper.ListUserRoles(id);
        //    AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
        //    AdminModel.Id = user.Id;
        //    AdminModel.Name = user.DisplayName;
        //    return view(AdminModel);
        //}

        ////POST: EditUser
        //public ActionResult EditUser(AdminUserViewModel model)
        //{
        //    var user = db.Users.Find(model.id);
        //    UsersRolesHelper helper = new UsersRolesHelper();
        //    foreach (var rolermv in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.RemoveUserFromRoles(user.Id, rolermv);
        //    }
        //    foreach (var roleadd in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.AddUserToRole(user.Id, roleadd);
        //    }
        //    return RedirectToAction("Index");
        //}


    }
}