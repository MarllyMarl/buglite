using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.HelperClasses;
using Bug_Lite.Models;

namespace Bug_Lite.Controllers
{ 
    public class ECSOSupportProductController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /ECSOSupportProduct/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var supportproducts = db.SupportProducts
                .Include(i => i.ECSOSupportCategory)
                .Include(i => i.ApplicationDeveloper)
                .Include(i => i.PrimarySupport)
                .Include(i => i.SecondarySupport)
                .OrderBy(i => i.ECSOSupportCategory.SupportCategory)
                .ThenBy(i => i.SupportProduct);          

            return View(supportproducts.ToList());
        }

        // GET: /ECSOSupportProduct/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories, "ECSOSupportCategoryId", "SupportCategory");
            ViewBag.DeveloperId = new SelectList(db.FDOTUsers.Where(i => i.UserSection.Section == "Development"), "FDOTUserId", "Name");
            ViewBag.PrimarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name");
            ViewBag.SecondarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name");

            return View();
        } 

        // POST: /ECSOSupportProduct/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(ECSOSupportProduct ecsosupportproduct)
        {
            if (ModelState.IsValid)
            {
                db.SupportProducts.Add(ecsosupportproduct);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Support Product " + ecsosupportproduct.SupportProduct + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories, "ECSOSupportCategoryId", "SupportCategory", ecsosupportproduct.ECSOSupportCategoryId);
            ViewBag.DeveloperId = new SelectList(db.FDOTUsers.Where(i => i.UserSection.Section == "Development"), "FDOTUserId", "Name", ecsosupportproduct.DeveloperId);
            ViewBag.PrimarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.PrimarySupportId);
            ViewBag.SecondarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.SecondarySupportId);

            return View(ecsosupportproduct);
        }
        
        // GET: /ECSOSupportProduct/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            ECSOSupportProduct ecsosupportproduct = db.SupportProducts.Find(id);
            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories, "ECSOSupportCategoryId", "SupportCategory", ecsosupportproduct.ECSOSupportCategoryId);
            ViewBag.DeveloperId = new SelectList(db.FDOTUsers.Where(i => i.UserSection.Section == "Development"), "FDOTUserId", "Name", ecsosupportproduct.DeveloperId);
            ViewBag.PrimarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.PrimarySupportId);
            ViewBag.SecondarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.SecondarySupportId);

            return View(ecsosupportproduct);
        }

        // POST: /ECSOSupportProduct/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(ECSOSupportProduct ecsosupportproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecsosupportproduct).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + ecsosupportproduct.SupportProduct + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories, "ECSOSupportCategoryId", "SupportCategory", ecsosupportproduct.ECSOSupportCategoryId);
            ViewBag.DeveloperId = new SelectList(db.FDOTUsers.Where(i => i.UserSection.Section == "Development"), "FDOTUserId", "Name", ecsosupportproduct.DeveloperId);
            ViewBag.PrimarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.PrimarySupportId);
            ViewBag.SecondarySupportId = new SelectList(db.FDOTUsers, "FDOTUserId", "Name", ecsosupportproduct.SecondarySupportId);

            return View(ecsosupportproduct);
        }

        // POST: /ECSOSupportProduct/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ECSOSupportProduct ecsosupportproduct = db.SupportProducts.Find(id);
                    // Delete Support Product
                    db.SupportProducts.Remove(ecsosupportproduct);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Support Product " + ecsosupportproduct.SupportProduct + " has been deleted";
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