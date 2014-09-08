using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Security;

namespace IssueTracker.Models.Data
{
    public class IssueTrackerInitializer : DropCreateDatabaseAlways<IssueTrackerDbContext>
    {
        protected override void Seed(IssueTrackerDbContext context)
        {

            var defaultUser = new User() {
                    ID = 1,
                    Name = "user",
                    FirstName = "First",
                    LastName = "Last"
            };

            var users = new List<User>
            {
                new User() {
                    ID = 0,
                    Name = "admin"
                },
                defaultUser
            };

            users.ForEach(i => context.Users.Add(i));
            context.SaveChanges();

            var defaultProject = new Project()
            {
                ID = 0,
                Name = "Seed Project",
                Description = "Auto-generated Seed Project",
                DefaultUser = defaultUser.Name
            };

            var projects = new List<Project>
            {
                defaultProject  
            };

            projects.ForEach(i => context.Projects.Add(i));
            context.SaveChanges();

            var issues = new List<Issue> {
            
                new Issue() 
                { 
                    Title = "Sample", 
                    Description = "A Sample issue.", 
                    CreatedBy = defaultUser.Name,
                    CreatedDate = DateTime.Now, 
                    AssignedTo = defaultUser.Name, 
                    Project = defaultProject.ID
                }
            };

            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();

            var workNotes = new List<WorkNote>
            {
                new WorkNote() 
                { 
                    IssueId = 1, 
                    EnteredBy = "Agoodner",
                    Detail = "A Sample Note on a sample issue.", 
                    LoggedDate = DateTime.Now, 
                    AttachedIssue = issues[0]
                },
                new WorkNote() 
                { 
                    IssueId = 1, 
                    EnteredBy = "Agoodner",
                    Detail = "Another Sample Note on a sample issue", 
                    LoggedDate = DateTime.Now, 
                    AttachedIssue = issues[0]
                }
            };

            workNotes.ForEach(w => context.WorkNotes.Add(w));

            var wnIssue = context.Issues.Where(i => i.IssueId == 1).Single();

            workNotes.ForEach(w => wnIssue.WorkNotes.Add(w));

            context.SaveChanges();
        }
    }
}