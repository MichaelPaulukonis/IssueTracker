using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;
using IssueTracker.Models.Data;
using IssueTracker.Utils;

namespace IssueTracker.Controllers
{
    public class HomeController : Controller
    {
        IssueTrackerDbContext ctx = new IssueTrackerDbContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome To IssueTracker";

            return View();
        }

        public ActionResult About()
        {
            var allIssues = ctx.Issues;
            var closedIssues = ctx.Issues.Where(i => i.ClosedBy != null);
            var openIssues = ctx.Issues.Where(i => i.ClosedBy == null);

            var oneWeek = GetClosedInOneWeek(closedIssues);
            var twoWeeks = GetOpenGreaterThanOneWeek(openIssues);
            double oneWeekCount = oneWeek.Count();
            double closedCount = closedIssues.Count();
            var pctOneWeek = oneWeekCount.PercentOf(closedCount);

            ViewBag.CountAllIssues = allIssues.Count();
            ViewBag.CountClosedIssues = closedIssues.Count();
            ViewBag.CountOpenIssues = openIssues.Count();
            ViewBag.OneWeek = oneWeek.Count();
            ViewBag.TwoWeeks = twoWeeks.Count();
            ViewBag.PctOneWeek = pctOneWeek;

            return View();
        }

        private IEnumerable<Issue> GetClosedInOneWeek(IEnumerable<Issue> closedIssues)
        {
            var results = new List<Issue>();
            closedIssues.SafeForEach(i =>
                {
                    if (i.ClosedDate.Value.AddDays(-7) <= i.CreatedDate) results.Add(i);
                });
            return results;
        }

        private IEnumerable<Issue> GetOpenGreaterThanOneWeek(IEnumerable<Issue> openIssues)
        {
            var results = new List<Issue>();
            openIssues.SafeForEach(i =>
                {
                    if (i.CreatedDate.AddDays(14) <= DateTime.Today) results.Add(i);
                });
            return results;
        }
    }
}
