﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Lite.HelperClasses;
using Bug_Lite.Models;
using Bug_Lite.Static_Classes;
using PagedList;
using Newtonsoft.Json;

/*
 *  *** DEPLOYMENT NOTES ***
 *  The Redirect Path of the Edit method differs when deploying to server:
 *  public ActionResult Edit(Issue issue, HttpPostedFileBase uploadFile)
 * 
    // Return to Details Page *ABFMGD*
    return Redirect("/Issues/Details/" + issue.IssueId);

    // Uncomment the line below for Deployment to Server due 
    to the difference in Physical Path Location. *ABFMGD*
    return Redirect("/BugLite/Issues/Details/" + issue.IssueId); 
*/

namespace Bug_Lite.Controllers
{ 
    public class IssuesController : Controller
    {
        private IssueContext db = new IssueContext();
        private const int PAGE_SIZE = 100;

        #region GET ActionResults

        // GET: /Issues/
        public ViewResult Index(string sortOrder,
            int? fDOTUserFilter, int? prevFDOTUserFilter, string issueTypeFilter, string prevIssueTypeFilter,
            string statusFilter, string prevStatusFilter, string priorityFilter, string prevPriorityFilter,
            int? page)
        {
            #region "Add Paging"
            // If Changing Pages
            if (Request.HttpMethod == "GET")
            {
                // Update Filter Parameters with Previously Selected Values
                fDOTUserFilter = prevFDOTUserFilter;
                issueTypeFilter = prevIssueTypeFilter;
                statusFilter = prevStatusFilter;
                priorityFilter = prevPriorityFilter;
            }
            else
            {
                // If Filters Change, Re-Set to First Page
                page = 1;
            }

            // Store Selected Filter Values for View Access
            ViewBag.PrevFDOTUserFilter = fDOTUserFilter;
            ViewBag.PrevIssueTypeFilter = issueTypeFilter;
            ViewBag.PrevStatusFilter = statusFilter;
            ViewBag.PrevPriorityFilter = priorityFilter;

            // Set Page Size
            int pageSize = PAGE_SIZE;

            // Set Page Number
            int pageNumber = (page ?? 1);
            #endregion

            // Create Database LINQ Query
            var issues = db.Issues
            .Include(i => i.IssueType)
            .Include(i => i.ECSOSupportCategory)
            .Include(i => i.ECSOSupportProduct)
            .Include(i => i.FDOTUser)
            .Include(i => i.PriorityCode)
            .Include(i => i.StatusCode)
            .Include(i => i.CADDSoftwareProduct)
            .Include(i => i.FDOTReleaseVersion)
            .Include(i => i.WindowsOperatingSystem)
            .Include(i => i.IssueHistory);

            #region "Apply Filtering"
            // Filter by User
            if (fDOTUserFilter == null) // Default to Current User
            {
                // Retrieve the Current User
                string curUser = string.Empty;
                curUser = HttpContext.User.Identity.Name;

                if (curUser.Contains(@"\"))
                    curUser = curUser.Split('\\')[1];

                FDOTUser user = db.FDOTUsers.Single(u => u.Username == curUser);

                // Filter by Current User's FDOTUserId
                issues = issues.Where(i => i.FDOTUserId == user.FDOTUserId);

                // Create FDOT User SelectList with Default User Selected
                SelectList usersList = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name), "FDOTUserId", "Name");

                ViewBag.FDOTUserFilter = usersList.PreAppend("--- Select All ---", "0", user.FDOTUserId);
            }
            else if (System.Convert.ToInt32(fDOTUserFilter) > 0) // If Selected User
            {
                // Filter by Selected User
                int fdotUserId = System.Convert.ToInt32(fDOTUserFilter);

                issues = issues.Where(i => i.FDOTUserId == fdotUserId);

                // Create FDOT User SelectList with Select User
                SelectList usersList = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name), "FDOTUserId", "Name");

