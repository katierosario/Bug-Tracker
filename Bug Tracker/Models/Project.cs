using Microsoft.Owin.Security.DataHandler.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Project Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectManagerId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        //public bool IsArchived { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        //public virtual ApplicationUser ProjectManagerId { get; set; }

        //Constructor
        public Project()
        {
        Users = new HashSet<ApplicationUser>();
        Tickets = new HashSet<Ticket>();
        }
    }
}