using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;            // Database.SetInitialize 
using Bug_Lite.Models;               // IssueInitializer

namespace Bug_Lite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Commented out for Release Deployment
            //Database.SetInitializer<IssueContext>(new IssueInitializer());
            //Database.SetInitializer<IssueContext>(new DropCreateDatabaseIfModelChanges<IssueContext>());
            //Database.SetInitializer<IssueContext>(null);

            //System.Web.Optimization.BundleTable.Bundles.Add(new System.Web.Optimization.ScriptBundle("~/bundle/js")
            //  .Include("~/Scripts/*.js"));
            //System.Web.Optimization.BundleTable.Bundles.Add(new System.Web.Optimization.StyleBundle("~/bundle/css")
            //  .Include("~/Styles/*.css"));

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}