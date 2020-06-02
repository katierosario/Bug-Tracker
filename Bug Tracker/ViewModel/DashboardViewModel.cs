using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class DashboardViewModel
    {
        public int TicketCount { get; set; }
        public int HighPriorityTicketCount { get; set; }
        public int NewTicketCount { get; set; }
        public int TotalComments { get; set; }
        public int UnassignedTickets { get; set; }
        public int NumberofProjects { get; set; }
        public int RecentlyAssigned { get; set; }


        public int TicketStatusNew { get; set; }
        public int TicketStatusAssigned { get; set; }
        public int TicketStatusCompleted { get; set; }
        public int TicketStatusUnAssigned { get; set; }

        public int TicketPriorityImmediate { get; set; }
        public int TicketPriorityHigh { get; set; }
        public int TicketPriorityMedium { get; set; }
        public int TicketPriorityLow { get; set; }

        public List<Ticket> AllTickets { get; set; }

        public ProjectViewModel ProjectVM { get; set; }

        public DashboardViewModel()
        {
            AllTickets = new List<Ticket>();
        }
    }
}