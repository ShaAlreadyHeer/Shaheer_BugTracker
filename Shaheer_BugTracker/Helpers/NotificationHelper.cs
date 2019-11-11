using Microsoft.AspNet.Identity;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUser != null;
            var ticketHasBeenUnAssigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var ticketHasBeenReAssigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId != null && oldTicket.AssignedToUserId != newTicket.AssignedToUserId;

            if (ticketHasBeenAssigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
            } else if (ticketHasBeenUnAssigned){
                AddUnassignmentNotification(oldTicket, newTicket);
             } else if (ticketHasBeenReAssigned){
                AddAssignmentNotification(oldTicket, newTicket);
                AddUnassignmentNotification(oldTicket, newTicket);
            }
        }

        private void AddAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = newTicket.AssignedToUserId,
                NotificationBody = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private void AddUnassignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = newTicket.AssignedToUserId,
                NotificationBody = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Remove(notification);
            db.SaveChanges();
        }

        public static List<TicketNotification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Include("Sender").Where(t => t.RecipientId == currentUserId && !t.IsRead).ToList();
        }

    }
}