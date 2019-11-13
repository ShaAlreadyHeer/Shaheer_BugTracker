using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class DashboardView
    {
        public virtual ICollection<Project> myProjects { get; set; }
        public virtual ICollection<Ticket> myTickets { get; set; }
        public virtual ICollection<ApplicationUser> myUsers { get; set; }

        public DashboardView()
        {
            myProjects = new HashSet<Project>();
            myTickets = new HashSet<Ticket>();
            myUsers = new HashSet<ApplicationUser>();
        }
    }
}