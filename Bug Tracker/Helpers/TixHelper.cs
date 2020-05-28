using Bug_Tracker.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace Bug_Tracker.Helpers
{
    public class TixHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserOnTicket(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            var flag = ticket.Users.Any(u => u.Id == userId);
            return (flag);
        }

        public ICollection<Ticket> ListSubmitterTickets(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var tickets = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
            return (tickets);
        }

        public ICollection<Ticket> ListDeveloperTickets(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var tickets = db.Tickets.Where(t => t.DeveloperId == userId).ToList();
            return (tickets);
        }

        public void AddUserToTicket(string userId, int ticketId)
        {
            if (!IsUserOnTicket(userId, ticketId))
            {
                Ticket tix = db.Tickets.Find(ticketId);
                var newUser = db.Users.Find(userId);

                tix.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromTicket(string userId, int ticketId)
        {
            if (IsUserOnTicket(userId, ticketId))
            {
                Ticket tix = db.Tickets.Find(ticketId);
                var delUser = db.Users.Find(userId);

                tix.Users.Remove(delUser);
                db.SaveChanges();
                db.Entry(tix).State = EntityState.Modified;
            }
        }

        public ICollection<ApplicationUser> UsersOnTicket(int ticketId)
        {
            return db.Tickets.Find(ticketId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnTicket(int ticketId)
        {
            return db.Users.Where(u => u.Tickets.All(p => p.Id != ticketId)).ToList();
        }

        public ICollection<Ticket> TicketsOnUserProject(string userId)
        {
            //For each dev this gives a list of all the tickets on all any assigned projects 
            ApplicationUser user = db.Users.Find(userId);
            //Gets a list of all of their projects
            var projects = user.Projects.ToList();
            //Makes a list of tickets
            List<Ticket> projTix = new List<Ticket>();
            //loop over every project in our list
            foreach (var proj in projects)
            {
                //grab every ticket attached to those projects and add them to the list
                projTix.AddRange(proj.Tickets.ToList());
            }

            return(projTix);
        }
    }
}