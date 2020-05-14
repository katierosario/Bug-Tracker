using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Bug_Tracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }

        public string TicketId { get; set; }

        public string UserId { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string DeveloperId { get; set; }

        public string FileUrl { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}