using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Email"), EmailAddress]
        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Name { get; set; }

        public string From { get; set; }

        public string email { get; set; }
    }
}