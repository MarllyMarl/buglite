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
    public class FDOTUserController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /FDOTUser/
        public ViewResult Index()
        {
            var fdotusers = db.FDOTUsers
                .Include(i => i.UserSection)
                .Include(i => i.UserRole)
                .OrderBy(i => i.Name);

            return View(fdotusers.ToList());
        }

        // GET: /FDOTUser/Create
        public ActionResult Create()
        {
            ViewBag.UserSectionId = new SelectList(db.UserSections, "UserSectionId", "Section");
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "Role");

            if (Request.IsAjaxRequest())
            {
                return PartialView("CreateFDOTUser");
            }

            return View();
        } 

        // POST: /FDOTUser/Create
        [HttpPost]
        public ActionResult Create(FDOTUser fdotuser)
        {
            if (ModelState.IsValid)
            {
                db.FDOTUsers.Add(fdotuser);
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "User " + fdotuser.Name + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            ViewBag.UserSectionId = new SelectList(db.UserSections, "UserSectionId", "Section", fdotuser.UserSectionId);
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "Role", fdotuser.UserRoleId);

            return View(fdotuser);
        }
        
        // GET: /FDOTUser/Edit/5
        public ActionResult Edit(int id)
        {
            FDOTUser fdotuser = db.FDOTUsers.Find(id);

            ViewBag.UserSectionId = new SelectList(db.UserSections, "UserSectionId", "Section", fdotuser.UserSectionId);
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "Role", fdotuser.UserRoleId);

            return View(fdotuser);
        }

        // POST: /FDOTUser/Edit/5
        [HttpPost]
        public ActionResult Edit(FDOTUser fdotuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fdotuser).State = EntityState.Modified;
                db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + fdotuser.Name + " has been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }

            ViewBag.UserSectionId = new SelectList(db.UserSections, "UserSectionId", "Section", fdotuser.UserSectionId);
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "Role", fdotuser.UserRoleId);

            return View(fdotuser);
        }

        // POST: /UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FDOTUser fdotuser = db.FDOTUsers.Find(id);
                    // Delete User
                    db.FDOTUsers.Remove(fdotuser);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "User " + fdotuser.Name + " has been deleted";
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