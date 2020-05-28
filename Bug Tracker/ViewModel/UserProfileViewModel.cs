using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        [Display(Name = "Upload Profile Picture")]
        public HttpPostedFileBase Avatar { get; set; }

    }
}