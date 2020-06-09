using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class HistoriesDisplayHelper
    {
        public static string DisplayData(string userId)
        {
            var db = new ApplicationDbContext();

            var data = "";

            data = db.Users.FirstOrDefault(u => u.Id == userId).FullName;

            return data;
            
        }
    }
}