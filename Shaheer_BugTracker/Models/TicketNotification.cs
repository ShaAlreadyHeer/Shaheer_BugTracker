using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string NotificationBody { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RecipientId { get; set; }
        public bool IsRead { get; set; }

        public Ticket Ticket { get; set; }
        public ApplicationUser Recipient { get; set; }


    }
}