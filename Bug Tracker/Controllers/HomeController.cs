using Bug_Tracker.Models;
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

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your landing page.";

            return View();
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return View(new EmailModel());
        }
    }
}