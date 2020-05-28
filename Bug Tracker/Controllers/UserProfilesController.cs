using Bug_Tracker.Helpers;
using Bug_Tracker.Models;
using Bug_Tracker.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult EditProfile()
        {
            var currentUserId = User.Identity.GetUserId();

            var currentUser = db.Users.Find(currentUserId);

            var userProfileViewModel = new UserProfileViewModel();

            userProfileViewModel.Id = currentUser.Id;
            userProfileViewModel.FirstName = currentUser.FirstName;
            userProfileViewModel.LastName = currentUser.LastName;
            userProfileViewModel.DisplayName = currentUser.DisplayName;
            userProfileViewModel.Email = currentUser.Email;
            //userProfileViewModel.Avatar = currentUser.AvatarPath;

            return View(userProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel model)
        {
            var currentUser = db.Users.Find(model.Id);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            currentUser.DisplayName = model.DisplayName;

            if (model.Avatar != null)
            {
                var justFileName = Path.GetFileNameWithoutExtension(model.Avatar.FileName);
                justFileName = StringUtilities.URLFriendly(justFileName);
                justFileName = $"{justFileName}-{DateTime.Now.Ticks}";
                justFileName = $"{justFileName}{Path.GetExtension(model.Avatar.FileName)}";

                currentUser.AvatarPath = $"/Images/Avatar/{justFileName}";
                model.Avatar.SaveAs(Path.Combine(Server.MapPath("~/Images/Avatar/"), justFileName));
            }

            db.SaveChanges();

            TempData["EditProfileMessage"] = $"Your profile has been updated successfully.";

            return RedirectToAction("EditProfile");
        }
    }
}