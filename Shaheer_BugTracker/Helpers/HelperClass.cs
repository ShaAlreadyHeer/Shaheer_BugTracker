﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shaheer_BugTracker.Helpers;

namespace Shaheer_BugTracker.Helpers
{
    public class HelperClass
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var projects = user.Projects.ToList(); return (projects);
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId); var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser); db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);
                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified;
            }
        }
        public ICollection<ApplicationUser> UsersOnProject(int projectId) 
        { 
            return db.Projects.Find(projectId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        { 
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList(); 
        }

        //Duplicate
        public class UserProjectsHelper
        {
            private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            private ApplicationDbContext db = new ApplicationDbContext();
            private UserRolesHelper roleHelper = new UserRolesHelper();
            public bool IsOnProject(string userId, int projectId)
            {
                if(db.Projects.Find(projectId).Users.Contains(db.Users.Find(userId)))
                {
                    return true;
                }
                return false;
            }
            
            public void AddUserToProject(string userId, int projectId)
            {
                if(!IsOnProject(userId,projectId))
                {
                    var project = db.Projects.Find(projectId);
                    project.Users.Add(db.Users.Find(userId));
                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            public void RemoveUserFromProject(string userId, int projectId)
            {
                if (IsOnProject(userId, projectId))
                {
                    var project = db.Projects.Find(projectId);
                    project.Users.Add(db.Users.Find(userId));
                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            public ICollection<ApplicationUser> ListUsersOnProject(int projectId)
            {
                return db.Projects.Find(projectId).Users;
            }

            public ICollection<Project> ListProjectsForUser(string userId)
            {
                return db.Users.Find(userId).Projects;
            }

            public ICollection<ApplicationUser> ListUsersNotOnProject(int projectId)
            {
                return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
            }

            public ICollection<ApplicationUser> ListUsersOnProjectInRole(int projectId, string roleName)
            {
                var resultList = new List<ApplicationUser>();

                foreach (var user in ListUsersOnProject(projectId))
                {
                    if (roleHelper.IsUserInRole(user.Id, roleName))
                    {
                        resultList.Add(user);
                    }
                }
    
                return resultList;
            }

        }

        public class AdminUserViewModel
        {
            public ApplicationUser User { get; set; }
            public MultiSelectList Roles { get; set; }
            public string[] SelectedRoles { get; set; }
        }

    }
}