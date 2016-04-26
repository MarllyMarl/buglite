using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;
using Bug_Lite.Controllers;
using Bug_Lite.Static_Classes;


namespace Bug_Lite.HelperClasses
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {   
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Denied");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // if the user is not authenticated, then redirect to unknown user page.
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Unknown");
                return;
            }

            // Determine if the user is authorized.
            bool authorized = Utilities.UserAuthorized(base.Roles);

            // Redirect to an access denied page.
            if (!authorized) filterContext.Result = new RedirectResult("~/Account/Denied");                       
        }

    }
}