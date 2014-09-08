using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Security;

namespace IssueTracker.Models.Data
{
    public class IssueTrackerInitializer : DropCreateDatabaseIfModelChanges<IssueTrackerDbContext>
    {
        protected override void Seed(IssueTrackerDbContext context)
        {

            var users = new List<User>
            {
                // TODO: populate w/ default
            };

            var Projects = new List<Project>
            {
                // TODO: populate w/ default
            };

            var issues = new List<Issue> {
            
                new Issue() 
                { 
                    Title = "Sample", 
                    Description = "A Samle issue.", 
                    CreatedBy = "Agoodner", 
                    CreatedDate = DateTime.Now, 
                    AssignedTo = "Agoodner", 
                }
            };

            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();

            var WorkNotes = new List<WorkNote>
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

            WorkNotes.ForEach(w => context.WorkNotes.Add(w));

            var wnIssue = context.Issues.Where(i => i.IssueId == 1).Single();

            WorkNotes.ForEach(w => wnIssue.WorkNotes.Add(w));

            context.SaveChanges();
        }
    }
}