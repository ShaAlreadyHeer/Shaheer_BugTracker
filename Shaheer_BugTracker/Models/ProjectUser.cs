using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class ProjectUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public ProjectUser()
        {
            AssignedProjects = new HashSet<Project>();
            AssignedTickets = new HashSet<Project>();
            Notifications = new HashSet<TicketNotification>();
        }

        public virtual ICollection<Project> AssignedProjects { get; set; }
        public virtual ICollection<Project> AssignedTickets { get; set; }
        public virtual ICollection<TicketNotification> Notifications { get; set; }


    }
}