using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;
using IssueTracker.Models.Data;

namespace IssueTracker.Controllers
{ 
    //[Authorize(Roles="Administrators,Manager,Developer,User")]
    public class WorkNoteController : Controller
    {
        private IssueTrackerDbContext db = new IssueTrackerDbContext();

        //
        // GET: /WorkNote/

        public ViewResult Index()
        {
            var worknotes = db.WorkNotes.Include(w => w.AttachedIssue);
            return View(worknotes.ToList());
        }

        //
        // GET: /WorkNote/Details/5

        public ViewResult Details(int id)
        {
            WorkNote worknote = db.WorkNotes.Find(id);
            return View(worknote);
        }

        //
        // GET: /WorkNote/Create

        public ActionResult Create()
        {
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Title");
            return View();
        } 

        //
        // POST: /WorkNote/Create

        [HttpPost]
        public ActionResult Create(WorkNote worknote)
        {
            if (ModelState.IsValid)
            {
                var issueId = db.Issues.Where(i => i.IssueId == worknote.IssueId).SingleOrDefault();
                worknote.EnteredBy = this.HttpContext.User.Identity.Name;
                worknote.LoggedDate = DateTime.Now;
                db.WorkNotes.Add(worknote);
                db.SaveChanges();
                return RedirectToAction("Details", "Issue", new { id = issueId.IssueId });   
            }

            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Title", worknote.IssueId);
            return View(worknote);
        }
        
        //
        // GET: /WorkNote/Edit/5
 
        public ActionResult Edit(int id)
        {
            WorkNote worknote = db.WorkNotes.Find(id);
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Title", worknote.IssueId);
            return View(worknote);
        }

        //
        // POST: /WorkNote/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkNote worknote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worknote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssueId = new SelectList(db.Issues, "IssueId", "Title", worknote.IssueId);
            return View(worknote);
        }

        //
        // GET: /WorkNote/Delete/5
 
        public ActionResult Delete(int id)
        {
            WorkNote worknote = db.WorkNotes.Find(id);
            return View(worknote);
        }

        //
        // POST: /WorkNote/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            WorkNote worknote = db.WorkNotes.Find(id);
            db.WorkNotes.Remove(worknote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}