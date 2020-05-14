using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int TicketId { get; set; }

        public string SenderId { get; set; }

        public string ReceipientId { get; set; }

        public bool IsRead { get; set; }

        public string NotificationBody { get; set; }

        public DateTime Created { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Receipient { get; set; }
    }
}