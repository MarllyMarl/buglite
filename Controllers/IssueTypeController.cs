using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;
using Bug_Lite.HelperClasses;

namespace Bug_Lite.Controllers
{ 
    public class IssueTypeController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /IssueType/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var issueTypes = db.IssueTypes
                .OrderBy(i => i.Type);

            return View(issueTypes.ToList());
        }

        // GET: /IssueType/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /IssueType/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                db.IssueTypes.Add(issueType);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Issue Type " + issueType.Type + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(issueType);
        }
        
        // GET: /IssueType/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            IssueType issueType = db.IssueTypes.Find(id);
            return View(issueType);
        }

        // POST: /IssueType/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueType).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + issueType.Type + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(issueType);
        }

        // POST: /IssueType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IssueType issueType = db.IssueTypes.Find(id);
                    // Delete IssueType
                    db.IssueTypes.Remove(issueType);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Issue Type " + issueType.Type + " has been deleted";
                    string varTempData = TempData["Message"].ToString();
                }
            }
            catch (Exception)
            {
                // Redirect to error page
                return Redirect("");
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}