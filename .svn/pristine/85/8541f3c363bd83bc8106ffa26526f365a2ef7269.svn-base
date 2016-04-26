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
    public class CADDSoftwareProductController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /CADDSoftwareProduct/
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ViewResult Index()
        {
            var caddSoftwareProducts = db.CADDSoftwareProducts
                .OrderBy(i => i.CADDSoftware);
            
            return View(caddSoftwareProducts.ToList());
        }

        // GET: /CADDSoftwareProduct/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /CADDSoftwareProduct/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(CADDSoftwareProduct caddsoftwareproduct)
        {
            if (ModelState.IsValid)
            {
                db.CADDSoftwareProducts.Add(caddsoftwareproduct);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "CADD Software " + caddsoftwareproduct.CADDSoftware + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(caddsoftwareproduct);
        }
        
        // GET: /CADDSoftwareProduct/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            CADDSoftwareProduct caddsoftwareproduct = db.CADDSoftwareProducts.Find(id);
            return View(caddsoftwareproduct);
        }

        // POST: /CADDSoftwareProduct/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(CADDSoftwareProduct caddsoftwareproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caddsoftwareproduct).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + caddsoftwareproduct.CADDSoftware + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(caddsoftwareproduct);
        }

        // POST: /CADDSoftwareProduct/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CADDSoftwareProduct caddsoftwareproduct = db.CADDSoftwareProducts.Find(id);
                    // Delete CADD Software Product
                    db.CADDSoftwareProducts.Remove(caddsoftwareproduct);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "CADD Software " + caddsoftwareproduct.CADDSoftware + " has been deleted";
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