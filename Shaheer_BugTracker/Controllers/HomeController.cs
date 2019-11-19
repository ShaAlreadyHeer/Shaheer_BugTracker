using Microsoft.AspNet.Identity;
using Shaheer_BugTracker.Helpers;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shaheer_BugTracker.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var data = new DashboardView();
            data.myProjects = db.Projects.ToList();
            data.myTickets = db.Tickets.ToList();
            data.myUsers = db.Users.ToList();
            return View(data);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult DemoUser()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: 
        [AllowAnonymous]
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var profile = new UserProfileViewModel();
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.DisplayName = user.DisplayName;
            profile.Email = user.Email;
            profile.Avatar = user.AvatarPath;
            return View(profile);
        }


        // POST: 
        [HttpPost]
        public ActionResult EditProfile(UserProfileViewModel model, HttpPostedFileBase avatar)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            user.DisplayName = model.DisplayName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            if ( avatar != null)
            {
                if(AttachmentUploadValidator.IsWebFriendlyAttachment(avatar))
                {
                    var filename = Path.GetFileName(avatar.FileName);
                    avatar.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), filename));
                    user.AvatarPath = "/Avatars/" + filename;
                }
            }

            db.SaveChanges();
            return View();
        }
    }
}