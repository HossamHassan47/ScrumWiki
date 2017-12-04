using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlasScrum.DAL
{
    using System.Web.Security;

    using AtlasScrum.Models;

    public class ScrumInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ScrumContext>
    {
        protected override void Seed(ScrumContext context)
        {
            // Roles
            var roles = new List<Role>
                        {
                            new Role { Description = "Scrum Master" },
                            new Role { Description = "Product Owner" },
                            new Role { Description = "Developer" },
                            new Role { Description = "Tester" },
                            new Role { Description = "Documentation" }
                        };

            roles.ForEach(r => context.Roles.Add(r));
            context.SaveChanges();

            // Projects
            var projects = new List<Project>
                           {
                               new Project { ProjectName = "Project 1" },
                               new Project { ProjectName = "Project 2" }
                           };

            projects.ForEach(p => context.Projects.Add(p));
            context.SaveChanges();

            // Team Members
            var teamMembers = new List<TeamMember>
                              {
                                  new TeamMember { FullName = "Hossam" },
                                  new TeamMember { FullName = "Mohamed" },
                                  new TeamMember { FullName = "Ahmed" },
                                  new TeamMember { FullName = "Mostafa" },
                                  new TeamMember { FullName = "Sarah" },
                                  new TeamMember { FullName = "Fahd" },
                                  new TeamMember { FullName = "Aya" }
                              };

            teamMembers.ForEach(m => context.TeamMembers.Add(m));
            context.SaveChanges();

            // Sprints
            var sprints = new List<Sprint>
                          {
                              new Sprint
                              {
                                  SprintName = "Project 1 - Sprint 1",
                                  SprintGoal = "Project 1 Phase 1 Complete",
                                  IsCurrent = true,
                                  ProjectId = 2,
                                  FromDate = DateTime.Parse("12/30/2015"),
                                  ToDate = DateTime.Parse("01/15/2016"),
                                  SprintDemo = "01/15/2016, 4:00 PM, in Meeting Room 1",
                                  DailyScrum = "9:30 - 9:45 AM, in Meeting Room 2"
                              },
                              new Sprint
                              {
                                  SprintName = "IFB - Sprint 1",
                                  SprintGoal = "IFB Doing the job",
                                  IsCurrent = true,
                                  ProjectId = 2,
                                  FromDate = DateTime.Parse("12/30/2015"),
                                  ToDate = DateTime.Parse("01/15/2016"),
                                  SprintDemo = "01/15/2016, 4:00 PM, in Meeting Room 1",
                                  DailyScrum = "9:30 - 9:45 AM, in Meeting Room 2"
                              },
                              new Sprint
                              {
                                  SprintName = "IFB - Sprint 2",
                                  SprintGoal = "Demonstrate a 4.0-ready release on Jan 15",
                                  IsCurrent = true,
                                  ProjectId = 2,
                                  FromDate = DateTime.Parse("12/30/2015"),
                                  ToDate = DateTime.Parse("01/15/2016"),
                                  SprintDemo = "01/15/2016, 4:00 PM, in Meeting Room 1",
                                  DailyScrum = "9:30 - 9:45 AM, in Meeting Room 2"
                              }
                          };

            sprints.ForEach(s => context.Sprints.Add(s));
            context.SaveChanges();

            // Sprint Team
            var sprintTeamMembers = new List<SprintTeamMember>
                                    {
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 1,
                                            TeamMemberId = 1
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 2,
                                            TeamMemberId = 2
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 3,
                                            TeamMemberId = 3
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 3,
                                            TeamMemberId = 4
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 3,
                                            TeamMemberId = 5
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 3,
                                            TeamMemberId = 6
                                        },
                                        new SprintTeamMember
                                        {
                                            SprintId = 3,
                                            RoleId = 4,
                                            TeamMemberId = 7
                                        }
                                    };

            sprintTeamMembers.ForEach(tm => context.SprintTeamMembers.Add(tm));
            context.SaveChanges();

            // Backlog
            var backlogItems = new List<BacklogItem>
                               {
                                   new BacklogItem
                                   {
                                       SprintId = 3,
                                       Description =
                                           "Product Backlog Item 1",
                                       Estimate = 3
                                   },
                                   new BacklogItem
                                   {
                                       SprintId = 3,
                                       Description =
                                           "Product Backlog Item 2",
                                       Estimate = 4
                                   },
                                   new BacklogItem
                                   {
                                       SprintId = 3,
                                       Description = "Product Backlog Item 3",
                                       Estimate = 2
                                   },
                                   new BacklogItem
                                   {
                                       SprintId = 3,
                                       Description =
                                           "Product Backlog Item 4",
                                       Estimate = 3
                                   }
                               };

            backlogItems.ForEach(i => context.BacklogItems.Add(i));
            context.SaveChanges();
        }
    }
}