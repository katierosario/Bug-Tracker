using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class MessageHelper
    {
        public static List<TicketNotification> GetUnreadtNotifications()
        {

            var userId = HttpContext.Current.User.Identity.GetUserId();

            if (userId == null)
                return new List<TicketNotification>();

            var db = new ApplicationDbContext();
            return db.TicketNotifications.Where(t => t.ReceipientId == userId && !t.IsRead).ToList();

        }
    }
}