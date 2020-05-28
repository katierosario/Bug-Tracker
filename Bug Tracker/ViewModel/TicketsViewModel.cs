using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class TicketsViewModel
    {
        public List<Ticket> AllTickets { get; set; }

        public List<Ticket> ProjectTickets { get; set; }

        public List<Ticket> SubTickets { get; set; }

        public List<Ticket> DevTickets { get; set; }

    }
}