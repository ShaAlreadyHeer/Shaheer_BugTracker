using Microsoft.AspNet.Identity;
using Shaheer_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shaheer_BugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {
            if(oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketStatusId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.TicketStatus.StatusName,
                    NewValue = newTicket.TicketStatus.StatusName,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };
                db.TicketHistories.Add(newHistoryRecord);
            }

            if(oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketPriorityId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.TicketPriority.PriorityName,
                    NewValue = newTicket.TicketPriority.PriorityName,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.OwnerUserId != newTicket.OwnerUserId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "DeveloperId",
                    TicketId = oldTicket.Id,
                    OldValue = oldTicket.OwnerUserId == null ? "UnAssigned" : oldTicket.OwnerUserId,
                    NewValue = newTicket.OwnerUserId == null ? "UnAssigned" : newTicket.OwnerUserId,
                    UpdateDate = (DateTime)newTicket.Updated,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            db.SaveChanges();
        }
    }
}