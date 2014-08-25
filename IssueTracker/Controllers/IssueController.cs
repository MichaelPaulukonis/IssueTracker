﻿using System;
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
    public class IssueController : Controller
    {
        private IssueTrackerDbContext db = new IssueTrackerDbContext();

        //
        // GET: /Issue/

        public ViewResult Index()
        {
            return View(db.Issues.ToList());
        }

        //
        // GET: /Issue/NewIssues

        public ViewResult NewIssues()
        {
            var result = db.Issues.Where(i => string.IsNullOrEmpty(i.AssignedTo));
            return View(result.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ViewResult Details(int id)
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                issue.CreatedBy = this.HttpContext.User.Identity.Name;
                issue.CreatedDate = DateTime.Now;
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(issue);
        }
        
        //
        // GET: /Issue/Edit/5
 
        //[Authorize(Roles="Administrators,Manager,Developers")]
        public ActionResult Edit(int id)
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        //[Authorize(Roles = "Administrators,Manager,Developer")]
        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                if (issue.ClosedDate != null) issue.ClosedBy = this.HttpContext.User.Identity.Name;
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5

        //[Authorize(Roles = "Administrators,Manager")]
        public ActionResult Delete(int id)
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

        //[Authorize(Roles = "Administrators,Manager")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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