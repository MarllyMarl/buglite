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
    public class WindowsOperatingSystemController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /WindowsOperatingSystem/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var operatingSystems = db.OperatingSystems
                .OrderBy(i => i.OperatingSystem);

            return View(operatingSystems.ToList());
        }

        // GET: /WindowsOperatingSystem/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /WindowsOperatingSystem/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(WindowsOperatingSystem windowsoperatingsystem)
        {
            if (ModelState.IsValid)
            {
                db.OperatingSystems.Add(windowsoperatingsystem);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Operating System " + windowsoperatingsystem.OperatingSystem + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(windowsoperatingsystem);
        }
        
        // GET: /WindowsOperatingSystem/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            WindowsOperatingSystem windowsoperatingsystem = db.OperatingSystems.Find(id);
            return View(windowsoperatingsystem);
        }

        // POST: /WindowsOperatingSystem/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(WindowsOperatingSystem windowsoperatingsystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(windowsoperatingsystem).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + windowsoperatingsystem.OperatingSystem + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(windowsoperatingsystem);
        }

        // POST: /WindowsOperatingSystem/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    WindowsOperatingSystem windowsoperatingsystem = db.OperatingSystems.Find(id);
                    // Delete Operating System
                    db.OperatingSystems.Remove(windowsoperatingsystem);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Operating System " + windowsoperatingsystem.OperatingSystem + " has been deleted";
                    string varTempData = TempData["Message"].ToString();
                }
            }

            catch (Exception)
            {
                // Redirect to error page - TO DO
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