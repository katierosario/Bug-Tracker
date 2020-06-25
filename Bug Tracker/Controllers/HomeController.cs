using Bug_Tracker.Helpers;
using Bug_Tracker.Models;
using Bug_Tracker.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper roleHelper = new RolesHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }

        [Authorize(Roles = "Admin, Submitter, ProjectManager, Developer")]
        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var allTickets = db.Tickets.ToList();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var dashboardVM = new DashboardViewModel();
            switch (myRole)
            {
                case "Admin":
                    dashboardVM = new DashboardViewModel()
                    {
                        TicketCount = allTickets.Count,
                        HighPriorityTicketCount = allTickets.Where(t => t.TicketPriority.Name == "Immediate").Count(),
                        NewTicketCount = allTickets.Where(t => t.TicketStatus.Name == "New").Count(),
                        //TotalComments = db.TicketComments.Count(),
                        AllTickets = allTickets
                    };
                    break;
                case "ProjectManager":
                    dashboardVM = new DashboardViewModel()
                    {
                        TicketCount = allTickets.Where(t => t.Project.ProjectManagerId == userId).Count(),
                        HighPriorityTicketCount = allTickets.Where(t => t.TicketPriority.Name == "Immediate").Where(t => t.Project.ProjectManagerId == userId).Count(),
                        NewTicketCount = allTickets.Where(t => t.Project.ProjectManagerId == userId).Where(t => t.TicketStatus.Name == "New").Count(),
                        //TotalComments = db.TicketComments.Where(c => c.Ticket.Project.ProjectManagerId == userId).Count(),
                        AllTickets = allTickets.Where(t => t.Project.ProjectManagerId == userId).ToList(),
                    };

                    break;
                case "Developer":
                    dashboardVM = new DashboardViewModel()
                    {
                        TicketCount = allTickets.Where(t => t.DeveloperId == userId).Count(),
                        HighPriorityTicketCount = allTickets.Where(t => t.TicketPriority.Name == "Immediate").Where(t => t.DeveloperId == userId).Count(),
                        NewTicketCount = allTickets.Where(t => t.TicketStatus.Name == "New").Where(t => t.DeveloperId == userId).Count(),
                        //TotalComments = db.TicketComments.Where(t => t.DeveloperId == userId).Count(),
                        AllTickets = allTickets
                    };
                    break;
                default:
                    dashboardVM = new DashboardViewModel()
                    {
                        TicketCount = allTickets.Count,
                        HighPriorityTicketCount = allTickets.Where(t => t.TicketPriority.Name == "Immediate").Count(),
                        NewTicketCount = allTickets.Where(t => t.TicketStatus.Name == "New").Count(),
                        TotalComments = db.TicketComments.Count(),
                        AllTickets = allTickets
                    };

                    break;
            }

            dashboardVM.TicketStatusNew = db.Tickets.Where(t => t.TicketStatus.Name == "New").Count();
            dashboardVM.TicketStatusAssigned = db.Tickets.Where(t => t.TicketStatus.Name == "Assigned").Count();
            dashboardVM.TicketStatusCompleted = db.Tickets.Where(t => t.TicketStatus.Name == "Completed").Count();
            dashboardVM.TicketStatusUnAssigned = db.Tickets.Where(t => t.TicketStatus.Name == "UnAssigned").Count();

            dashboardVM.TicketPriorityImmediate = db.Tickets.Where(t => t.TicketStatus.Name == "Immediate").Count();
            dashboardVM.TicketPriorityHigh = db.Tickets.Where(t => t.TicketStatus.Name == "High").Count();
            dashboardVM.TicketPriorityMedium = db.Tickets.Where(t => t.TicketStatus.Name == "Medium").Count();
            dashboardVM.TicketPriorityLow = db.Tickets.Where(t => t.TicketStatus.Name == "Low").Count();

            dashboardVM.TicketCount = db.Tickets.Count();


            return View(dashboardVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactAsync(EmailModel model)
        {
            try
            {
                var emailAddress = WebConfigurationManager.AppSettings["Emailto"];
                var emailFrom = $"{model.From}<{emailAddress}";
                var FinalBody = $"{model.Name} has sent you the following message <br /> {model.Body} {WebConfigurationManager.AppSettings["LegalText"]}";


                var email = new MailMessage(emailFrom, emailAddress)
                {
                    Subject = model.Subject,
                    Body = FinalBody,
                    IsBodyHtml = true
                };

                var emailSvc = new EmailService();
                await emailSvc.SendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return View(new EmailModel());
        }

        public PartialViewResult _TopNav()
        {
            var model = db.Users.Find(User.Identity.GetUserId());
            return PartialView(model);
        }
    }
}