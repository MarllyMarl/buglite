using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.Models;

namespace Bug_Lite.Static_Classes
{
    public static class Utilities
    {
        public static SelectList PreAppend(this SelectList list, string dataTextField, string dataValueField, int selectedValue)
        {
            var items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Text = dataTextField, Value = dataValueField });

            IEnumerator<SelectListItem> en = list.GetEnumerator();

            while (en.MoveNext())
            {
                SelectListItem sli = en.Current;
                items.Add(sli);
            }

            // Return 
            return new SelectList(items, "Value", "Text", selectedValue);
        }

        /// <summary>
        /// Determines if a user can view a link.
        /// </summary>
        /// <param name="value">Value of an html string</param>
        /// <param name="Roles">Comma delaminated list of roles</param>
        /// <returns>Html string</returns>
        public static MvcHtmlString If(this MvcHtmlString value, string Roles)
        {   
            // Returns original value if the user is authorized, else returns an empty html string.
            return UserAuthorized(Roles) ? value : MvcHtmlString.Empty;
        }

        /// <summary>
        /// Retrieves the current user's full name.
        /// </summary>
        /// <returns>User's full name</returns>
        public static string UserName()
        {
            // Connection to database
            IssueContext db = new IssueContext();

            // Retrieve the current user
            string curUser = HttpContext.Current.User.Identity.Name;

            // Retrieve the user name without domain
            if (curUser.Contains(@"\")) curUser = curUser.Split('\\')[1];

            // Find the user in the database
            FDOTUser user = db.FDOTUsers.Single(u => u.Username == curUser);

            // Return the user's full name.
            return user.Name;
        }

        /// <summary>
        /// Determines if the user is authorized based on the user's role.
        /// </summary>
        /// <param name="Roles">Comma delaminated string of roles</param>
        /// <returns>True/False</returns>
        public static bool UserAuthorized(string Roles)
        {
            // Connection to database
            IssueContext db = new IssueContext();
                        
            // Split comma delaminated string of roles into an array
            string[] roles = Roles.Split(',');

            // Retrieve the current user
            string curUser = HttpContext.Current.User.Identity.Name;

            // Retrieve the current user
            if (curUser.Contains(@"\")) curUser = curUser.Split('\\')[1];

            // Retrieve the user from the database.
            FDOTUser user = db.FDOTUsers.Single(u => u.Username == curUser);

            bool authorized = false;
            
            // Loop thru the roles if the authorized role matches the users role then the user is authorized.
            foreach (string role in roles)
            {
                if (user.UserRole.Role.ToLower() == role.ToLower().Trim())
                {
                    authorized = true;
                }
            }

            return authorized;
        }
    }
}