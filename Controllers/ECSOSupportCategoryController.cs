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
    public class ECSOSupportCategoryController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /ECSOSupportCategory/
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ViewResult Index()
        {
            var supportcategories = db.SupportCategories
                .OrderBy(i => i.SupportCategory);

            return View(supportcategories.ToList());
        }

        // GET: /ECSOSupportCategory/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /ECSOSupportCategory/Create
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Create(ECSOSupportCategory ecsosupportcategory)
        {
            if (ModelState.IsValid)
            {
                db.SupportCategories.Add(ecsosupportcategory);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Support Category " + ecsosupportcategory.SupportCategory + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(ecsosupportcategory);
        }
        
        // GET: /ECSOSupportCategory/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(int id)
        {
            ECSOSupportCategory ecsosupportcategory = db.SupportCategories.Find(id);

            return View(ecsosupportcategory);
        }

        // POST: /ECSOSupportCategory/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "Admin")]  
        public ActionResult Edit(ECSOSupportCategory ecsosupportcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecsosupportcategory).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + ecsosupportcategory.SupportCategory + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }

            return View(ecsosupportcategory);
        }

        // POST: /ECSOSupportCategory/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ECSOSupportCategory ecsosupportcategory = db.SupportCategories.Find(id);
                    // Delete Support Category
                    db.SupportCategories.Remove(ecsosupportcategory);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Support Category " + ecsosupportcategory.SupportCategory + " has been deleted";
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