using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;
using Bug_Lite.ViewModels;

namespace Bug_Lite.Controllers
{
    public class HelpController : Controller
    {
        private IssueContext db = new IssueContext();

        public ActionResult Index()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
