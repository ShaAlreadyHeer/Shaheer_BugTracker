using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public int CreatedId { get; set; }
        //public ApplicationUser Creator { get; set; }



        //Navigation
        public virtual ICollection<Ticket> Tickets  { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<ApplicationUser>();
        }

    }
}