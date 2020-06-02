using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class ProjectsViewModel
    {
        public List<Project> AllProjects { get; set; }

        public List<Project> MyProjects { get; set; }

        public virtual ApplicationUser ProjectManager { get; set; }

    }
}