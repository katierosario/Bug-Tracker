using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.Helpers;
using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;

namespace Bug_Tracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Developer).Include(t => t.Project).Include(t => t.Submitter);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create(int? projectId)
        {
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName");

            var myUserId = User.Identity.GetUserId();
            var myProjects = projHelper.ListUserProjects(myUserId);
            var newTicket = new Ticket();

            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.ProjectId = new SelectList(myProjects, "Id", "Name");
            //ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,TicketTypeId,TicketPriorityId,Title,Description")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.SubmitterId = User.Identity.GetUserId();
                ticket.TicketStatusId = db.TicketStatus.FirstOrDefault(t => t.Name == "New").Id;
                ticket.Created = DateTime.Now;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.DeveloperId == new SelectList(db.Users, "Id", "FirstName");

            var myUserId = User.Identity.GetUserId();
            var myProjects = projHelper.ListUserProjects(myUserId);


            if (ticket.ProjectId == 0)
                ViewBag.ProjectId = new SelectList(myProjects, "Id", "Name");

            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatus = new SelectList(db.TicketStatus, "Id", "Name");

            return View(ticket);

        }

        // GET: Tickets/Edit/5
        [Authorize(Roles="Developer, Submitter, ProjectManager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);

            var currentUserId = User.Identity.GetUserId();

            if (User.IsInRole("Developer") && ticket.DeveloperId != currentUserId)
                TempData["UnAuthorizedTicketAccess"] = $"You are no authorized to edit this ticket {id}.";
                return RedirectToAction("Dashboard", "Home");

            if (User.IsInRole("Submitter") && ticket.DeveloperId == currentUserId)
            if (User.IsInRole("ProjectManager") && ticket.DeveloperId == currentUserId)
            {
                    var myTickets = db.Projects.Where(p => p.ProjectManagerId == currentUserId).SelectMany(p => p.Tickets).Select(t => t.Id);
            }

            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketType = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketPriority = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatus = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketStatus,TicketPriorityId,SubmitterId,DeveloperId,Title,Description,Created,Updated,IsArchived")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName", ticket.DeveloperId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmitterId = new SelectList(db.Users, "Id", "FirstName", ticket.SubmitterId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
