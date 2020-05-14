using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.Helpers;
using Bug_Tracker.ViewModel;

namespace Bug_Tracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper roleHelper = new RolesHelper();

        public ActionResult ManageRoles()
        {
            var viewData = new List<CustomUserData>();
            var users = db.Users.ToList();
            foreach(var user in users)
            {
                var newUserData = new CustomUserData();

                newUserData.FirstName = user.FirstName;
                newUserData.LastName = user.LastName;
                newUserData.Email = user.Email;
                newUserData.RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault() ?? "UnAssigned";

                viewData.Add(newUserData);
            }

            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");
            
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string roleName)
        {
            if(userIds != null)
            {
                foreach(var userId in userIds)
                {
                    var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

                    if (!string.IsNullOrEmpty(userRole))
                    {
                        roleHelper.RemoveUserFromRole(userId, userRole);
                    }

                    if (!string.IsNullOrEmpty(roleName))
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }        
                }
            }

            return RedirectToAction("ManageRoles");

        }

    }
}