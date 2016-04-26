using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;
using Bug_Lite.ViewModels;
using System.Security.Principal;
using Bug_Lite.HelperClasses;

namespace Bug_Lite.Controllers
{
    public class AdminController : Controller
    {
        private IssueContext db = new IssueContext();
                
        [CustomAuthorizeAttribute(Roles = "Admin,User")]        
        public ViewResult Index()
        {            
            var viewModel = new AdminData();            
            AuthorizationContext context = new AuthorizationContext();

            // Retrieve the Current User
            string curUser = HttpContext.User.Identity.Name;
            
            if (curUser.Contains(@"\"))
            {
                curUser = curUser.Split('\\')[1];
            }
             // Get FDOTUser Info
            FDOTUser getuser = db.FDOTUsers.Single(u => u.Username == curUser);
            // Get their role
            ViewBag.UserRole = getuser.UserRole.Role;  

            return View();            
        }
    }
}
