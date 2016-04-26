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
    public class UserSectionController : Controller
    {
        private IssueContext db = new IssueContext();

        // GET: /UserSection/
        public ViewResult Index()
        {
            var userSections = db.UserSections
                .OrderBy(i => i.Section);

            return View(userSections.ToList());
        }

        // GET: /UserSection/Create
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /UserSection/Create
        [HttpPost]
        public ActionResult Create(UserSection usersection)
        {
            if (ModelState.IsValid)
            {
                db.UserSections.Add(usersection);
               // db.SaveChanges();
                //Sends notification message to the [View]
                TempData["Message"] = "User Section " + usersection.Section + " has been created";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");  
            }

            return View(usersection);
        }
        
        // GET: /UserSection/Edit/5
        public ActionResult Edit(int id)
        {
            UserSection usersection = db.UserSections.Find(id);
            return View(usersection);
        }

        // POST: /UserSection/Edit/5
        [HttpPost]
        public ActionResult Edit(UserSection usersection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersection).State = EntityState.Modified;
                //db.SaveChanges();
                // Sends notification message to the [View]
                TempData["Message"] = "Changes to " + usersection.Section + " have been made";
                string varTempData = TempData["Message"].ToString();

                return RedirectToAction("Index");
            }
            return View(usersection);
        }

        // POST: /UserSection/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserSection usersection = db.UserSections.Find(id);
                    // Delete UserSection
                    db.UserSections.Remove(usersection);
                    //db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "User Section " + usersection.Section + " has been deleted";
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