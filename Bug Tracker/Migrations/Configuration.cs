namespace Bug_Tracker.Migrations
{
    using Bug_Tracker.Helpers;
    using Bug_Tracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Bug_Tracker.Models.ApplicationDbContext context)
        {
            #region Roles
            var roleManager = new RoleManager<IdentityRole>(
                                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            #endregion

            #region Add User creation here
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.Email == "katieliz@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "katieliz@gmail.com",
                    Email = "katieliz@gmail.com",
                    FirstName = "Katie",
                    LastName = "Rosario",
                    DisplayName = "OwnerAdmin"
                };


                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "Admin");

            }


            if (!context.Users.Any(u => u.Email == "JasonTwichell@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "JasonTwichell@coderfoundry.com",
                    Email = "JasonTwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Prof"
                };


                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "Developer");

            }

            if (!context.Users.Any(u => u.Email == "submitter@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "submitter@mailinator.com",
                    Email = "submitter@mailinator.com",
                    FirstName = "Ronald",
                    LastName = "Weasley",
                    DisplayName = "Ronnie"
                };


                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "Submitter");

            }

            if (!context.Users.Any(u => u.Email == "projectmanager@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "projectmanager@mailinator.com",
                    Email = "projectmanager@mailinator.com",
                    FirstName = "Hermonie",
                    LastName = "Granger",
                    DisplayName = "MissWizard"
                };


                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "ProjectManager");

            }

            #endregion

            #region Load Up Ticket Types
            context.TicketTypes.AddOrUpdate(
                t => t.Name, 
                    new TicketType { Name = "Defect", Description = "Represents the classic Bug in software."},
                    new TicketType { Name = "New Functionality", Description = "Represents request for new funtionality." },
                    new TicketType { Name = "Training", Description = "Represents request for training." },
                    new TicketType { Name = "Videos", Description = "Represents training video request." }
                );
            #endregion

            #region Load Up Ticket Priorities
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate"},
                    new TicketPriority { Name = "High"},
                    new TicketPriority { Name = "Medium"},
                    new TicketPriority { Name = "Low"},
                    new TicketPriority { Name = "None"}
                );
            #endregion

            #region Load Up Ticket Statuses
            context.TicketStatus.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New", Description = "Newly created and unasssigned." },
                    new TicketStatus { Name = "Assigned", Description = "A ticket that is assigned to a developer." },
                    new TicketStatus { Name = "UnAssigned", Description = "A ticket previously assigned but is no longer assigned to a developer." },
                    new TicketStatus { Name = "Completed", Description = "A ticket that has been solved." },
                    new TicketStatus { Name = "Archived", Description = "A solved ticket that has been included in a release." }
                );
            #endregion

            #region Seed a Demo Project
            context.Projects.AddOrUpdate(
                t => t.Name,
                    new Project { 
                        Name = "Seeded Project", 
                        Description = "A seeded project.",
                        Created = DateTime.Now
                    });

            context.SaveChanges();
            #endregion

            #region Seed a Demo Ticket

            var seededProjectId = context.Projects.FirstOrDefault(p => p.Name == "Seeded Project").Id;

            var seededTicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id;

            var seededTicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id;

            var seededTicketStatusId = context.TicketStatus.FirstOrDefault(t => t.Name == "New").Id;

            var seededSubmitterId = context.Users.FirstOrDefault(u => u.Email == "submitter@mailinator.com").Id;

            var projHelper = new ProjectsHelper();
            projHelper.AddUserToProject(seededSubmitterId, seededProjectId);

            context.Tickets.AddOrUpdate(
                t => t.Title,
                    new Ticket() {
                        Title = "Seed Ticket 1",
                        Description = "There is a defect in the software",
                        Created = DateTime.Now,
                        ProjectId = seededProjectId,
                        TicketTypeId = seededTicketTypeId, 
                        TicketPriorityId = seededTicketPriorityId,
                        TicketStatusId = seededTicketStatusId,
                        SubmitterId = seededSubmitterId
                    });
            #endregion

        }
    }
}

