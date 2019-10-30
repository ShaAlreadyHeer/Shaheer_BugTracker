using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class TicketPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //Navigation
        public virtual ICollection<Ticket> Tickets { get; set;}

    }
}