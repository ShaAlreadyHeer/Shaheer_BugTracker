using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shaheer_BugTracker.Models;
using Shaheer_BugTracker.Helpers;
using static Shaheer_BugTracker.Helpers.HelperClass;
using Microsoft.AspNet.Identity;

namespace Shaheer_BugTracker.Controllers
{
    //[Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HelperClass helperRoles = new HelperClass();
        private UserRolesHelper userRoles = new UserRolesHelper();
        private UserProjectsHelper userProjectsHelper = new UserProjectsHelper();

        public ActionResult ManageUsers(int id)
        {
            ViewBag.ProjectId = id;

            #region PM Sections
            var pmId = userProjectsHelper.ListUsersOnProjectInRole(id, "Project_Manager").FirstOrDefault();
            ViewBag.ProjectManagerId = new SelectList(userRoles.UsersInRole("Project_Manager"), "Id", "Email", pmId);
            #endregion

            #region Dev Section
            ViewBag.Developers = new MultiSelectList(userRoles.UsersInRole("Developer"), "Id", "Email", 
                userProjectsHelper.ListUsersOnProjectInRole(id, "Developer"));
            #endregion

            #region Sub Section
            ViewBag.submitters = new MultiSelectList(userRoles.UsersInRole("Submitters"), "Id", "Email",
                userProjectsHelper.ListUsersOnProjectInRole(id, "Submitters"));
            #endregion

            return View();
        }

        // GET: Projects
        public ActionResult Index()
        {
            var dashboardView = new DashboardView();
            var user = db.Users.Find(User.Identity.GetUserId());
            dashboardView.myProjects = db.Projects.ToList();
            if (userRoles.ListUserRoles(user.Id).FirstOrDefault() == "Developer")
            {
                dashboardView.myProjects = userProjectsHelper.ListProjectsForUser(user.Id);
            }
            if (userRoles.ListUserRoles(user.Id).FirstOrDefault() == "Project_Manager")
            {
                dashboardView.myProjects = userProjectsHelper.ListProjectsForUser(user.Id);
            }
            if (userRoles.ListUserRoles(user.Id).FirstOrDefault() == "Submitter")
            {
                dashboardView.myProjects = userProjectsHelper.ListProjectsForUser(user.Id);
            }
            
            return View(dashboardView);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id, ProjectName, Description, CreatorId")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatorId = User.Identity.GetUserId();
                project.CreatedDate = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedDate,UpdatedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
