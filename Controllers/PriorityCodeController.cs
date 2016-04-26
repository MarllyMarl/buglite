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
    public class PriorityCodeController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /PriorityCode/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var priorityCodes = db.PriorityCodes
                .OrderBy(i => i.Priority);

            return View(priorityCodes.ToList());
        }

        // GET: /PriorityCode/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /PriorityCode/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(PriorityCode prioritycode)
        {
            if (ModelState.IsValid)
            {
                db.PriorityCodes.Add(prioritycode);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Priority " + prioritycode.Priority + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(prioritycode);
        }
        
        // GET: /PriorityCode/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            PriorityCode prioritycode = db.PriorityCodes.Find(id);
            return View(prioritycode);
        }

        // POST: /PriorityCode/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(PriorityCode prioritycode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prioritycode).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + prioritycode.Priority + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(prioritycode);
        }

        // POST: /IssueType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PriorityCode prioritycode = db.PriorityCodes.Find(id);
                    // Delete Priority
                    db.PriorityCodes.Remove(prioritycode);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Priority " + prioritycode.Priority + " has been deleted";
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