﻿using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            GenerateTicketAssignmentNotifications(oldTicket, newTicket);

            GenerateTicketChangeNotification(oldTicket, newTicket);
        }

        private void GenerateTicketAssignmentNotifications(Ticket oldTicket, Ticket newTicket)
        {
            bool assigned = oldTicket.DeveloperId == null && newTicket.DeveloperId != null;
            bool unassigned = oldTicket.DeveloperId != null && newTicket.DeveloperId == null;
            bool reassigned = !assigned && !unassigned && oldTicket.DeveloperId != newTicket.DeveloperId;

            if (assigned)
            {
                var created = DateTime.Now;
                db.TicketNotifications.Add(new TicketNotification
                {
                    Created = created,
                    TicketId = newTicket.Id,
                    SenderId = HttpContext.Current.User.Identity.GetUserId(),
                    ReceipientId = newTicket.DeveloperId,
                    Subject = "You have been assigned to a Ticket",
                    NotificationBody = $"You have been assigned to Ticket Id : {newTicket.Id} on {created.ToString("MMM dd, yyyy")}. This ticket is on the {newTicket.Project.Name}."
                });
            }

            db.SaveChanges();
        }

        private void FlipIsReadFlag (int notificationId)
        {
            TicketNotification notification = db.TicketNotifications.Find(notificationId);

            notification.IsRead = false;

            db.SaveChanges();

        }

        private void GenerateTicketChangeNotification(Ticket oldTicket, Ticket newTicket)
        {

        }

    }
}