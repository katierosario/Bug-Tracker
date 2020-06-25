using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }

        public List<ApplicationUser> ProjectManagers { get; set; }
    }
}