                ViewBag.FDOTUserFilter = usersList.PreAppend("--- Select All ---", "0", fdotUserId);
            }
            else // All Users
            {
                // Create FDOT User SelectList with All Users
                SelectList usersList = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name), "FDOTUserId", "Name");

                ViewBag.FDOTUserFilter = usersList.PreAppend("--- Select All ---", "0", 0);
            }

            // If Filter by Issue Type
            if (!String.IsNullOrEmpty(issueTypeFilter))
            {
                // Filter by Issue Type
                int issueTypeId = System.Convert.ToInt32(issueTypeFilter);
                issues = issues.Where(i => i.IssueTypeId == issueTypeId);

                // Create Issue Type SelectList with Selected Value
                ViewBag.IssueTypeFilter = new SelectList(db.IssueTypes.OrderBy(i => i.Type),
                    "IssueTypeId", "Type", issueTypeFilter);
            }
            else
            {
                // Create Issue Type SelectList
                ViewBag.IssueTypeFilter = new SelectList(db.IssueTypes.OrderBy(i => i.Type),
                    "IssueTypeId", "Type");
            }

            // If Filter by Status
            if (!String.IsNullOrEmpty(statusFilter))
            {
                // Filter by Issue Status
                int statusCodeId = System.Convert.ToInt32(statusFilter);

                issues = issues.Where(i => i.StatusCodeId == statusCodeId);

                // Create Status SelectList with Selected Value
                ViewBag.StatusFilter = new SelectList(db.StatusCodes, "StatusCodeId", "Status", statusFilter);
            }
            else
            {
                // Create Status SelectList
                ViewBag.StatusFilter = new SelectList(db.StatusCodes, "StatusCodeId", "Status");
            }

            // If Filter by Priority
            if (!String.IsNullOrEmpty(priorityFilter))
            {
                // Filter by Issue Priority
                int priorityCodeId = System.Convert.ToInt32(priorityFilter);

                issues = issues.Where(i => i.PriorityCodeId == priorityCodeId);

                // Create Priority SelectList with Selected Value
                ViewBag.PriorityFilter = new SelectList(db.PriorityCodes, "PriorityCodeId", "Priority", priorityFilter);
            }
            else
            {
                // Create Priority SelectList
                ViewBag.PriorityFilter = new SelectList(db.PriorityCodes, "PriorityCodeId", "Priority");
            }
            #endregion

            #region "Apply Sorting"
            // Store Current Sort Order
            ViewBag.CurrentSort = sortOrder;

            // Set Sort Paramers
            ViewBag.IssueCodeSortParam = String.IsNullOrEmpty(sortOrder) ? "IssueCode desc" : "";
            ViewBag.IssueTypeSortParam = sortOrder == "IssueType" ? "IssueType desc" : "IssueType";
            ViewBag.StatusCodeSortParam = sortOrder == "StatusCode" ? "StatusCode desc" : "StatusCode";
            ViewBag.PriorityCodeSortParam = sortOrder == "PriorityCode" ? "PriorityCode desc" : "PriorityCode";
            ViewBag.SubjectSortParam = sortOrder == "Subject" ? "Subject desc" : "Subject";
            ViewBag.ECSOSupportProductSortParam = sortOrder == "ECSOSupportProduct" ? "ECSOSupportProduct desc" : "ECSOSupportProduct";
            ViewBag.FDOTUserSortParam = sortOrder == "FDOTUser" ? "FDOTUser desc" : "FDOTUser";
            ViewBag.CreateDateSortParam = sortOrder == "CreateDate" ? "CreateDate desc" : "CreateDate";

            // Apply Sorting
            switch (sortOrder)
            {
                case "IssueCode desc":
                    issues = issues.OrderByDescending(i => i.IssueCode);
                    break;
                case "IssueType":
                    issues = issues.OrderBy(i => i.IssueType.Type);
                    break;
                case "IssueType desc":
                    issues = issues.OrderByDescending(i => i.IssueType.Type);
                    break;
                case "StatusCode":
                    issues = issues.OrderBy(i => i.StatusCode.Status);
                    break;
                case "StatusCode desc":
                    issues = issues.OrderByDescending(i => i.StatusCode.Status);
                    break;
                case "PriorityCode":
                    issues = issues.OrderBy(i => i.PriorityCode.Priority);
                    break;
                case "PriorityCode desc":
                    issues = issues.OrderByDescending(i => i.PriorityCode.Priority);
                    break;
                case "Subject":
                    issues = issues.OrderBy(i => i.Subject);
                    break;
                case "Subject desc":
                    issues = issues.OrderByDescending(i => i.Subject);
                    break;
                case "ECSOSupportProduct":
                    issues = issues.OrderBy(i => i.ECSOSupportProduct.SupportProduct);
                    break;
                case "ECSOSupportProduct desc":
                    issues = issues.OrderByDescending(i => i.ECSOSupportProduct.SupportProduct);
                    break;
                case "FDOTUser":
                    issues = issues.OrderBy(i => i.FDOTUser.Name);
                    break;
                case "FDOTUser desc":
                    issues = issues.OrderByDescending(i => i.FDOTUser.Name);
                    break;
                case "CreateDate":
                    issues = issues.OrderBy(i => i.CreateDate);
                    break;
                case "CreateDate desc":
                    issues = issues.OrderByDescending(i => i.CreateDate);
                    break;
                default:
                    issues = issues.OrderBy(i => i.IssueCode);
                    break;
            }
            #endregion

            // Return Page Results, Filtered, Sorted
            return View(issues.ToPagedList(pageNumber, pageSize));
            //return View();
        }
     
        // GET: /Issues/Create/
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult Create()
        {
            // Load Active Lookup values
            ViewBag.WindowsOperatingSystemId = new SelectList(db.OperatingSystems.Where(i => i.Active == true).OrderBy(i => i.OperatingSystem), 
                "WindowsOperatingSystemId", "OperatingSystem", 3);          

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts.Where(i => i.Active == true).OrderBy(i => i.CADDSoftware), 
                "CADDSoftwareProductId", "CADDSoftware");

            ViewBag.FDOTReleaseVersionId = new SelectList(db.ReleaseVersions.Where(i => i.Active == true).OrderBy(i => i.ReleaseVersion),
                "FDOTReleaseVersionId", "ReleaseVersion");

            ViewBag.IssueTypeId = new SelectList(db.IssueTypes.Where(i => i.Active == true).OrderBy(i => i.Type), 
                "IssueTypeId", "Type", 4);            

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories.Where(i => i.Active == true).OrderBy(i => i.SupportCategory), 
                "ECSOSupportCategoryId", "SupportCategory");

            ViewBag.ECSOSupportProductId = new SelectList(db.SupportProducts.Where(i => i.Active == true).OrderBy(i => i.SupportProduct),
                "ECSOSupportProductId", "SupportProduct");

            ViewBag.FDOTUserId = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name), 
                "FDOTUserId", "Name");            

            ViewBag.PriorityCodeId = new SelectList(db.PriorityCodes.Where(i => i.Active == true), 
                "PriorityCodeId", "Priority", 3);            

            ViewBag.StatusCodeId = new SelectList(db.StatusCodes.Where(i => i.Active == true).OrderBy(i => i.Status), 
                "StatusCodeId", "Status", 3);

            // Fetches Contacts Data
            var fetchcontact = db.Contacts.Where(i => i.Active == true).OrderBy(i => i.ContactName).ToList().Select
               (i => new { Value = i.ContactId, Text = i.ContactName + "   |  " + i.District + "   |  " + i.Company  });
            ViewBag.Contact = new SelectList(fetchcontact, "Value", "Text");

            //ViewBag.ContactId = new SelectList(db.Contacts.Where(i => i.Active == true).OrderBy(i => i.ContactName),
            //"ContactId", "ContactName");
            //ViewBag.District = new SelectList(db.Contacts.Where(i => i.Active == true).OrderBy(i => i.District),
            //"District", "District");
            //ViewBag.Contact = db.Contacts.Select(p => new SelectListItem { Value = p.ContactName, Text = p.ContactName }).ToList(); 
            //ViewData["Contact-Name"] = new SelectList(db.Contacts.Where(i => i.Active == true).OrderBy(i => i.ContactName),
            //"ContactId", "ContactName");
            
            return View();
        }

        // GET: /Issues/Edit/5
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult Edit(int id)
        {
            Issue issue = db.Issues.Find(id);

            // If Contact Not Null
            if (issue.ContactId != null)
            {
                // Store Contact Values in ViewBag
                ViewBag.ContactName = issue.Contact.ContactName;
                ViewBag.ContactNumber = issue.Contact.ContactNumber;
                ViewBag.EmailAddress = issue.Contact.EmailAddress;
            }
            else
            {
                // Set Contact Fields to Blank
                ViewBag.ContactName = "";
                ViewBag.ContactNumber = "";
                ViewBag.EmailAddress = "";
            }

            // Load Active Lookup Values, Set Selected Value
            ViewBag.WindowsOperatingSystemId = new SelectList(db.OperatingSystems.Where(i => i.Active == true),
                "WindowsOperatingSystemId", "OperatingSystem", issue.WindowsOperatingSystemId);

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts.Where(i => i.Active == true),
                "CADDSoftwareProductId", "CADDSoftware", issue.CADDSoftwareProductId);

            ViewBag.FDOTReleaseVersionId = new SelectList(db.ReleaseVersions.Where(i => i.Active == true), 
                "FDOTReleaseVersionId", "ReleaseVersion", issue.FDOTReleaseVersionId);

            ViewBag.IssueTypeId = new SelectList(db.IssueTypes.Where(i => i.Active == true).OrderBy(i => i.Type),
                "IssueTypeId", "Type", issue.IssueTypeId);

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories.Where(i => i.Active == true).OrderBy(i => i.SupportCategory),
                "ECSOSupportCategoryId", "SupportCategory", issue.ECSOSupportCategoryId);

            ViewBag.ECSOSupportProductId = new SelectList(db.SupportProducts.Where(i => i.Active == true).OrderBy(i => i.SupportProduct), 
                "ECSOSupportProductId", "SupportProduct", issue.ECSOSupportProductId);

            ViewBag.FDOTUserId = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name),
                "FDOTUserId", "Name", issue.FDOTUserId);

            ViewBag.PriorityCodeId = new SelectList(db.PriorityCodes.Where(i => i.Active == true),
                "PriorityCodeId", "Priority", issue.PriorityCodeId);

            ViewBag.StatusCodeId = new SelectList(db.StatusCodes.Where(i => i.Active == true),
                "StatusCodeId", "Status", issue.StatusCodeId);

            // Fetches Contacts Data 
            var fetchcontact = db.Contacts.Where(i => i.Active == true).OrderBy(i => i.ContactName).ToList().Select
               (i => new { Value = i.ContactId, Text = i.ContactName + "   |  " + i.District + "   |  " + i.Company });
            ViewBag.Contact = new SelectList(fetchcontact, "Value", "Text", issue.ContactId);

            //ViewBag.ContactId = new SelectList(db.Contacts.Where(i => i.Active == true).OrderBy(i => i.ContactName),
            //"ContactId", "ContactName", issue.ContactId);
            
            return View(issue);
        }

        // Gets a specific Release Version      
        public ActionResult GetReleaseVersionsList(string caddProductId)
        {
            try
            {
                int productId = Convert.ToInt32(caddProductId);

                // Filter FDOTReleaseVersions by CADDSoftwareProductId
                var releaseVersions = from r in db.ReleaseVersions
                                      where r.CADDSoftwareProductId == productId && r.Active == true
                                      orderby r.ReleaseVersion
                                      select r;

                // Return List of FDOTReleaseVersions for the requested CADD Product
                return Json(new SelectList(releaseVersions.ToArray(), "FDOTReleaseVersionId", "ReleaseVersion"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                // Select All FDOTReleaseVersions
                var releaseVersions = from r in db.ReleaseVersions
                                      where r.Active == true
                                      orderby r.ReleaseVersion
                                      select r;

                // Return List of all Release Versions
                return Json(new SelectList(releaseVersions.ToArray(), "FDOTReleaseVersionId", "ReleaseVersion"), JsonRequestBehavior.AllowGet);
            }
        }

        // Gets a specific Support Product
        public ActionResult GetSupportProductsList(string supportCategoryId)
        {
            try
            {
                int categoryId = Convert.ToInt32(supportCategoryId);

                // Filter ECSOSupportProducts by ECSOSupportCategoryId
                var supportProducts = from sp in db.SupportProducts
                                      where sp.ECSOSupportCategoryId == categoryId && sp.Active == true
                                      orderby sp.SupportProduct
                                      select sp;

                // Return List of Products for the requested Category
                return Json(new SelectList(supportProducts.ToArray(), "ECSOSupportProductId", "SupportProduct"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                // Select All Active ECSOSupportProducts
                var supportProducts = from sp in db.SupportProducts
                                      where sp.Active == true
                                      orderby sp.SupportProduct
                                      select sp;

                // Return List of all Products
                return Json(new SelectList(supportProducts.ToArray(), "ECSOSupportProductId", "SupportProduct"), JsonRequestBehavior.AllowGet);
            }
        }

        // Get a specific Product
        public JsonResult GetProductById(int id)
        {
            int assignmentId = 0;

            // Get selected Product data
            ECSOSupportProduct product = (ECSOSupportProduct)db.SupportProducts.Single(i => i.ECSOSupportProductId == id);

            // Assign default Product support person
            if (product.DeveloperId != null)
            {
                // Auto-assign to application developer
                assignmentId = product.DeveloperId.Value;
            }
            else if (product.PrimarySupportId != null)
            {
                // Auto-assign to primary support personel
                assignmentId = product.PrimarySupportId.Value;
            }
            else if (product.SecondarySupportId != null)
            {
                // Auto-assign to secondary support personel
                assignmentId = product.SecondarySupportId.Value;
            }

            // Return Associated Assignment Id
            return Json(assignmentId, JsonRequestBehavior.AllowGet);
        }

        // GET: /Issues/Details/5
        [CustomAuthorizeAttribute(Roles = "Guest,User,Admin")]
        public ViewResult Details(int id)
        {
            Issue issue = db.Issues.Find(id);
            return View(issue);
        }

        // GET: /Contact/SearchContactsById
        [HttpGet]
        public JsonResult SearchContactsById(int id)
        {
            var data = db.Contacts.Single(i => i.ContactId == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: /Issues/GetAttachment
        [Authorize]
        public FileContentResult GetAttachment(int attachmentId)
        {
            Attachment attachment = db.Attachments.FirstOrDefault(a => a.AttachmentId == attachmentId);

            if (attachment != null)
            {
                return File(attachment.FileData, attachment.ContentType, attachment.FileName);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region POST ActionResults

        // POST: /Issues/Create/
        [HttpPost]
        //[ValidateInput(false)]
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult Create(Issue issue, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                // If Valid Model
                if (ModelState.IsValid)
                {
                    // Retrieve the Current User
                    string curUser = HttpContext.User.Identity.Name;

                    if (curUser.Contains(@"\"))
                    {
                        curUser = curUser.Split('\\')[1];
                    }

                    // *MGD*
                    SelectList selectList = new SelectList(db.FDOTUsers, "FDOTUserId", "Name");
                    ViewBag.UsersList = selectList;

                    // Get FDOTUser Info
                    FDOTUser createdBy = db.FDOTUsers.Single(u => u.Username == curUser);

                    // Set Created By FDOTUserId
                    issue.CreatedById = createdBy.FDOTUserId;

                    // Set Issue Create Date
                    issue.CreateDate = DateTime.Now;

                    // Set Issue Modified Date
                    issue.ModifiedDate = DateTime.Now;

                    // Add New Issue to Database Context
                    db.Issues.Add(issue);

                    // Save Changes
                    db.SaveChanges(); // Need to save to generate IssueId used to calculate IssueCode

                    // Calculate new Issue Code
                    issue.IssueCode = (issue.IssueId + 1000).ToString().PadLeft(6, '0');

                    // For each attached file
                    foreach (var uploadFile in files)
                    {
                        // Create File Attachment and Add to Database Context
                        CreateFileAttachmentRecord(issue.IssueId, uploadFile);
                    }

                    // Set Issue State to Modified
                    db.Entry(issue).State = EntityState.Modified;

                    // Save Changes
                    db.SaveChanges();

                    // Sends notification message to the [View]
                    TempData["Message"] = "Issue Number: " + issue.IssueCode + " has been created";
                    string varTempData = TempData["Message"].ToString();

                    // Create Notification Email(New) via ActionMail.Net
                    //ActionMailer.Net.Mvc.EmailResult emailResult = new MailController().EmailNotification(issue.IssueId);

                    try
                    {
                        // Send Notification Email via ActionMail.Net
                        //emailResult.Deliver();
                    }
                    catch (Exception e)
                    {
                        string error = e.Message;

                        // Logs Exception to a txt file *MGD*
                        //string filePath = @"\\codata\shares\CO\CADD\Users\Marlon\blitelog\BugLiteEmailError.txt";
                        string filePath = @"C:\buglitelog\BugLiteEmailError.txt";
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("Message :" + e.Message + "<br/>" + Environment.NewLine + "StackTrace :" + e.StackTrace +
                               "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                        }
                    }

                    // Return to Index Page
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // Redirect to error page - TO DO
                return Redirect("");
            }

            // Re-Load Active Lookup values
            ViewBag.WindowsOperatingSystemId = new SelectList(db.OperatingSystems.Where(i => i.Active == true),
                "WindowsOperatingSystemId", "OperatingSystem", issue.WindowsOperatingSystemId);

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts.Where(i => i.Active == true),
                "CADDSoftwareProductId", "CADDSoftware", issue.CADDSoftwareProductId);

            ViewBag.FDOTReleaseVersionId = new SelectList(db.ReleaseVersions.Where(i => i.Active == true),
                "FDOTReleaseVersionId", "ReleaseVersion", issue.FDOTReleaseVersionId);

            ViewBag.IssueTypeId = new SelectList(db.IssueTypes.Where(i => i.Active == true).OrderBy(i => i.Type),
                "IssueTypeId", "Type", issue.IssueTypeId);

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories.Where(i => i.Active == true).OrderBy(i => i.SupportCategory),
                "ECSOSupportCategoryId", "SupportCategory", issue.ECSOSupportCategoryId);

            ViewBag.ECSOSupportProductId = new SelectList(db.SupportProducts.Where(i => i.Active == true).OrderBy(i => i.SupportProduct),
                "ECSOSupportProductId", "SupportProduct", issue.ECSOSupportProductId);

            ViewBag.FDOTUserId = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name),
                "FDOTUserId", "Name", issue.FDOTUserId);

            ViewBag.PriorityCodeId = new SelectList(db.PriorityCodes.Where(i => i.Active == true),
                "PriorityCodeId", "Priority", issue.PriorityCodeId);

            ViewBag.StatusCodeId = new SelectList(db.StatusCodes.Where(i => i.Active == true),
                "StatusCodeId", "Status", issue.StatusCodeId);

            // Return to Create Page
            return View(issue);
        }

        // GET: /Issues/Search
        public ViewResult Search(string sortOrder,
            string issueTypeFilter, string prevIssueTypeFilter,
            string eCSOSupportCategoryFilter, string prevECSOSupportCategoryFilter,
            string eCSOSupportProductFilter, string prevECSOSupportProductFilter,
            int? fDOTUserFilter, int? prevFDOTUserFilter,
            string priorityFilter, string prevPriorityFilter,
            string statusFilter, string prevStatusFilter,
            string cADDSoftwareFilter, string prevCADDSoftwareFilter,
            string fDOTReleaseVersionFilter, string prevFDOTReleaseVersionFilter,
            string operatingSystemFilter, string prevOperatingSystemFilter,
            string detailsFilter, string prevDetailsFilter,
            int? page)
        {

            #region "Add Paging"
            // If Changing Pages
            if (Request.HttpMethod == "GET")
            {
                // Update Filter Parameters with Previously Selected Values
                issueTypeFilter = prevIssueTypeFilter;
                eCSOSupportCategoryFilter = prevECSOSupportCategoryFilter;
                eCSOSupportProductFilter = prevECSOSupportProductFilter;
                fDOTUserFilter = prevFDOTUserFilter;
                priorityFilter = prevPriorityFilter;
                statusFilter = prevStatusFilter;
                cADDSoftwareFilter = prevCADDSoftwareFilter;
                fDOTReleaseVersionFilter = prevFDOTReleaseVersionFilter;
                operatingSystemFilter = prevOperatingSystemFilter;
                detailsFilter = prevDetailsFilter;
            }
            else
            {
                // If Filters Change, Re-Set to First Page
                page = 1;
            }

            // Store Selected Filters in ViewBag
            ViewBag.PrevIssueTypeFilter = issueTypeFilter;
            ViewBag.PrevECSOSupportCategoryFilter = eCSOSupportCategoryFilter;
            ViewBag.PrevECSOSupportProductFilter = eCSOSupportProductFilter;
            ViewBag.PrevFDOTUserFilter = fDOTUserFilter;
            ViewBag.PrevPriorityFilter = priorityFilter;
            ViewBag.PrevStatusFilter = statusFilter;
            ViewBag.PrevCADDSoftwareFilter = cADDSoftwareFilter;
            ViewBag.PrevFDOTReleaseVersionFilter = fDOTReleaseVersionFilter;
            ViewBag.PrevOperatingSystemFilter = operatingSystemFilter;
            ViewBag.PrevDetailsFilter = detailsFilter;

            // Set Page Size
            int pageSize = PAGE_SIZE;

            // Set Page Number
            int pageNumber = (page ?? 1);
            #endregion

            // Create Database LINQ Query
            var issues = db.Issues
                .Include(i => i.IssueType)
                .Include(i => i.ECSOSupportCategory)
                .Include(i => i.ECSOSupportProduct)
                .Include(i => i.FDOTUser)
                .Include(i => i.PriorityCode)
                .Include(i => i.StatusCode)
                .Include(i => i.CADDSoftwareProduct)
                .Include(i => i.FDOTReleaseVersion)
                .Include(i => i.WindowsOperatingSystem)
                .Include(i => i.IssueHistory);

            #region "Apply Filtering"
            // If Filter by Issue Type
            if (!String.IsNullOrEmpty(issueTypeFilter))
            {
                // Filter by Issue Type
                int issueTypeId = System.Convert.ToInt32(issueTypeFilter);
                issues = issues.Where(i => i.IssueTypeId == issueTypeId);

                // Create Issue Type SelectList with Selected Value
                ViewBag.IssueTypeFilter = new SelectList(db.IssueTypes.OrderBy(i => i.Type),
                    "IssueTypeId", "Type", issueTypeFilter);
            }
            else
            {
                // Create Issue Type SelectList
                ViewBag.IssueTypeFilter = new SelectList(db.IssueTypes.OrderBy(i => i.Type),
                    "IssueTypeId", "Type");
            }

            // If Filter by ECSO Support Category
            if (!String.IsNullOrEmpty(eCSOSupportCategoryFilter))
            {
                // Filter by ECSO Support Category
                int supportCategoryId = System.Convert.ToInt32(eCSOSupportCategoryFilter);
                issues = issues.Where(i => i.ECSOSupportCategoryId == supportCategoryId);

                // Create ECSO Support Category SelectList with Selected Value
                ViewBag.ECSOSupportCategoryFilter = new SelectList(db.SupportCategories.OrderBy(i => i.SupportCategory),
                    "ECSOSupportCategoryId", "SupportCategory", eCSOSupportCategoryFilter);
            }
            else
            {
                // Create ECSO Support Category SelectList
                ViewBag.ECSOSupportCategoryFilter = new SelectList(db.SupportCategories.OrderBy(i => i.SupportCategory),
                    "ECSOSupportCategoryId", "SupportCategory");
            }

            // If Filter by ECSO Support Product
            if (!String.IsNullOrEmpty(eCSOSupportProductFilter))
            {
                // Filter by ECSO Support Product
                int supportProductId = System.Convert.ToInt32(eCSOSupportProductFilter);
                issues = issues.Where(i => i.ECSOSupportProductId == supportProductId);

                // Create ECSO Support Products SelectList with Selected Value
                ViewBag.ECSOSupportProductFilter = new SelectList(db.SupportProducts.OrderBy(i => i.SupportProduct),
                    "ECSOSupportProductId", "SupportProduct", eCSOSupportProductFilter);
            }
            else
            {
                // Create ECSO Support Products SelectList
                ViewBag.ECSOSupportProductFilter = new SelectList(db.SupportProducts.OrderBy(i => i.SupportProduct),
                    "ECSOSupportProductId", "SupportProduct");
            }

            // Filter by Assigned To
            if (System.Convert.ToInt32(fDOTUserFilter) > 0)
            {
                // Filter by Selected User
                int fdotUserId = System.Convert.ToInt32(fDOTUserFilter);
                issues = issues.Where(i => i.FDOTUserId == fdotUserId);

                // Create Assigned To SelectList with Select User
                ViewBag.FDOTUserFilter = new SelectList(db.FDOTUsers.OrderBy(i => i.Name),
                    "FDOTUserId", "Name");
            }
            else // All Users
            {
                // Create Assigned To SelectList
                ViewBag.FDOTUserFilter = new SelectList(db.FDOTUsers.OrderBy(i => i.Name),
                    "FDOTUserId", "Name");
            }

            // If Filter by Priority
            if (!String.IsNullOrEmpty(priorityFilter))
            {
                // Filter by Issue Priority
                int priorityCodeId = System.Convert.ToInt32(priorityFilter);
                issues = issues.Where(i => i.PriorityCodeId == priorityCodeId);

                // Create Priority SelectList with Selected Value
                ViewBag.PriorityFilter = new SelectList(db.PriorityCodes,
                    "PriorityCodeId", "Priority", priorityFilter);
            }
            else
            {
                // Create Priority SelectList
                ViewBag.PriorityFilter = new SelectList(db.PriorityCodes,
                    "PriorityCodeId", "Priority");
            }

            // If Filter by Status
            if (!String.IsNullOrEmpty(statusFilter))
            {
                // Filter by Issue Status
                int statusCodeId = System.Convert.ToInt32(statusFilter);
                issues = issues.Where(i => i.StatusCodeId == statusCodeId);

                // Create Status SelectList with Selected Value
                ViewBag.StatusFilter = new SelectList(db.StatusCodes,
                    "StatusCodeId", "Status", statusFilter);
            }
            else
            {
                // Create Status SelectList
                ViewBag.StatusFilter = new SelectList(db.StatusCodes,
                    "StatusCodeId", "Status");
            }

            // If Filter by CADD Software
            if (!String.IsNullOrEmpty(cADDSoftwareFilter))
            {
                // Filter by CADD Software Product
                int caddSoftwareId = System.Convert.ToInt32(cADDSoftwareFilter);
                issues = issues.Where(i => i.CADDSoftwareProductId == caddSoftwareId);

                // Create CADD Software SelectList with Selected Value
                ViewBag.CADDSoftwareFilter = new SelectList(db.CADDSoftwareProducts,
                    "CADDSoftwareProductId", "CADDSoftware", cADDSoftwareFilter);
            }
            else
            {
                // Create CADD Software SelectList
                ViewBag.CADDSoftwareFilter = new SelectList(db.CADDSoftwareProducts,
                    "CADDSoftwareProductId", "CADDSoftware");
            }

            // If Filter by FDOT Release Version
            if (!String.IsNullOrEmpty(fDOTReleaseVersionFilter))
            {
                // Filter by FDOT Release Version
                int fdotReleaseId = System.Convert.ToInt32(fDOTReleaseVersionFilter);
                issues = issues.Where(i => i.FDOTReleaseVersionId == fdotReleaseId);

                // Create FDOT Release Versions SelectList with Selected Value
                ViewBag.FDOTReleaseVersionFilter = new SelectList(db.ReleaseVersions,
                    "FDOTReleaseVersionId", "ReleaseVersion", fDOTReleaseVersionFilter);
            }
            else
            {
                // Create FDOT Release Version SelectList
                ViewBag.FDOTReleaseVersionFilter = new SelectList(db.ReleaseVersions,
                    "FDOTReleaseVersionId", "ReleaseVersion");
            }

            // If Filter by Operating System
            if (!String.IsNullOrEmpty(operatingSystemFilter))
            {
                // Filter by Windows Operating System
                int operatingSystemId = System.Convert.ToInt32(operatingSystemFilter);
                issues = issues.Where(i => i.WindowsOperatingSystemId == operatingSystemId);

                // Create Operating Systems SelectList with Selected Value
                ViewBag.OperatingSystemFilter = new SelectList(db.OperatingSystems,
                    "WindowsOperatingSystemId", "OperatingSystem", operatingSystemFilter);
            }
            else
            {
                // Create Operating Systems SelectList
                ViewBag.OperatingSystemFilter = new SelectList(db.OperatingSystems,
                    "WindowsOperatingSystemId", "OperatingSystem");
            }

            // If Filter by Detail Text
            if (!String.IsNullOrEmpty(detailsFilter))
            {
                // Search Issue's Details field for Details Search Text
                issues = issues.Where(i => i.Details.Contains(detailsFilter));

                // Store Details Search Text in ViewBag to send back to page
                ViewBag.DetailsFilter = detailsFilter;
            }
            #endregion

            #region "Apply Sorting"
            // Store Current Sort Order
            ViewBag.CurrentSort = sortOrder;

            // Set Sort Paramers
            ViewBag.IssueCodeSortParam = String.IsNullOrEmpty(sortOrder) ? "IssueCode desc" : "";
            ViewBag.IssueTypeSortParam = sortOrder == "IssueType" ? "IssueType desc" : "IssueType";
            ViewBag.StatusCodeSortParam = sortOrder == "StatusCode" ? "StatusCode desc" : "StatusCode";
            ViewBag.PriorityCodeSortParam = sortOrder == "PriorityCode" ? "PriorityCode desc" : "PriorityCode";
            ViewBag.SubjectSortParam = sortOrder == "Subject" ? "Subject desc" : "Subject";
            ViewBag.ECSOSupportProductSortParam = sortOrder == "ECSOSupportProduct" ? "ECSOSupportProduct desc" : "ECSOSupportProduct";
            ViewBag.FDOTUserSortParam = sortOrder == "FDOTUser" ? "FDOTUser desc" : "FDOTUser";
            ViewBag.CreateDateSortParam = sortOrder == "CreateDate" ? "CreateDate desc" : "CreateDate";

            // Apply Sorting
            switch (sortOrder)
            {
                case "IssueCode desc":
                    issues = issues.OrderByDescending(i => i.IssueCode);
                    break;
                case "IssueType":
                    issues = issues.OrderBy(i => i.IssueType.Type);
                    break;
                case "IssueType desc":
                    issues = issues.OrderByDescending(i => i.IssueType.Type);
                    break;
                case "StatusCode":
                    issues = issues.OrderBy(i => i.StatusCode.Status);
                    break;
                case "StatusCode desc":
                    issues = issues.OrderByDescending(i => i.StatusCode.Status);
                    break;
                case "PriorityCode":
                    issues = issues.OrderBy(i => i.PriorityCode.Priority);
                    break;
                case "PriorityCode desc":
                    issues = issues.OrderByDescending(i => i.PriorityCode.Priority);
                    break;
                case "Subject":
                    issues = issues.OrderBy(i => i.Subject);
                    break;
                case "Subject desc":
                    issues = issues.OrderByDescending(i => i.Subject);
                    break;
                case "ECSOSupportProduct":
                    issues = issues.OrderBy(i => i.ECSOSupportProduct.SupportProduct);
                    break;
                case "ECSOSupportProduct desc":
                    issues = issues.OrderByDescending(i => i.ECSOSupportProduct.SupportProduct);
                    break;
                case "FDOTUser":
                    issues = issues.OrderBy(i => i.FDOTUser.Name);
                    break;
                case "FDOTUser desc":
                    issues = issues.OrderByDescending(i => i.FDOTUser.Name);
                    break;
                case "CreateDate":
                    issues = issues.OrderBy(i => i.CreateDate);
                    break;
                case "CreateDate desc":
                    issues = issues.OrderByDescending(i => i.CreateDate);
                    break;
                default:
                    issues = issues.OrderBy(i => i.IssueCode);
                    break;
            }
            #endregion

            // Return Page Results, Filtered, Sorted
            return View(issues.ToPagedList(pageNumber, pageSize));
        }

        // POST: /Issues/Edit/5
        [HttpPost]
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult Edit(Issue issue, HttpPostedFileBase uploadFile)
        {
            // If Valid Model
            if (ModelState.IsValid)
            {
                // Set Issue Modified Date
                issue.ModifiedDate = DateTime.Now;

                // Set Issue State to Modified
                db.Entry(issue).State = EntityState.Modified;

                // Save Changes
                db.SaveChanges(); // Need to save to add Issue details to database for creating Issue History

                // Create File Attachment and Add to Database Context
                CreateFileAttachmentRecord(issue.IssueId, uploadFile);

                // Create Issue History and Add to Database Context
                CreateIssueHistoryRecord(issue.IssueId);

                try
                {
                    // Save Changes
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    // Comment out when handled
                    throw;
                }

                // Sends notification message to the [View]
                TempData["Message"] = "Changes to Issue Number: " + issue.IssueCode + " has been made";
                string varTempData = TempData["Message"].ToString();

                // Create Notification Email(Edit) via ActionMail.Net
                ActionMailer.Net.Mvc.EmailResult emailResult = new MailController().EditEmailNotification(issue.IssueId);

                try
                {

                    //DirectoryInfo src = new DirectoryInfo(@"C:\temp\BugLite");
                    //DirectoryInfo dest = new DirectoryInfo(@"C:\inetpub\mailroot\pickup");
                    
                    // Send Notification Email via ActionMail.Net
                    emailResult.Deliver();

                    //CopyDirectory(src, dest);
                }
                catch (Exception e)
                {
                    string error = e.Message;

                    // Logs Exception to a txt file *MGD*
                    //string filePath = @"\\codata\shares\CO\CADD\Users\Marlon\blitelog\BugLiteEmailError.txt";
                    //string filePath = @"C:\buglitelog\BugLiteEmailError.txt";
                    //using (StreamWriter writer = new StreamWriter(filePath, true))
                    //{
                    //    writer.WriteLine("Message :" + e.Message + "<br/>" + Environment.NewLine + "StackTrace :" + e.StackTrace +
                    //       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    //    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    //}
                }

                // Return to Details Page *ABFMGD*
                return Redirect("/Issues/Details/" + issue.IssueId);

                // Uncomment the line below for Deployment to Server due to the difference in Physical Path Location. *ABFMGD*
                //return Redirect("/BugLite/Issues/Details/" + issue.IssueId); 
            }

            // Re-Store Issue SelectLists to Current Values
            ViewBag.WindowsOperatingSystemId = new SelectList(db.OperatingSystems.Where(i => i.Active == true),
                "WindowsOperatingSystemId", "OperatingSystem", issue.WindowsOperatingSystemId);

            ViewBag.CADDSoftwareProductId = new SelectList(db.CADDSoftwareProducts.Where(i => i.Active == true),
                "CADDSoftwareProductId", "CADDSoftware", issue.CADDSoftwareProductId);

            ViewBag.FDOTReleaseVersionId = new SelectList(db.ReleaseVersions.Where(i => i.Active == true),
                "FDOTReleaseVersionId", "ReleaseVersion", issue.FDOTReleaseVersionId);

            ViewBag.IssueTypeId = new SelectList(db.IssueTypes.Where(i => i.Active == true).OrderBy(i => i.Type),
                "IssueTypeId", "Type", issue.IssueTypeId);

            ViewBag.ECSOSupportCategoryId = new SelectList(db.SupportCategories.Where(i => i.Active == true).OrderBy(i => i.SupportCategory),
                "ECSOSupportCategoryId", "SupportCategory", issue.ECSOSupportCategoryId);

            ViewBag.ECSOSupportProductId = new SelectList(db.SupportProducts.Where(i => i.Active == true).OrderBy(i => i.SupportProduct),
                "ECSOSupportProductId", "SupportProduct", issue.ECSOSupportProductId);

            ViewBag.FDOTUserId = new SelectList(db.FDOTUsers.Where(i => i.Active == true).OrderBy(i => i.Name),
                "FDOTUserId", "Name", issue.FDOTUserId);

            ViewBag.PriorityCodeId = new SelectList(db.PriorityCodes.Where(i => i.Active == true),
                "PriorityCodeId", "Priority", issue.PriorityCodeId);

            ViewBag.StatusCodeId = new SelectList(db.StatusCodes.Where(i => i.Active == true),
                "StatusCodeId", "Status", issue.StatusCodeId);

            // Return to Edit Page
            return View(issue);
        }

        // POST: /Issues/Delete/5
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            Issue issue = db.Issues.Find(id);
            // Delete Issue
            db.Issues.Remove(issue);
            db.SaveChanges();
            // Sends notification message to the [View]
            TempData["Message"] = "Issue Number: " + issue.IssueCode + " has been deleted";
            string varTempData = TempData["Message"].ToString();

            return RedirectToAction("Index");
        }

        // POST: /Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Issue issue = db.Issues.Find(id);
            // Delete Issue
            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Issues/DeleteFile
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "User,Admin")]
        public ActionResult DeleteFile(int attachmentId, int id)
        {
            Attachment attachment = db.Attachments.FirstOrDefault(a => a.AttachmentId == attachmentId);
            Issue issue = db.Issues.Find(id);

            if (attachment != null)
            {
                db.Attachments.Remove(attachment);
                db.SaveChanges();
                //return RedirectToAction("Details" + "/" + id);
                
                // Return to Details Page *ABFMGD*
                return Redirect("/Issues/Details/" + issue.IssueId);

                // Uncomment the line below for Deployment to Server due to the difference in Physical Path Location. *ABFMGD*
                //return Redirect("/BugLite/Issues/Details/" + issue.IssueId);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Custom Class Methods

        // Creates a File Attachment
        private void CreateFileAttachmentRecord(int issueId, HttpPostedFileBase uploadFile)
        {
            // If Attachment file found
            if (uploadFile != null)
            {
                // Create new Attachment object
                Attachment attachment = new Attachment();
                attachment.IssueId = issueId;
                attachment.ContentType = uploadFile.ContentType;
                attachment.FileName = new FileInfo(uploadFile.FileName).Name;
                attachment.FileData = new byte[uploadFile.ContentLength];

                // Read file data into byte array (byte[])
                uploadFile.InputStream.Read(attachment.FileData, 0, uploadFile.ContentLength);

                // Add Attachment to Database Context
                db.Attachments.Add(attachment);
            }
        }

        // Creates the Issue History
        private void CreateIssueHistoryRecord(int issueId)
        {
            // Get Issue Detail
            Issue issue = db.Issues
                .Include(i => i.CADDSoftwareProduct)
                .Include(i => i.FDOTReleaseVersion)
                .Include(i => i.ECSOSupportCategory)
                .Include(i => i.ECSOSupportProduct)
                .Include(i => i.PriorityCode)
                .Include(i => i.StatusCode)
                .Single(i => i.IssueId == issueId);

            // Create new History object
            IssueHistory history = new IssueHistory();
            history.IssueId = issue.IssueId;
            history.IssueCode = issue.IssueCode;

            // Populate History Fields
            history.ModifiedDate = DateTime.Now;        // Modified Date
            history.FDOTUserId = issue.FDOTUserId;      // Modified By
            history.Status = issue.StatusCode.Status;   // Issue Status
            //history.Notes = history.Notes;   // Notes

            // If Comment is Not Empty or NULL
            if (!string.IsNullOrEmpty(Request.Form["HiddenComment"]))
            {
                //history.Comment = Request.Form["HiddenComment"].ToString().Replace(",",""); 
                //history.Comment = Request.Form["HiddenComment"].ToString(); //Request.Form["HiddenComment"].ToString(); 
                
                // NOTE: For some odd reason the comment text has a preceding "," character, hence i removed it with the Replace() function 
                history.Comment = Convert.ToString(Request.Form["HiddenComment"]).Replace(",", ""); // Reason for Change    
            }

            // Add History to Database Context
            db.IssueHistories.Add(history);
        }

        // Copy Directory method to handle copying files from source to destination - DUH!
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destination.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        #endregion

        // Required Default Object Dispose Method
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}