﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Ticket
    {
        #region Ids
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Ticket Type")]
        public int TicketTypeId { get; set; }

        [Display(Name = "Ticket Status")]
        public int TicketStatusId { get; set; }

        [Display(Name = "Ticket Priority")]
        public int TicketPriorityId { get; set; }

        [Display(Name = "Submitter")]
        public string SubmitterId { get; set; }

        [Display(Name = "Developer")]
        public string DeveloperId { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual TicketPriority TicketPriority { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual TicketType TicketType { get; set; }


        #endregion

        #region Description

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsArchived { get; set; }

        #endregion

        #region Navigation

        public virtual Project Project { get; set; }

        public virtual ApplicationUser Submitter { get; set; }

        public virtual ApplicationUser Developer { get; set; }

        public virtual ICollection<TicketAttachment> Attachments { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; }

        public virtual ICollection<TicketHistory> Histories { get; set; }

        public virtual ICollection<TicketNotification> Notifications { get; set; }

        #endregion

        public Ticket()
        {
            Attachments = new HashSet<TicketAttachment>();
            Comments = new HashSet<TicketComment>();
            Histories = new HashSet<TicketHistory>();
            Notifications = new HashSet<TicketNotification>();
        }
    }

}