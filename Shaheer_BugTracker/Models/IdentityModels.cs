using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Shaheer_BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "FirstName")]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "First Name must contain 3 - 30 Characters")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last Name must contain 3 - 30 Characters")]
        public string LastName { get; set; }
        [Display(Name = "DisplayName")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must contain 3 - 30 Characters")]
        public string DisplayName { get; set; }
        
        public string AvatarPath { get; set; }

        [NotMapped]
        public string FullNameEmail
        {
            get
            {
                return $"{Email}, -{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }


        //Children
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public ApplicationUser()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
            Projects = new HashSet<Project>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.Ticket> Tickets { get; set; }


        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketPriority> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketStatus> TicketStatuses { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketAttachment> TicketAttachments { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketComment> TicketComments { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketHistory> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.TicketNotification> TicketNotifications { get; set; }

        //public System.Data.Entity.DbSet<Shaheer_BugTracker.Models.ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<ApplicationUser> User { get; set; }
    }
}