using Bug_Tracker.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class 
        UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string UserId { get; set; }

        public UserHelper()
        {
            UserId = HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetUserDisplayName()
        {
            return db.Users.Find(UserId).DisplayName;
        }

        public string GetUserAvatar()
        {
            return db.Users.Find(UserId).AvatarPath;
        }
    }
}
