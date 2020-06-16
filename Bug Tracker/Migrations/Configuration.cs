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
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Bug_Tracker.Models.ApplicationDbContext context)
        {
            #region Seeded Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


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

            #region Seeded Users
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            if (!context.Users.Any(u => u.Email == "katieliz@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "katieliz@gmail.com",
                    Email = "katieliz@gmail.com",
                    FirstName = "Katie",
                    LastName = "Rosario",
                    DisplayName = "OwnerAdmin",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar-637258669950770737.png"
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
                    DisplayName = "Prof",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy3.png"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "Developer");
            }

            if (!context.Users.Any(u => u.Email == "developer@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "developer@mailinator.com",
                    Email = "developer@mailinator.com",
                    FirstName = "Senior",
                    LastName = "Developer",
                    DisplayName = "NumThreeDev",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy3.png"
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
                    DisplayName = "Ronnie",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy2.png"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "Submitter");

            }

            if (!context.Users.Any(u => u.Email == "submitterdos@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "submitterdos@mailinator.com",
                    Email = "submitterdos@mailinator.com",
                    FirstName = "Missy",
                    LastName = "Submittor",
                    DisplayName = "MissSUB",
                    EmailConfirmed = true,
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
                    DisplayName = "MissWizard",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_girl2.png"
                };


                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id, "ProjectManager");

            }

            //demoUsers

            if (!context.Users.Any(u => u.Email == "demoadmin@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demoadmin@mailinator.com",
                    Email = "demoadmin@mailinator.com",
                    FirstName = "Demmy",
                    LastName = "Tester",
                    DisplayName = "DemoAdmin",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy2.png"
                };

                userManager.Create(user, demoPassword);

                userManager.AddToRoles(user.Id, "Admin");

            }

            if (!context.Users.Any(u => u.Email == "demopm@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demopm@mailinator.com",
                    Email = "demopm@mailinator.com",
                    FirstName = "John",
                    LastName = "Manager",
                    DisplayName = "DemoPM",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy.png"
                };

                userManager.Create(user, demoPassword);

                userManager.AddToRoles(user.Id, "ProjectManager");

            }

            if (!context.Users.Any(u => u.Email == "demosubmitter@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demosubmitter@mailinator.com",
                    Email = "demosubmitter@mailinator.com",
                    FirstName = "Miss",
                    LastName = "Submitter",
                    DisplayName = "DemoSub",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_girl2.png"
                };

                userManager.Create(user, demoPassword);

                userManager.AddToRoles(user.Id, "Submitter");

            }

            if (!context.Users.Any(u => u.Email == "demodeveloper@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demodeveloper@mailinator.com",
                    Email = "demodeveloper@mailinator.com",
                    FirstName = "Johnny",
                    LastName = "Developer",
                    DisplayName = "DemoDev",
                    EmailConfirmed = true,
                    AvatarPath = "/Images/Avatar/avatar_boy2.png"
                };

                userManager.Create(user, demoPassword);

                userManager.AddToRoles(user.Id, "Developer");

            }
            #endregion

            #region Ticket Types
            context.TicketTypes.AddOrUpdate(
                t => t.Name, 
                    new TicketType { Name = "Software", Description = "Represents the classic Bug in software."},
                    new TicketType { Name = "New Feature", Description = "Represents request for new funtionality." },
                    new TicketType { Name = "Incident", Description = "Report and incident or service outage." },
                    new TicketType { Name = "Support", Description = "Request help from customer support." }
                );
            #endregion

            #region Ticket Priorities
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate"},
                    new TicketPriority { Name = "High"},
                    new TicketPriority { Name = "Medium"},
                    new TicketPriority { Name = "Low"},
                    new TicketPriority { Name = "None"}
                );
            #endregion

            #region Ticket Statuses
            context.TicketStatus.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New", Description = "Newly created and unasssigned." },
                    new TicketStatus { Name = "Assigned", Description = "A ticket that is assigned to a developer." },
                    new TicketStatus { Name = "UnAssigned", Description = "A ticket previously assigned but is no longer assigned to a developer." },
                    new TicketStatus { Name = "Completed", Description = "A ticket that has been solved." },
                    new TicketStatus { Name = "Archived", Description = "A solved ticket that has been included in a release." }
                );
            #endregion

            #region Seeded Projects
            context.Projects.AddOrUpdate(
                t => t.Name,
                    new Project { 
                        Name = "Bumblebee Website Project", 
                        Description = "A seeded project for the company website.",
                        Created = DateTime.Now.AddDays(-7),
                    });

             context.Projects.AddOrUpdate(
                 t => t.Name,
                    new Project
                    {
                        Name = "Ladybug E-Commerce Project",
                        Description = "A seeded project for the company's E-Commerce project.",
                        Created = DateTime.Now.AddDays(-7),
                    });

             context.Projects.AddOrUpdate(
                  t => t.Name,
                    new Project
                    {
                        Name = "Butterfly Internal Employee Hub",
                        Description = "A seeded project for the employee software system.",
                        Created = DateTime.Now.AddDays(-7),
                    });

            #endregion

            context.SaveChanges();

            CreateTickets();
        }

        private void Create(ApplicationUser user, string demoPassword)
        {
            throw new NotImplementedException();
        }

        public void CreateTickets(int numberOfTix = 10)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Random rand = new Random();
            var rolesHelper = new Bug_Tracker.Helpers.RolesHelper();
            var projHelper = new Bug_Tracker.Helpers.ProjectsHelper();

            var developers = rolesHelper.UsersInRole("Developer").ToList();
            var submitters = rolesHelper.UsersInRole("Submitter").ToList();

            var seedTicketType = db.TicketTypes.Select(t => t.Id).ToList();
            var seedTicketPriority = db.TicketPriorities.Select(t => t.Id).ToList();
            //var seedTicketStatus = db.TicketStatus.FirstOrDefault(t => t.Name == "New").Id;
            var seedTicketStatus = db.TicketStatus.Select(t => t.Id).ToList();
            var projects = db.Projects.ToList();

            foreach (var project in projects)
            {
                var seedSub = submitters[rand.Next(0, submitters.Count)];
                projHelper.AddUserToProject(seedSub.Id, project.Id);

                for (int j = 1; j <= 10; j++)
                {
                    db.Tickets.AddOrUpdate(t => t.Title, new Ticket
                    {
                        Title = $"Ticket {db.Tickets.Count() + 1}",
                        ProjectId = project.Id,
                        Description = "This is a seeded demo Ticket.",
                        TicketTypeId = seedTicketType[rand.Next(0, seedTicketType.Count)],
                        TicketPriorityId = seedTicketPriority[rand.Next(0, seedTicketPriority.Count)],
                        TicketStatusId = seedTicketStatus[rand.Next(0, seedTicketStatus.Count)],
                        SubmitterId = seedSub.Id,
                        Created = DateTime.Now.AddDays(-7),
                    });
                }
                db.SaveChanges();
            }
        }

    }
}

