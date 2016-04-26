using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net;
using ActionMailer.Net.Mvc;
using Bug_Lite.Models;

namespace Bug_Lite.Controllers
{
    public class MailController : MailerBase
    {
        private IssueContext db = new IssueContext();
        
        public EmailResult EmailNotification(int issueId)
        {
            Issue issue = db.Issues.Find(issueId);

            // Populate Email Header
            To.Add(issue.FDOTUser.EmailAddress); // 'Assigned To' User's email address
            From = "no-reply@bug-lite.com";
            Subject = "Bug-Lite - New Issue Notification (Issue Number:" + issue.IssueCode + ")";
           
            return Email("EmailNotification", issue);
        }

        public EmailResult EditEmailNotification(int issueId)
        {
            Issue issue = db.Issues.Find(issueId);

            // Populate Email Header
            To.Add(issue.FDOTUser.EmailAddress);  // 'Assigned To' User's email address
            To.Add(issue.CreatedBy.EmailAddress); // 'Created By' User's email address
            From = "no-reply@bug-lite.com";
            Subject = "Bug-Lite - Edit Issue Notification (Issue Number:" + issue.IssueCode + ")";

            return Email("EditEmailNotification", issue);
        }
    }
}
