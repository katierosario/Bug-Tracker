using Bug_Tracker.Helpers;
using Bug_Tracker.Models;
using Bug_Tracker.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private RolesHelper rolesHelper = new RolesHelper();

        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult ManageProjectAssignments()
        {
            var emptyCustomUserDataList = new List<CustomUserData>();

            var users = db.Users.ToList();

            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "FullName");

            ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name");

            foreach (var user in users)
            {
                emptyCustomUserDataList.Add(new CustomUserData
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProjectNames = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                });
            }

            return View(emptyCustomUserDataList);
        }

        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult ManageProjectLevelUsers(int id)
        {
            var userIds = projHelper.UsersOnProject(id).Select(u => u.Id).ToList();
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email", userIds);
            return View(db.Projects.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectLevelUsers(List<string> userIds, int projectId)
        {
            if (userIds != null)
            {

                //var projMembersIds = projHelper.UsersOnProject(projectId).Select(u => u.Id).ToList();

                //foreach(var memberId in projMembersIds)
                //{
                //    projHelper.RemoveUserFromProject(memberId, projectId);
                //}

                foreach (var userId in userIds)
                {
                    if (!projHelper.IsUserOnProject(userId, projectId))
                    {
                        projHelper.AddUserToProject(userId, projectId);
                        if (rolesHelper.IsUserInRole(userId, "ProjectManager"))
                        {
                            var proj = db.Projects.Find(projectId);
                            proj.ProjectManagerId = userId;
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        projHelper.RemoveUserFromProject(userId, projectId);
                        if (rolesHelper.IsUserInRole(userId, "ProjectManager"))
                        {
                            var proj = db.Projects.Find(projectId);
                            proj.ProjectManagerId = null;
                            db.SaveChanges();
                        }
                    }

                }
            }

            return RedirectToAction("ManageProjectLevelUsers", new { id = projectId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectAssignments(List<string> userIds, List<int> projectIds)
        {
            if (userIds == null)
                return RedirectToAction("ManageProjectAssignments");

            foreach (var userId in userIds)
            {
                foreach (var projectId in projectIds)
                {
                    if (rolesHelper.IsUserInRole(userId, "ProjectManager"))
                    {
                        var proj = db.Projects.Find(projectId);
                        proj.ProjectManagerId = userId;
                        db.SaveChanges();
                    }
                    projHelper.AddUserToProject(userId, projectId);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("ManageProjectAssignments");
        }

        // GET: Projects
        [Authorize(Roles = "Admin, Submitter, ProjectManager, Developer")]
        public ActionResult Index()
        {
            var projects = new ProjectsViewModel();

            projects.AllProjects = db.Projects.ToList();
            projects.MyProjects = projHelper.ListUserProjects(User.Identity.GetUserId()).ToList();

            //ViewBag.ProjectManager = db.Users.Find(projects.ProjectManagerId).FullName;


            return View(projects);
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Admin, Submitter, ProjectManager, Developer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);

            ViewBag.ProjectManagerId = new SelectList(rolesHelper.UsersInRole("ProjectManager"), "Id", "FullName");

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/AssignProjectManager
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignProjectManager(int ProjectId, string ProjectManagerId)
        {
            if (ProjectId != 0 && ProjectManagerId != null)
            {
                projHelper.AssignProjectManager(ProjectId, ProjectManagerId);
            }
            return RedirectToAction("Details", new { id = ProjectId });
        }




        // GET: Projects/Create
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }


        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ProjectManagerId,Updated")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Updated = DateTime.Now;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectManagerId = new SelectList(db.Users, "Id", "FullName", project.ProjectManagerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool dispoSign)
        {
            if (dispoSign)
            {
                db.Dispose();
            }
            base.Dispose(dispoSign);
        }
    }
}
