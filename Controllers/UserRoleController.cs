using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;

namespace Bug_Lite.Controllers
{ 
    public class UserRoleController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /UserRole/
        public ViewResult Index()
        {
            var userRoles = db.UserRoles
                .OrderBy(i => i.Role);

            return View(userRoles.ToList());
        }

        // GET: /UserRole/Create
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /UserRole/Create
        [HttpPost]
        public ActionResult Create(UserRole userrole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userrole);
                db.SaveChanges();
                //Sends notification message to the [View]
                TempData["Message"] = "User Role " + userrole.Role + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(userrole);
        }
        
        // GET: /UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            UserRole userrole = db.UserRoles.Find(id);
            return View(userrole);
        }

        // POST: /UserRole/Edit/5
        [HttpPost]
        public ActionResult Edit(UserRole userrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userrole).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + userrole.Role + " have been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(userrole);
        }

        // POST: /UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRole userrole = db.UserRoles.Find(id);
                    // Delete User Role
                    db.UserRoles.Remove(userrole);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "User Role " + userrole.Role + " has been deleted";
                    string varTempData = TempData["Message"].ToString();
                }
            }
            catch (Exception)
            {
                // Redirect to error page
                return Redirect("Error");
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