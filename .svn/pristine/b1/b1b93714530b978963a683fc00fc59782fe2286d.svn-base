using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;
using System.Text;
using Bug_Lite.HelperClasses;

namespace Bug_Lite.Controllers
{ 
    public class ContactController : Controller 
    {
        private IssueContext db = new IssueContext();

        #region Views

        // GET: /Contact/
        public ActionResult Index()
        {
            var contacts = db.Contacts
                .OrderBy(i => i.ContactName);
  
            return View(contacts.ToList());
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Contact contact = db.Contacts.Find(id);
                return View(contact);
            }

            catch (Exception)
            {
                // Redirect to error page - TO DO
                return Redirect("");
            }
        }

        public ActionResult PartialIndex(string contactFilter)
        {
            if (Request.IsAjaxRequest())
            {
                var contacts = db.Contacts.
                    Where(i => i.ContactName == contactFilter).
                    OrderBy(i => i.ContactName).ToList();

                return PartialView("ContactIndex", contacts);
            }
            else
            {
                return PartialView("ContactIndex");
            }
        }

        public ActionResult SearchContacts()
        {
            StringBuilder sb = new StringBuilder();
            var contacts = db.Contacts.ToList();

            sb.AppendFormat("<table id='gradient-style'>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<th>Name</th>");
            sb.AppendFormat("<th>District</th>");
            sb.AppendFormat("<th>Company</th>");
            //sb.AppendFormat("<th>Number</th>");
            //sb.AppendFormat("<th>Email</th>");
            sb.AppendFormat("</tr>");

            foreach (Contact c in contacts)
            {
                sb.AppendFormat("<tr id={0}>", c.ContactId);
                sb.AppendFormat("<td>{0}</td>", c.ContactName);
                sb.AppendFormat("<td>{0}</td>", c.District);
                sb.AppendFormat("<td>{0}</td>", c.Company);
                //sb.AppendFormat("<td>{0}</td>", c.ContactNumber);
                //sb.AppendFormat("<td>{0}</td>", c.EmailAddress);
                sb.AppendFormat("</tr>");
            }
            sb.AppendFormat("</table>");

            Response.Write(sb.ToString());
            return null;
        }

        //public ActionResult SearchContacts(string contactFilter)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    var contacts = db.Contacts.Where(i => i.ContactName.Contains(contactFilter)).OrderBy(i => i.ContactName);

        //    sb.AppendFormat("<table id='gradient-style'>");
        //    sb.AppendFormat("<tr>");
        //    sb.AppendFormat("<th>Name</th>");
        //    sb.AppendFormat("<th>District</th>");
        //    sb.AppendFormat("<th>Company</th>");
        //    //sb.AppendFormat("<th>Number</th>");
        //    //sb.AppendFormat("<th>Email</th>");
        //    sb.AppendFormat("</tr>");

        //    foreach (Contact c in contacts)
        //    {
        //        sb.AppendFormat("<tr id={0}>", c.ContactId);
        //        sb.AppendFormat("<td>{0}</td>", c.ContactName);
        //        sb.AppendFormat("<td>{0}</td>", c.District);
        //        sb.AppendFormat("<td>{0}</td>", c.Company);
        //        //sb.AppendFormat("<td>{0}</td>", c.ContactNumber);
        //        //sb.AppendFormat("<td>{0}</td>", c.EmailAddress);
        //        sb.AppendFormat("</tr>");
        //    }
        //    sb.AppendFormat("</table>");

        //    Response.Write(sb.ToString());
        //    return null;
        //}

        // GET: /Contact/Create

        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("ContactCreate");
            }

            return View();
        }

        // POST: /Contact/Create
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    //db.Contacts.Add(contact);
                    //db.SaveChanges();
                    //Response.Write(contact.ContactId);
                    return null;
                }

                return PartialView("ContactCreate", contact);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Contacts.Add(contact);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Contact " + contact.ContactName + " has been created";
                    string varTempData = TempData["Message"].ToString();

                    return RedirectToAction("Index");
                }

                return View(contact);
            }
        }

        // POST: /Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();

                    // Sends notification message to the [View]
                    TempData["Message"] = "Changes to " + contact.ContactName + " have been made";
                    string varTempData = TempData["Message"].ToString();

                    return RedirectToAction("Index");
                }
            }

            catch (Exception)
            {
                // Redirect to error page
                return Redirect("");
            }

            return View(contact);
        }

        // POST: /Contact/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contact contact = db.Contacts.Find(id);
                    // Delete Contact
                    db.Contacts.Remove(contact);
                    db.SaveChanges();
                    // Sends notification message to the [View]
                    TempData["Message"] = "Contact " + contact.ContactName + " has been deleted";
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

        #endregion

        #region Json Calls

        // GET: /
        [HttpGet]
        public JsonResult JsonSearchContacts(string contactFilter)
        {
            var contacts = db.Contacts.Where(i => i.ContactName == contactFilter).OrderBy(i => i.ContactName);    
        
            return Json(contacts.ToArray());
        }

        // GET: /
        [HttpGet]
        public JsonResult SearchContactsById(int id)
        {
            var data = db.Contacts.Single(i => i.ContactId == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}