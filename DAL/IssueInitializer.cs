using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bug_Lite.Models
{
    public class IssueInitializer : DropCreateDatabaseAlways<IssueContext>
    {

        protected override void Seed(IssueContext context)
        {
            try
            {
                #region "User Sections"
                // Add Seed User Sections
                var userSections = new List<UserSection> {   
                    new UserSection { UserSectionId = 1, Section = "Administration", Active = true},     
                    new UserSection { UserSectionId = 2, Section = "Development", Active = true},     
                    new UserSection { UserSectionId = 3, Section = "Management", Active = true},     
                    new UserSection { UserSectionId = 4, Section = "Support", Active = true},     
                };

                userSections.ForEach(d => context.UserSections.Add(d));
                context.SaveChanges();
                #endregion

                #region "User Roles"
                // Add Seed User Roles
                var userRoles = new List<UserRole> {   
                    new UserRole { UserRoleId = 1, Role = "Admin", Active = true},     
                    new UserRole { UserRoleId = 2, Role = "Guest", Active = true},     
                    new UserRole { UserRoleId = 3, Role = "Power User",  Active = true},     
                    new UserRole { UserRoleId = 4, Role = "User",  Active = true},     
                };

                userRoles.ForEach(d => context.UserRoles.Add(d));
                context.SaveChanges();
                #endregion

                #region "FDOT Users"
                var fdotUsers = new List<FDOTUser> {
                    new FDOTUser { FDOTUserId = 1,  Name = "Andrew Fuller",     Username = "rd964af",   Password = "password", PhoneNumber = "1-800-245-1603", EmailAddress = "andrew.fuller@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
                    new FDOTUser { FDOTUserId = 2,  Name = "Bob Thoburn",       Username = "rd964rt",   Password = "password", PhoneNumber = "1-800-245-1610", EmailAddress = "robert.thorburn@dot.state.fl.us",    UserSectionId = 4, UserRoleId = 4, Active = false},
                    new FDOTUser { FDOTUserId = 3,  Name = "Brad Eiler",        Username = "rd964be",   Password = "password", PhoneNumber = "1-800-245-1625", EmailAddress = "brad.eiler@dot.state.fl.us",         UserSectionId = 4, UserRoleId = 4, Active = false},
                    new FDOTUser { FDOTUserId = 4,  Name = "Bruce Dana",        Username = "rd964bd",   Password = "password", PhoneNumber = "1-800-245-1606", EmailAddress = "bruce.dana@dot.state.fl.us",         UserSectionId = 3, UserRoleId = 1, Active = false},
                    new FDOTUser { FDOTUserId = 5,  Name = "Christiana Holmes", Username = "rd964ch",   Password = "password", PhoneNumber = "1-800-245-1629", EmailAddress = "christiana.holmes@dot.state.fl.us",  UserSectionId = 4, UserRoleId = 4, Active = false},
                    new FDOTUser { FDOTUserId = 6,  Name = "Denise Broom",      Username = "rd964db",   Password = "password", PhoneNumber = "1-800-245-1607", EmailAddress = "denise.broom@dot.state.fl.us",       UserSectionId = 4, UserRoleId = 4, Active = false},
                    new FDOTUser { FDOTUserId = 7,  Name = "James Worley",      Username = "rd964jw",   Password = "password", PhoneNumber = "1-800-245-1614", EmailAddress = "james.worley@dot.state.fl.us",       UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 8,  Name = "Jason Roberts",     Username = "rd964jr",   Password = "password", PhoneNumber = "1-800-245-1609", EmailAddress = "jason.roberts@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
                    new FDOTUser { FDOTUserId = 9,  Name = "Jimmie Prow",       Username = "rd964jp",   Password = "password", PhoneNumber = "1-800-245-1626", EmailAddress = "jimmie.prow@dot.state.fl.us",        UserSectionId = 4, UserRoleId = 1, Active = false},
                    new FDOTUser { FDOTUserId = 10, Name = "Marlon Dunn",       Username = "knasdmd",   Password = "password", PhoneNumber = "1-800-245-1615", EmailAddress = "marlon.dunn@dot.state.fl.us",        UserSectionId = 2, UserRoleId = 1, Active = true},
                    new FDOTUser { FDOTUserId = 11, Name = "Scott Dixon",       Username = "knasdsd",   Password = "password", PhoneNumber = "1-800-245-1620", EmailAddress = "scott.dixon@dot.state.fl.us",        UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 12, Name = "Michael Robertson", Username = "rd964mr",   Password = "password", PhoneNumber = "1-800-245-1611", EmailAddress = "michael.robertson@dot.state.fl.us",  UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 13, Name = "Michele Schiele",   Username = "rd964ms",   Password = "password", PhoneNumber = "1-800-245-1628", EmailAddress = "michele.schiele@dot.state.fl.us",    UserSectionId = 4, UserRoleId = 4, Active = false},
                    new FDOTUser { FDOTUserId = 14, Name = "Mike Racca",        Username = "rd964mr",   Password = "password", PhoneNumber = "1-800-245-1621", EmailAddress = "mike.racca@dot.state.fl.us",         UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 15, Name = "Quinton Tillman",   Username = "rd964qt",   Password = "password", PhoneNumber = "1-800-245-1630", EmailAddress = "quinton.tillman@dot.state.fl.us",    UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 16, Name = "Randy Roberts",     Username = "rd964rr",   Password = "password", PhoneNumber = "1-800-245-1651", EmailAddress = "randy.roberts@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 4, Active = true},
                    new FDOTUser { FDOTUserId = 17, Name = "Ray Lamoreaux",     Username = "rd964rl",   Password = "password", PhoneNumber = "1-800-245-1613", EmailAddress = "ray.lamoreuax@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
                    new FDOTUser { FDOTUserId = 18, Name = "Vern Danforth",     Username = "rd964vd",   Password = "password", PhoneNumber = "1-800-245-1618", EmailAddress = "vern.danforth@dot.state.fl.us",      UserSectionId = 4, UserRoleId = 4, Active = false},
                };

                fdotUsers.ForEach(d => context.FDOTUsers.Add(d));
                context.SaveChanges();
                #endregion

                #region "CADD Software Products"
                var caddSoftwareProducts = new List<CADDSoftwareProduct> {   
                    new CADDSoftwareProduct { CADDSoftwareProductId = 1, CADDSoftware = "AutoCAD Civil 3D 2014", Active = true},
                    new CADDSoftwareProduct { CADDSoftwareProductId = 2, CADDSoftware = "AutoCAD Civil 3D 2015", Active = true},
                    new CADDSoftwareProduct { CADDSoftwareProductId = 3, CADDSoftware = "AutoCAD Civil 3D 2016", Active = true},
                    new CADDSoftwareProduct { CADDSoftwareProductId = 4, CADDSoftware = "Microstation", Active = true},
                };

                caddSoftwareProducts.ForEach(d => context.CADDSoftwareProducts.Add(d));
                context.SaveChanges();
                #endregion

                #region "FDOT Software Releases"
                var fdotReleaseVersions = new List<FDOTReleaseVersion> {   
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 1, CADDSoftwareProductId = 4, ReleaseVersion = "FDOTSS2", Active = false},     
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 2, CADDSoftwareProductId = 4, ReleaseVersion = "FDOTSS3", Active = false},
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 3, CADDSoftwareProductId = 4, ReleaseVersion = "FDOTSS4", Active = false},
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 4, CADDSoftwareProductId = 4, ReleaseVersion = "FDOT2010-MR3", Active = false},     
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 5, CADDSoftwareProductId = 4, ReleaseVersion = "FDOT2010-MR4", Active = false},     
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 6, CADDSoftwareProductId = 1, ReleaseVersion = "FDOT2014.C3D", Active = true},
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 7, CADDSoftwareProductId = 2, ReleaseVersion = "FDOT2015.C3D", Active = true},
                    new FDOTReleaseVersion { FDOTReleaseVersionId = 8, CADDSoftwareProductId = 3, ReleaseVersion = "FDOT2016.C3D", Active = true},
                };

                fdotReleaseVersions.ForEach(d => context.ReleaseVersions.Add(d));
                context.SaveChanges();
                #endregion

                #region "ESCO Support Categories"
                var ecsoSupportCategories = new List<ECSOSupportCategory> {   
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 1,  SupportCategory = "AutoCAD Civil 3D Add-Ins", Active = false},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 2,  SupportCategory = "AutoCAD Civil 3D Subassemblies", Active = true},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 3,  SupportCategory = "AutoCAD Civil 3D Applications", Active = true},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 4,  SupportCategory = "ECSO Software Applications", Active = true},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 5,  SupportCategory = "ECSO Web", Active = true},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 6,  SupportCategory = "Digital Delivery", Active = true},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 7,  SupportCategory = "Electronic Delivery", Active = false},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 8,  SupportCategory = "MicroStation Add-Ins", Active = false},
                    //new ECSOSupportCategory { ECSOSupportCategoryId = 9,  SupportCategory = "Other", Active = true},

                    new ECSOSupportCategory { ECSOSupportCategoryId = 1,  SupportCategory = "AutoCAD Civil 3D Add-Ins", Active = false},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 2,  SupportCategory = "AutoCAD Civil 3D Subassemblies", Active = true},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 3,  SupportCategory = "ECSO Software Applications", Active = true},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 4,  SupportCategory = "ECSO Web", Active = true},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 5,  SupportCategory = "Electronic Delivery", Active = false},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 6,  SupportCategory = "MicroStation Add-Ins", Active = false},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 7,  SupportCategory = "Other", Active = true},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 8,  SupportCategory = "AutoCAD Civil 3D Applications", Active = true},
                    new ECSOSupportCategory { ECSOSupportCategoryId = 9,  SupportCategory = "Digital Delivery", Active = true},
                   
                };

                ecsoSupportCategories.ForEach(d => context.SupportCategories.Add(d));
                context.SaveChanges();
                #endregion

                #region "ECSO Support Products"
                var ecsoSupportProducts = new List<ECSOSupportProduct> {
                    new ECSOSupportProduct { ECSOSupportProductId = 1,   ECSOSupportCategoryId = 6,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "Bug-Lite", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 2,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "Create Project", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 3,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 18, SecondarySupportId = 18, SupportProduct = "Create Edit Tool", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 4,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 3,  SecondarySupportId = 7,  SupportProduct = "Electronic Delivery Indexer", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 5,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 3,  SecondarySupportId = 7,  SupportProduct = "File Checker", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 6,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "PEDDS Client", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 7,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "PEDDS Database Manager", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 8,   ECSOSupportCategoryId = 4,  DeveloperId = 13, PrimarySupportId = 13, SecondarySupportId = 13, SupportProduct = "Project Delta", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 9,   ECSOSupportCategoryId = 4,  DeveloperId = 8,  PrimarySupportId = 8,  SecondarySupportId = 8,  SupportProduct = "Q-Sheet", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 10,  ECSOSupportCategoryId = 4,  DeveloperId = 13, PrimarySupportId = 13, SecondarySupportId = 13, SupportProduct = "Set Maker", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 11,  ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 11, SecondarySupportId = 11, SupportProduct = "SheetSet Organizer", Active = true},
                    //new ECSOSupportProduct { ECSOSupportProductId = 12,  ECSOSupportCategoryId = 4,  DeveloperId = 8,  PrimarySupportId = 8,  SecondarySupportId = 8,  SupportProduct = "Strung Project", Active = true},
                };

                ecsoSupportProducts.ForEach(d => context.SupportProducts.Add(d));
                context.SaveChanges();
                #endregion

                #region "Issue Types"
                // Add Seed Issue Types - Software Bug, CADD support, User Question, Enhancement Request
                var issueTypes = new List<IssueType> {   
                    new IssueType { IssueTypeId = 1, Type = "CADD Support", Active = true},     
                    new IssueType { IssueTypeId = 2, Type = "Enhancement Request", Active = true},     
                    new IssueType { IssueTypeId = 3, Type = "Modification Request", Active = true},     
                    new IssueType { IssueTypeId = 4, Type = "Software Bug", Active = true},     
                    new IssueType { IssueTypeId = 5, Type = "User Question", Active = true},     
                };

                issueTypes.ForEach(d => context.IssueTypes.Add(d));
                context.SaveChanges();
                #endregion

                #region "Priority Codes"
                // Add Seed Priority Codes - Critical, High, Medium, Low
                var priorityCodes = new List<PriorityCode> {   
                    new PriorityCode { PriorityCodeId = 1, Priority = "Critical", Active = true},     
                    new PriorityCode { PriorityCodeId = 2, Priority = "High", Active = true},     
                    new PriorityCode { PriorityCodeId = 3, Priority = "Medium", Active = true},     
                    new PriorityCode { PriorityCodeId = 4, Priority = "Low", Active = true},     
                };

                priorityCodes.ForEach(d => context.PriorityCodes.Add(d));
                context.SaveChanges();
                #endregion

                #region "Status Codes"
                // Add Seed Status Codes - New, InProgress, Resolved, Closed
                var statusCodes = new List<StatusCode> {  
                    new StatusCode { StatusCodeId = 1,  Status = "New", Active = true},     
                    new StatusCode { StatusCodeId = 2,  Status = "InProgress", Active = true},     
                    new StatusCode { StatusCodeId = 3,  Status = "Resloved", Active = true},     
                    new StatusCode { StatusCodeId = 4,  Status = "Closed", Active = true},     
                   
                    //new StatusCode { StatusCodeId = 1,  Status = "Active", Active = true},     
                    //new StatusCode { StatusCodeId = 2,  Status = "Active(In-Testing)", Active = true},     
                    //new StatusCode { StatusCodeId = 3,  Status = "Closed", Active = true},     
                    //new StatusCode { StatusCodeId = 4,  Status = "Closed(Duplicate)", Active = true},     
                    //new StatusCode { StatusCodeId = 5,  Status = "Closed(Fixed)", Active = true},     
                    //new StatusCode { StatusCodeId = 6,  Status = "Closed(Rejected)", Active = true},     
                    //new StatusCode { StatusCodeId = 7,  Status = "Resolved", Active = true},     
                    //new StatusCode { StatusCodeId = 8,  Status = "Resolved(By Design)", Active = true},     
                    //new StatusCode { StatusCodeId = 9,  Status = "Resolved(Fixed)", Active = true},     
                    //new StatusCode { StatusCodeId = 10, Status = "Resolved(Rejected)", Active = true},     
                    //new StatusCode { StatusCodeId = 11, Status = "Resolved(Responded)", Active = true},     
                };

                statusCodes.ForEach(d => context.StatusCodes.Add(d));
                context.SaveChanges();
                #endregion

                #region "Windows Operating Systems"
                var windowsOperatingSystems = new List<WindowsOperatingSystem> {   
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 1, OperatingSystem = "Windows2000", Active = false},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 2, OperatingSystem = "Windows7 (32-bit)", Active = true},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 3, OperatingSystem = "Windows7 (64-bit)", Active = true},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 4, OperatingSystem = "Windows8 (32-bit)", Active = true},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 5, OperatingSystem = "Windows8 (64-bit)", Active = true},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 6, OperatingSystem = "Windows Vista", Active = true},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 7, OperatingSystem = "WindowsXP (32-bit_", Active = false},     
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 8, OperatingSystem = "WindowsXP (64-bit)", Active = false},
                    new WindowsOperatingSystem { WindowsOperatingSystemId = 8, OperatingSystem = "Windows10", Active = true},
                };

                windowsOperatingSystems.ForEach(d => context.OperatingSystems.Add(d));
                context.SaveChanges();
                #endregion

                #region "Sample Issue Data"
                //// Add Seed Issues
                //var issues = new List<Issue> {   
                //    new Issue { IssueId = 1, IssueCode = "000001", FPID = "010101-1-01-01", Subject = "Test Issue #1",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1, 
                //        Details = "Test Issue #1 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 2, IssueCode = "000002", FPID = "020202-2-02-02", Subject = "Test Issue #2", 
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #2 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 3, IssueCode = "000003", FPID = "030303-3-03-03", Subject = "Test Issue #3",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #3 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 4, IssueCode = "000004", FPID = "040404-4-04-04", Subject = "Test Issue #4",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #4 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 5, IssueCode = "000005", FPID = "050505-5-05-05", Subject = "Test Issue #5",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #5 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 6, IssueCode = "000006", FPID = "060606-6-06-06", Subject = "Test Issue #6",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #6 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 7, IssueCode = "000007", FPID = "070707-7-07-07", Subject = "Test Issue #7",
                //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #7 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 8, IssueCode = "000008", FPID = "080808-8-08-08", Subject = "Test Issue #8",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #8 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 9, IssueCode = "000009", FPID = "090909-9-09-09", Subject = "Test Issue #9",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #9 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 10, IssueCode = "000010", FPID = "101010-1-10-10", Subject = "Test Issue #10",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #10 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 11, IssueCode = "000011", FPID = "111111-1-11-11", Subject = "Test Issue #11",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #11 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 12, IssueCode = "000012", FPID = "121212-1-12-12", Subject = "Test Issue #12",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #12 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 13, IssueCode = "000013", FPID = "131313-1-13-13", Subject = "Test Issue #13",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #13 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //    new Issue { IssueId = 14, IssueCode = "000014", FPID = "141414-1-14-14", Subject = "Test Issue #14",
                //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
                //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
                //        Details = "Test Issue #14 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
                //};

                //issues.ForEach(d => context.Issues.Add(d));
                //context.SaveChanges();

                // Add Seed Issue History
                //var issueHistories = new List<IssueHistory> {
                //    new IssueHistory { IssueHistoryId = 1, IssueId = 1, FDOTUserId = 1, IssueCode = "000001",
                //                        Status = "New", Notes = "Changed Issue Priority to High", ModifiedDate = DateTime.Now}, 
                //    new IssueHistory { IssueHistoryId = 2, IssueId = 1, FDOTUserId = 1, IssueCode = "000001",
                //                       Status = "Closed", Notes = "Changed Issue Status to Closed", ModifiedDate = DateTime.Now}, 
                //};

                //issueHistories.ForEach(d => context.IssueHistories.Add(d));
                //context.SaveChanges();
                #endregion
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }




        //protected override void Seed(IssueContext context)
        //{
        //    try
        //    {
        //        #region "User Sections"
        //        // Add Seed User Sections
        //        var userSections = new List<UserSection> {   
        //            new UserSection { UserSectionId = 1, Section = "Administration", Active = true},     
        //            new UserSection { UserSectionId = 2, Section = "Development", Active = true},     
        //            new UserSection { UserSectionId = 3, Section = "Management", Active = true},     
        //            new UserSection { UserSectionId = 4, Section = "Support", Active = true},     
        //        };

        //        userSections.ForEach(d => context.UserSections.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "User Roles"
        //        // Add Seed User Roles
        //        var userRoles = new List<UserRole> {   
        //            new UserRole { UserRoleId = 1, Role = "Admin", Active = true},     
        //            new UserRole { UserRoleId = 2, Role = "Guest", Active = true},     
        //            new UserRole { UserRoleId = 3, Role = "Power User",  Active = true},     
        //            new UserRole { UserRoleId = 4, Role = "User",  Active = true},     
        //        };

        //        userRoles.ForEach(d => context.UserRoles.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "FDOT Users"
        //        var fdotUsers = new List<FDOTUser> {
        //            new FDOTUser { FDOTUserId = 1,  Name = "Andrew Fuller",     Username = "rd964af",   Password = "password", PhoneNumber = "1-800-245-1603", EmailAddress = "andrew.fuller@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 2,  Name = "Bob Thoburn",       Username = "rd964rt",   Password = "password", PhoneNumber = "1-800-245-1610", EmailAddress = "robert.thorburn@dot.state.fl.us",    UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 3,  Name = "Brad Eiler",        Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1625", EmailAddress = "brad.eiler@dot.state.fl.us",         UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 4,  Name = "Bruce Dana",        Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1606", EmailAddress = "bruce.dana@dot.state.fl.us",         UserSectionId = 3, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 5,  Name = "Christiana Holmes", Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1629", EmailAddress = "christiana.holmes@dot.state.fl.us",  UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 6,  Name = "Denise Broom",      Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1607", EmailAddress = "denise.broom@dot.state.fl.us",       UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 7,  Name = "James Worley",      Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1614", EmailAddress = "james.worley@dot.state.fl.us",       UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 8,  Name = "Jason Roberts",     Username = "rd964jr",   Password = "password", PhoneNumber = "1-800-245-1609", EmailAddress = "jason.roberts@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 9,  Name = "Jennifer Weeks",    Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1616", EmailAddress = "jennifer.weeks@dot.state.fl.us",     UserSectionId = 1, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 10, Name = "Jimmie Prow",       Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1626", EmailAddress = "jimmie.prow@dot.state.fl.us",        UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 11, Name = "Marlon Dunn",       Username = "KNASDMD",   Password = "password", PhoneNumber = "1-800-245-1615", EmailAddress = "marlon.dunn@dot.state.fl.us",        UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 12, Name = "Mary Peterman",     Username = "rd964mp",   Password = "password", PhoneNumber = "1-800-245-1623", EmailAddress = "mary.peterman@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 13, Name = "Michael Robertson", Username = "rd964mr",   Password = "password", PhoneNumber = "1-800-245-1611", EmailAddress = "michael.robertson@dot.state.fl.us",  UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 14, Name = "Michele Schiele",   Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1628", EmailAddress = "michele.schiele@dot.state.fl.us",    UserSectionId = 4, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 15, Name = "Mike Racca",        Username = "rd964mr",   Password = "password", PhoneNumber = "1-800-245-1621", EmailAddress = "mike.racca@dot.state.fl.us",         UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 16, Name = "Quinton Tillman",   Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1630", EmailAddress = "quinton.tillman@dot.state.fl.us",    UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 17, Name = "Randy Roberts",     Username = "rd964rr",   Password = "password", PhoneNumber = "1-800-245-1651", EmailAddress = "randy.roberts@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 18, Name = "Ray Lamoreaux",     Username = "rd964rl",   Password = "password", PhoneNumber = "1-800-245-1613", EmailAddress = "ray.lamoreuax@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 19, Name = "Shannon Miles",     Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1620", EmailAddress = "shannon.miles@dot.state.fl.us",      UserSectionId = 2, UserRoleId = 1, Active = true},
        //            new FDOTUser { FDOTUserId = 20, Name = "Susan Laird",       Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1608", EmailAddress = "susan.laird@dot.state.fl.us",        UserSectionId = 1, UserRoleId = 1, Active = false},
        //            new FDOTUser { FDOTUserId = 21, Name = "Vern Danforth",     Username = "loginName", Password = "password", PhoneNumber = "1-800-245-1618", EmailAddress = "vern.danforth@dot.state.fl.us",      UserSectionId = 4, UserRoleId = 1, Active = false},
        //        };

        //        fdotUsers.ForEach(d => context.FDOTUsers.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "CADD Software Products"
        //        var caddSoftwareProducts = new List<CADDSoftwareProduct> {   
        //            new CADDSoftwareProduct { CADDSoftwareProductId = 1, CADDSoftware = "AutoCAD Civil 3D 2012", Active = true},
        //            new CADDSoftwareProduct { CADDSoftwareProductId = 2, CADDSoftware = "AutoCAD Civil 3D 2013", Active = true},
        //            new CADDSoftwareProduct { CADDSoftwareProductId = 3, CADDSoftware = "AutoCAD Civil 3D 2014", Active = true},
        //            new CADDSoftwareProduct { CADDSoftwareProductId = 4, CADDSoftware = "Microstation", Active = true},
        //        };

        //        caddSoftwareProducts.ForEach(d => context.CADDSoftwareProducts.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "FDOT Software Releases"
        //        var fdotReleaseVersions = new List<FDOTReleaseVersion> {   
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 1, CADDSoftwareProductId = 2, ReleaseVersion = "FDOTSS2", Active = true},     
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 2, CADDSoftwareProductId = 2, ReleaseVersion = "FDOTSS3", Active = true},     
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 3, CADDSoftwareProductId = 2, ReleaseVersion = "FDOT2010-MR3", Active = true},     
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 4, CADDSoftwareProductId = 2, ReleaseVersion = "FDOT2010-MR4", Active = true},     
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 5, CADDSoftwareProductId = 1, ReleaseVersion = "FDOT2012.C3D", Active = true},     
        //            new FDOTReleaseVersion { FDOTReleaseVersionId = 6, CADDSoftwareProductId = 1, ReleaseVersion = "FDOT2014.C3D", Active = true},     
        //        };

        //        fdotReleaseVersions.ForEach(d => context.ReleaseVersions.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "ESCO Support Categories"
        //        var ecsoSupportCategories = new List<ECSOSupportCategory> {   
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 1,  SupportCategory = "Civil 3D Add-Ins", Active = true},
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 2,  SupportCategory = "ECSO Software Applications", Active = true},
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 3,  SupportCategory = "ECSO Web", Active = true},
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 4,  SupportCategory = "Electronic Delivery", Active = true},
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 5,  SupportCategory = "MicroStation Add-Ins", Active = true},
        //            new ECSOSupportCategory { ECSOSupportCategoryId = 6,  SupportCategory = "Other", Active = true},

        //            #region "Sharepoint Categories List"
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 1, CADDSoftwareProductId = 1, SupportCategory = "AutoCAD", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 2, CADDSoftwareProductId = 1, SupportCategory = "CADD Production", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 3, CADDSoftwareProductId = 1, SupportCategory = "CAiCE", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 4, CADDSoftwareProductId = 1, SupportCategory = "Construction", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 5, CADDSoftwareProductId = 1, SupportCategory = "Software Development", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 6, CADDSoftwareProductId = 1, SupportCategory = "ECSO Web", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 7, CADDSoftwareProductId = 1, SupportCategory = "Electronic Delivery", Active = true},
        //            //new ECSOSupportCategory { ECSOSupportCategoryId = 8, CADDSoftwareProductId = 2, SupportCategory = "GEOPAK", Active = true},
        //            #endregion
        //        };

        //        ecsoSupportCategories.ForEach(d => context.SupportCategories.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "ECSO Support Products"
        //        var ecsoSupportProducts = new List<ECSOSupportProduct> {
        //            new ECSOSupportProduct { ECSOSupportProductId = 1,   ECSOSupportCategoryId = 6,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "Bug-Lite", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 2,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "Create Project", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 3,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 18, SecondarySupportId = 18, SupportProduct = "Create Edit Tool", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 4,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 3,  SecondarySupportId = 7,  SupportProduct = "Electronic Delivery Indexer", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 5,   ECSOSupportCategoryId = 4,  DeveloperId = 18, PrimarySupportId = 3,  SecondarySupportId = 7,  SupportProduct = "File Checker", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 6,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "PEDDS Client", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 7,   ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 7,  SecondarySupportId = 16, SupportProduct = "PEDDS Database Manager", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 8,   ECSOSupportCategoryId = 4,  DeveloperId = 13, PrimarySupportId = 13, SecondarySupportId = 13, SupportProduct = "Project Delta", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 9,   ECSOSupportCategoryId = 4,  DeveloperId = 8,  PrimarySupportId = 8,  SecondarySupportId = 8,  SupportProduct = "Q-Sheet", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 10,  ECSOSupportCategoryId = 4,  DeveloperId = 13, PrimarySupportId = 13, SecondarySupportId = 13, SupportProduct = "Set Maker", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 11,  ECSOSupportCategoryId = 4,  DeveloperId = 1,  PrimarySupportId = 11, SecondarySupportId = 11, SupportProduct = "SheetSet Organizer", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 12,  ECSOSupportCategoryId = 4,  DeveloperId = 8,  PrimarySupportId = 8,  SecondarySupportId = 8,  SupportProduct = "Strung Project", Active = true},

        //            #region "Sharepoint Products List"
        //            //// AutoCAD
        //            //new ECSOSupportProduct { ECSOSupportProductId = 1,   ECSOSupportCategoryId = 1, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Civil 3D", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 2,   ECSOSupportCategoryId = 1, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Civil 3D (R/W and Survey)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 3,   ECSOSupportCategoryId = 1, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Architectural", Active = true},

        //            //// CADD Production
        //            //new ECSOSupportProduct { ECSOSupportProductId = 4,  ECSOSupportCategoryId = 2, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "CADD Manual", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 5,  ECSOSupportCategoryId = 2, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Criteria Handbook (CPCH)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 6,  ECSOSupportCategoryId = 2, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Criteria Handbook (CPCH) CH11 Survey", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 7,  ECSOSupportCategoryId = 2, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Criteria Handbook (CPCH) CH9 R/W", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 8,  ECSOSupportCategoryId = 2, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Criteria Handbook (CPCH) CH18 Structures", Active = true},
                    
        //            //// CAiCE
        //            //new ECSOSupportProduct { ECSOSupportProductId = 9,   ECSOSupportCategoryId = 3, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "R/W and Survey", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 10,  ECSOSupportCategoryId = 3, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "CDG to V8 Convertion", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 11,  ECSOSupportCategoryId = 3, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Feature Tables", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 12,  ECSOSupportCategoryId = 3, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "LIS Files", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 13,  ECSOSupportCategoryId = 3, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Theme Viewer", Active = true},

        //            //// Construction
        //            //new ECSOSupportProduct { ECSOSupportProductId = 14,  ECSOSupportCategoryId = 4, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Density Log Book Plots", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 15,  ECSOSupportCategoryId = 4, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Multi-Line Earthwork", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 16,  ECSOSupportCategoryId = 4, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Quantities", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 17,  ECSOSupportCategoryId = 4, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Software (Csi)", Active = true},

        //            //// Software Development
        //            //new ECSOSupportProduct { ECSOSupportProductId = 18, ECSOSupportCategoryId = 5, DeveloperId = 1, PrimarySupportId = 17, SecondarySupportId = 17,  SupportProduct = "Bug-Lite", Active = true},

        //            //// ECSO Web
        //            //new ECSOSupportProduct { ECSOSupportProductId = 19,  ECSOSupportCategoryId = 6, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Internet Website", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 20,  ECSOSupportCategoryId = 6, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Intranet Website", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 21,  ECSOSupportCategoryId = 6, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Project Information Tracking System (PITS)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 22,  ECSOSupportCategoryId = 6, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "SharePoint Site", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 23,  ECSOSupportCategoryId = 6, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Web Site", Active = true},

        //            //// Electronic Delivery
        //            //new ECSOSupportProduct { ECSOSupportProductId = 24, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Create FDOT Project", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 25, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "EDI2", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 26, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "File Checker", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 27, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Project Browser", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 28, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Project Delta", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 29, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Q Sheet", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 30, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Set Maker", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 31, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Strung Project", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 32, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Development", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 33, ECSOSupportCategoryId = 7, DeveloperId = 1,    PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Process", Active = true},

        //            //// GEOPACK
        //            //new ECSOSupportProduct { ECSOSupportProductId = 34, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Criteria Files", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 35, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "D&C Manager", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 36, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "D&C Manager (R/W)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 37, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Drainage", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 38, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Geotechnical", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 39, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Quantity Manager", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 40, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Road", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 41, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Road (R/W)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 42, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Road (Multi-Line/Gen Files)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 43, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Sheet Clipping", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 44, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Site", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 45, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "SMD Features Database", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 46, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Survey", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 47, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9, SupportProduct = "Survey (R/W)", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 48, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "Water/Sewer", Active = true},
        //            //new ECSOSupportProduct { ECSOSupportProductId = 49, ECSOSupportCategoryId = 8, DeveloperId = null, PrimarySupportId = 9, SecondarySupportId = 9,  SupportProduct = "X-Sections", Active = true},
        //            #endregion
        //        };

        //        ecsoSupportProducts.ForEach(d => context.SupportProducts.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "Issue Types"
        //        // Add Seed Issue Types - Software Bug, CADD support, User Question, Enhancement Request
        //        var issueTypes = new List<IssueType> {   
        //            new IssueType { IssueTypeId = 1, Type = "CADD Support", Active = true},     
        //            new IssueType { IssueTypeId = 2, Type = "Enhancement Request", Active = true},     
        //            new IssueType { IssueTypeId = 3, Type = "Modification Request", Active = true},     
        //            new IssueType { IssueTypeId = 4, Type = "Software Bug", Active = true},     
        //            new IssueType { IssueTypeId = 5, Type = "User Question", Active = true},     
        //        };

        //        issueTypes.ForEach(d => context.IssueTypes.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "Priority Codes"
        //        // Add Seed Priority Codes - Critical, High, Medium, Low
        //        var priorityCodes = new List<PriorityCode> {   
        //            new PriorityCode { PriorityCodeId = 1, Priority = "Critical", Active = true},     
        //            new PriorityCode { PriorityCodeId = 2, Priority = "High", Active = true},     
        //            new PriorityCode { PriorityCodeId = 3, Priority = "Medium", Active = true},     
        //            new PriorityCode { PriorityCodeId = 4, Priority = "Low", Active = true},     
        //        };

        //        priorityCodes.ForEach(d => context.PriorityCodes.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "Status Codes"
        //        // Add Seed Status Codes - Open, Rejected, Failed Testing, Complete, Closed
        //        var statusCodes = new List<StatusCode> {   
        //            new StatusCode { StatusCodeId = 1,  Status = "Active", Active = true},     
        //            new StatusCode { StatusCodeId = 2,  Status = "Active(In-Testing)", Active = true},     
        //            new StatusCode { StatusCodeId = 3,  Status = "Closed", Active = true},     
        //            new StatusCode { StatusCodeId = 4,  Status = "Closed(Duplicate)", Active = true},     
        //            new StatusCode { StatusCodeId = 5,  Status = "Closed(Fixed)", Active = true},     
        //            new StatusCode { StatusCodeId = 6,  Status = "Closed(Rejected)", Active = true},     
        //            new StatusCode { StatusCodeId = 7,  Status = "Resolved", Active = true},     
        //            new StatusCode { StatusCodeId = 8,  Status = "Resolved(By Design)", Active = true},     
        //            new StatusCode { StatusCodeId = 9,  Status = "Resolved(Fixed)", Active = true},     
        //            new StatusCode { StatusCodeId = 10, Status = "Resolved(Rejected)", Active = true},     
        //            new StatusCode { StatusCodeId = 11, Status = "Resolved(Responded)", Active = true},     
        //        };

        //        statusCodes.ForEach(d => context.StatusCodes.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "Windows Operating Systems"
        //        var windowsOperatingSystems = new List<WindowsOperatingSystem> {   
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 1, OperatingSystem = "Windows2000", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 2, OperatingSystem = "Windows7 (32-bit)", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 3, OperatingSystem = "Windows7 (64-bit)", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 4, OperatingSystem = "Windows8 (32-bit)", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 5, OperatingSystem = "Windows8 (64-bit)", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 6, OperatingSystem = "Windows Vista", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 7, OperatingSystem = "WindowsXP (32-bit_", Active = true},     
        //            new WindowsOperatingSystem { WindowsOperatingSystemId = 8, OperatingSystem = "WindowsXP (64-bit)", Active = true},     
        //        };

        //        windowsOperatingSystems.ForEach(d => context.OperatingSystems.Add(d));
        //        context.SaveChanges();
        //        #endregion

        //        #region "Sample Issue Data"
        //        //// Add Seed Issues
        //        //var issues = new List<Issue> {   
        //        //    new Issue { IssueId = 1, IssueCode = "000001", FPID = "010101-1-01-01", Subject = "Test Issue #1",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1, 
        //        //        Details = "Test Issue #1 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 2, IssueCode = "000002", FPID = "020202-2-02-02", Subject = "Test Issue #2", 
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #2 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 3, IssueCode = "000003", FPID = "030303-3-03-03", Subject = "Test Issue #3",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #3 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 4, IssueCode = "000004", FPID = "040404-4-04-04", Subject = "Test Issue #4",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #4 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 5, IssueCode = "000005", FPID = "050505-5-05-05", Subject = "Test Issue #5",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #5 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 6, IssueCode = "000006", FPID = "060606-6-06-06", Subject = "Test Issue #6",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #6 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 7, IssueCode = "000007", FPID = "070707-7-07-07", Subject = "Test Issue #7",
        //        //        FDOTUserId = 1, CADDSoftwareProductId = 1, FDOTReleaseVersionId = 1,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 24, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #7 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 8, IssueCode = "000008", FPID = "080808-8-08-08", Subject = "Test Issue #8",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #8 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 9, IssueCode = "000009", FPID = "090909-9-09-09", Subject = "Test Issue #9",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #9 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 10, IssueCode = "000010", FPID = "101010-1-10-10", Subject = "Test Issue #10",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #10 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 11, IssueCode = "000011", FPID = "111111-1-11-11", Subject = "Test Issue #11",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #11 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 12, IssueCode = "000012", FPID = "121212-1-12-12", Subject = "Test Issue #12",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #12 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 13, IssueCode = "000013", FPID = "131313-1-13-13", Subject = "Test Issue #13",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #13 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //    new Issue { IssueId = 14, IssueCode = "000014", FPID = "141414-1-14-14", Subject = "Test Issue #14",
        //        //        FDOTUserId = 7, CADDSoftwareProductId = 2, FDOTReleaseVersionId = 2,  ECSOSupportCategoryId = 7, ECSOSupportProductId = 31, 
        //        //        IssueTypeId = 4, PriorityCodeId = 3, StatusCodeId = 1, WindowsOperatingSystemId  = 1,                         
        //        //        Details = "Test Issue #14 Details", CreateDate = DateTime.Now, ModifiedDate = DateTime.Now},     
        //        //};

        //        //issues.ForEach(d => context.Issues.Add(d));
        //        //context.SaveChanges();

        //        //// Add Seed Issue History
        //        //var issueHistories = new List<IssueHistory> {
        //        //    new IssueHistory { IssueHistoryId = 1, IssueId = 1, FDOTUserId = 1, IssueCode = "000001",
        //        //        CADDSoftware = "AutoCAD", ReleaseVersion = "FDOT2012.C3D", SupportCategory = "Electronic Delivery", SupportProduct = "Create FDOT Project", 
        //        //        Priority = "High", Status = "Open", Comment = "Changed Issue Priority to High", ModifiedDate = DateTime.Now}, 
        //        //    new IssueHistory { IssueHistoryId = 2, IssueId = 1, FDOTUserId = 1, IssueCode = "000001",
        //        //        CADDSoftware = "AutoCAD", ReleaseVersion = "FDOT2012.C3D", SupportCategory = "Electronic Delivery", SupportProduct = "Create FDOT Project", 
        //        //        Priority = "High", Status = "Closed", Comment = "Changed Issue Status to Closed", ModifiedDate = DateTime.Now}, 
        //        //};

        //        //issueHistories.ForEach(d => context.IssueHistories.Add(d));
        //        //context.SaveChanges();
        //        #endregion
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message;
        //    }
        //}
    }
}