namespace Shaheer_BugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shaheer_BugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shaheer_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Shaheer_BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            #region Role Creation
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Project_Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project_Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }            
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region User Creation
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "shaheerahmed23@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "shaheerahmed23@gmail.com",
                    Email = "shaheerahmed23@gmail.com",
                    FirstName = "Shaheer",
                    LastName = "Ahmed",
                    DisplayName = "ShaAlreadyHeer",
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "demoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoAdmin@Mailinator.com",
                    Email = "demoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "DemoAdmin",
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "demoPM@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoPM@mailinator.com",
                    Email = "demoPM@mailinator.com",
                    FirstName = "Demo",
                    LastName = "ProjectManager",
                    DisplayName = "DemoPM",
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "demoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoDeveloper@mailinator.com",
                    Email = "demoDeveloper@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "DemoDeveloper",
                }, "Abc&123!");
            }
            if (!context.Users.Any(u => u.Email == "demoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "demoSubmitter@mailinator.com",
                    Email = "demoSubmitter@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "DemoSubmitter",
                }, "Abc&123!");
            }
            #endregion

            #region Assign Roles
            var userId = userManager.FindByEmail("shaheerahmed23@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("demoAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("demoPM@mailinator.com").Id;
            userManager.AddToRole(userId, "Project_Manager");

            userId = userManager.FindByEmail("demoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("demoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
            #endregion

            #region Tickets
            context.TicketStatuses.AddOrUpdate(
                u => u.StatusName,
                new TicketStatus { StatusName = "Open", Description = "A newly created or simply unassigned ticket" },
                new TicketStatus { StatusName = "Assigned", Description = "A Ticket that has been assigned but has yet to be worked on" },
                new TicketStatus { StatusName = "In Progress", Description = "A Ticket has been assigned and is currently being worked on" },
                new TicketStatus { StatusName = "Resolved", Description = "A Ticket has been completed" },
                new TicketStatus { StatusName = "Archived", Description = "A Ticket that has been archived" }
                );

            context.TicketPriorities.AddOrUpdate(
                u => u.PriorityName,
                new TicketPriority { PriorityName = "Immediate", Description = "This priority levels require completion within 2 days" },
                new TicketPriority { PriorityName = "Highest", Description = "This priority levels requires completion within 1 week" },
                new TicketPriority { PriorityName = "High", Description = "This priority levels requires completion within 1 week" },
                new TicketPriority { PriorityName = "Medium", Description = "This priority levels requires completion within 1 week" },
                new TicketPriority { PriorityName = "Low", Description = "This priority levels requires completion within 1 week" },
                new TicketPriority { PriorityName = "None", Description = "This priority levels does not require any attention" }
                );

            context.TicketTypes.AddOrUpdate(
                u => u.TypeName,
                new TicketType { TypeName = "Defect", Description = "A defect in the software that has been identified" },
                new TicketType { TypeName = "Feature Request", Description = "The client has called and requested a new feature to be added" },
                new TicketType { TypeName = "Documentation Request", Description = "The client has called requesting documentation for a specific problem" },
                new TicketType { TypeName = "Training Request", Description = "The client has called requesting training on the software" }
                );
            #endregion

        }
    }
}
