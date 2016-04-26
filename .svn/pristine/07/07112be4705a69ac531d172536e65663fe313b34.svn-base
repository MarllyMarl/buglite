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
    public class StatusCodeController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /StatusCode/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var statusCodes = db.StatusCodes
                .OrderBy(i => i.Status);

            return View(statusCodes.ToList());
        }

        // GET: /StatusCode/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /StatusCode/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(StatusCode statuscode)
        {
            if (ModelState.IsValid)
            {
                db.StatusCodes.Add(statuscode);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Status " + statuscode.Status + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(statuscode);
        }
        
        // GET: /StatusCode/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            StatusCode statuscode = db.StatusCodes.Find(id);
            return View(statuscode);
        }

        // POST: /StatusCode/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(StatusCode statuscode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statuscode).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + statuscode.Status+ " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(statuscode);
        }

        // POST: /IssueType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StatusCode statuscode = db.StatusCodes.Find(id);
                    // Delete IssueType
                    db.StatusCodes.Remove(statuscode);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Status " + statuscode.Status + " has been deleted";
                    string varTempData = TempData["Message"].ToString();
                }
            }
            catch (Exception e)
            {
                string error = e.Message;

                // Redirect to error page
                return View("Error");
            }

            return RedirectToAction("Index", "StatusCode");
            //return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}