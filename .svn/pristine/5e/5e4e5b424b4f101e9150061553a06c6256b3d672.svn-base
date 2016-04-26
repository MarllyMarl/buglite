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
    public class FDOTReleaseVersionController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /FDOTReleaseVersion/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var releaseversions = db.ReleaseVersions
                .Include(i => i.CADDSoftwareProduct)
                .OrderBy(i => i.CADDSoftwareProduct.CADDSoftware)
                .ThenBy(i => i.ReleaseVersion);

            return View(releaseversions.ToList());
        }

        // GET: /FDOTReleaseVersion/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts, "CADDSoftwareProductId", "CADDSoftware");
            return View();
        } 

        // POST: /FDOTReleaseVersion/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(FDOTReleaseVersion fdotreleaseversion)
        {
            if (ModelState.IsValid)
            {
                db.ReleaseVersions.Add(fdotreleaseversion);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Release Version " + fdotreleaseversion.ReleaseVersion + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts, "CADDSoftwareProductId", "CADDSoftware", fdotreleaseversion.CADDSoftwareProductId);
            return View(fdotreleaseversion);
        }
        
        // GET: /FDOTReleaseVersion/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            FDOTReleaseVersion fdotreleaseversion = db.ReleaseVersions.Find(id);
            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts, "CADDSoftwareProductId", "CADDSoftware", fdotreleaseversion.CADDSoftwareProductId);
            return View(fdotreleaseversion);
        }

        // POST: /FDOTReleaseVersion/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(FDOTReleaseVersion fdotreleaseversion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fdotreleaseversion).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + fdotreleaseversion.ReleaseVersion + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts, "CADDSoftwareProductId", "CADDSoftware", fdotreleaseversion.CADDSoftwareProductId);
            return View(fdotreleaseversion);
        }

        // POST: /FDOTReleaseVersion/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FDOTReleaseVersion fdotreleaseversion = db.ReleaseVersions.Find(id);
                    // Delete Release Version
                    db.ReleaseVersions.Remove(fdotreleaseversion);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Release Version " + fdotreleaseversion.ReleaseVersion + " has been deleted";
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