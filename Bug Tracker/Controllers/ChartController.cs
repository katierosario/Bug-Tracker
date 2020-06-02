using Bug_Tracker.Models;
using Bug_Tracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult GetTicketPriorityChartData()
        {
            var colorList = new List<string>();
            colorList.Add("#FFE971");
            colorList.Add("#D04F32");
            colorList.Add("#49BBD0");
            colorList.Add("#8BD3E3");
            colorList.Add("#FFFFBF");

            var rand = new Random();

            var morrisBarVM = new MorrisChartViewModel();
            var priorities = db.TicketPriorities.ToList();

            var dataKey = 0;

            morrisBarVM.Data.Insert(dataKey, "Tickets");

            foreach(var priority in priorities)
            {
                dataKey++;

                var count = db.Tickets.Where(t => t.TicketPriorityId == priority.Id).Count();
                morrisBarVM.Data.Insert(dataKey, count.ToString());
                morrisBarVM.YKeys.Add(dataKey.ToString());
                morrisBarVM.Labels.Add(priority.Name);
                morrisBarVM.BarColors.Add(colorList[rand.Next(0, colorList.Count)]);

            }

            return Json(morrisBarVM);
        }
    }
}