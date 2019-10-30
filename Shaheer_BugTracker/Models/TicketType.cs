using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        

        //Navigation
        public ICollection<Ticket> Tickets { get; set; }

    }
}