using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bug_Lite.Models
{
    public class Issue
    {
        public int IssueId                  { get; set; } // Primary Key        
        public int IssueTypeId              { get; set; } // Foreign Key to the IssueTypes Table
        public int WindowsOperatingSystemId { get; set; } // Foreign Key to the WindowsOperatingSystems Table
        public int? CADDSoftwareProductId   { get; set; } // Foreign Key to the CADDSoftwareProducts Table
        public int? FDOTReleaseVersionId    { get; set; } // Foreign Key to the ReleaseVersions Table
        public int? ECSOSupportCategoryId   { get; set; } // Foreign Key to the ECSOSupportCategorys Table
        public int? ECSOSupportProductId    { get; set; } // Foreign Key to the ECSOSupportProducts Table

        [Required(ErrorMessage = "Assign To is Required")]
        public int FDOTUserId               { get; set; } // Foreign Key to the FDOTUsers Table
        public int CreatedById              { get; set; } // Foreign Key to the FDOTUsers Table
        public int PriorityCodeId           { get; set; } // Foreign Key to the PriorityCodes Table
        public int StatusCodeId             { get; set; } // Foreign Key to the StatusCodes Table
        public int? ContactId               { get; set; } // Foreign Key to the Contacts Table

        [Column("IssueCode")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [Display(Name = "Issue Number")]
        public string IssueCode { get; set; } // Candidate Key
        
        [Column("Subject")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Subject is Required")]
        [Display(Name = "Subject")]
        public string Subject { get; set; } // Subject of the Issue being reported.

        [Column("FPID")]
        [DataType(DataType.Text)]
        [MaxLength(15)]
        [Display(Name = "FPID")]
        public string FPID { get; set; } // Financial Project Identifier

        [Column("Details")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Issue Detail is Required")]
        [Display(Name = "Details")]
        public string Details { get; set; } // Details of the Issue being reported. How did it occur? What steps did you take?        

        [Column("CreateDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } // Date the Issue was Created

        [Column("ModifiedDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; } // Date the Issue was last Modified

        //[Column("Notes")]
        //[DataType(DataType.MultilineText)]
        //[Required(ErrorMessage = "Issue Notes is Required")]
        //[Display(Name = "Notes")]
        //public string Notes { get; set; } // Notes of the Issue being reported. How did it occur? What steps did you take?

        // Navigation Properties for Foreign Keys
        [ForeignKey("IssueTypeId")]
        public virtual IssueType IssueType { get; set; }

        [ForeignKey("WindowsOperatingSystemId")]
        public virtual WindowsOperatingSystem WindowsOperatingSystem { get; set; }

        [ForeignKey("CADDSoftwareProductId")]
        public virtual CADDSoftwareProduct CADDSoftwareProduct { get; set; }

        [ForeignKey("FDOTReleaseVersionId")]
        public virtual FDOTReleaseVersion FDOTReleaseVersion { get; set; }

        [ForeignKey("ECSOSupportCategoryId")]
        public virtual ECSOSupportCategory ECSOSupportCategory { get; set; }

        [ForeignKey("ECSOSupportProductId")]
        public virtual ECSOSupportProduct ECSOSupportProduct { get; set; }

        [ForeignKey("FDOTUserId")]
        public virtual FDOTUser FDOTUser { get; set; }

        [ForeignKey("CreatedById")]
        public virtual FDOTUser CreatedBy { get; set; }

        [ForeignKey("PriorityCodeId")]
        public virtual PriorityCode PriorityCode { get; set; }

        [ForeignKey("StatusCodeId")]
        public virtual StatusCode StatusCode { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

        // Navigation Properties for Zero to Many realationships
        public virtual ICollection<Attachment> Attachments { get; set; } // Navigation Property for Attachment

        public virtual ICollection<IssueHistory> IssueHistory { get; set; } // Navigation Property for IssueHistory
    }

    public class IssueType
    {
        public int IssueTypeId { get; set; } // Primary Key

        [Column("IssueType")]
        [DataType(DataType.Text)]
        [MaxLength(25)]
        [Required(ErrorMessage = "Issue Type is Required")]
        [Display(Name = "Issue Type")]
        public string Type { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class WindowsOperatingSystem
    {
        public int WindowsOperatingSystemId { get; set; } // Primary Key

        [Column("WindowsOperatingSystem")]
        [DataType(DataType.Text)]
        [MaxLength(25)]
        [Required(ErrorMessage = "Operating System is Required")]
        [Display(Name = "Operating System")]
        public string OperatingSystem { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class CADDSoftwareProduct
    {
        public int CADDSoftwareProductId    { get; set; } // Primary Key

        [Column("CADDSoftwareProduct")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required(ErrorMessage = "CADD Software Product is Required")]
        [Display(Name = "CADD Platform")]
        public string CADDSoftware { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        // Navigation Properties for Zero to Many realationships
        public virtual ICollection<FDOTReleaseVersion> FDOTReleaseVersions { get; set; } // Navigation Property for ReleaseVersions
    }

    public class ECSOSupportCategory
    {
        public int ECSOSupportCategoryId { get; set; } // Primary Key

        [Column("ECSOSupportCategory")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required(ErrorMessage = "ECSO Support Category is Required")]
        [Display(Name = "Support Category")]
        public string SupportCategory { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        public virtual ICollection<ECSOSupportProduct> ECSOSupportProducts { get; set; } // Navigation Property for ECSOSupportProducts
    }

    public class ECSOSupportProduct
    {
        public int ECSOSupportProductId { get; set; } // Primary Key

        [Required(ErrorMessage = "ECSO Support Category is Required")]
        public int ECSOSupportCategoryId { get; set; } // Foreign Key to the ECSOSupportCategories Table

        public int? DeveloperId { get; set; } // Foreign Key to the FDOTUser Table
        public int? PrimarySupportId { get; set; } // Foreign Key to the FDOTUser Table
        public int? SecondarySupportId { get; set; } // Foreign Key to the FDOTUser Table

        [Column("ECSOSupportProduct")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required(ErrorMessage = "ECSO Support Product is Required")]
        [Display(Name = "Support Product")]
        public string SupportProduct { get; set; }

        // Navigation Properties for Foreign Keys
        [ForeignKey("ECSOSupportCategoryId")]
        public virtual ECSOSupportCategory ECSOSupportCategory { get; set; }

        [ForeignKey("DeveloperId")]
        public virtual FDOTUser ApplicationDeveloper { get; set; }

        [ForeignKey("PrimarySupportId")]
        public virtual FDOTUser PrimarySupport { get; set; }

        [ForeignKey("SecondarySupportId")]
        public virtual FDOTUser SecondarySupport { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class FDOTReleaseVersion
    {
        public int FDOTReleaseVersionId  { get; set; } // Primary Key

        [Required(ErrorMessage = "CADD Software Product is Required")]
        public int CADDSoftwareProductId { get; set; } // Foreign Key to the CADDSoftwareProducts Table

        [Column("FDOTReleaseVersion")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Required(ErrorMessage = "FDOT Release Version is Required")]
        [Display(Name = "FDOT Release Version")]
        public string ReleaseVersion { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

        // Navigation Properties for Foreign Keys
        [ForeignKey("CADDSoftwareProductId")]
        public virtual CADDSoftwareProduct CADDSoftwareProduct { get; set; } // Navigation Property to the CADDSoftwareProducts Table
    }

    public class FDOTUser
    {
        public int FDOTUserId { get; set; }     // Primary Key

        [Required(ErrorMessage = "Section is Required")]
        public int UserSectionId { get; set; }  // Foreign Key to the UserSection Table
        
        [Required(ErrorMessage = "Role is Required")]
        public int UserRoleId { get; set; }     // Foreign Key to the Role Table

        [MaxLength(50)]
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Column("Username")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Column("Password")]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Column("PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Column("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Email Address is Required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [ForeignKey("UserSectionId")]
        public virtual UserSection UserSection { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }
    
    public class UserSection
    {
        public int UserSectionId { get; set; } // Primary Key

        [Column("UserSection")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Required(ErrorMessage = "Section is Required")]
        [Display(Name = "Section")]
        public string Section { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class UserRole
    {
        public int UserRoleId { get; set; } // Primary Key

        [Column("Role")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        [Required(ErrorMessage = "Role is Required")]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class PriorityCode
    {
        public int PriorityCodeId { get; set; } // Primary Key

        [Column("PriorityCode")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Required(ErrorMessage = "Priority Code is Required")]
        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }
       
    public class StatusCode
    {
        public int StatusCodeId { get; set; } // Primary Key

        [Column("StatusCode")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        [Required(ErrorMessage = "Status Code is Required")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class Contact
    {
        public int ContactId { get; set; } // Primary Key

        [Column("ContactName")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required(ErrorMessage = "Contact Name is Required")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Column("District")]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Display(Name = "District")]
        public string District { get; set; }

        [Column("Company")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Column("JobTitle")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Display(Name = "Job Subject")]
        public string JobTitle { get; set; }

        [Column("ContactNumber")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(25)]
        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        [Column("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Email Address is Required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }

    public class Attachment
    {
        public int AttachmentId {set; get; } // Primary Key
        public int IssueId { get; set; } // Foreign Key to the Issues Table

        [Column("FileName")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Column("ContentType")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }

        public byte[] FileData { get; set; }
    }

    public class IssueHistory
    {
        public int IssueHistoryId   { get; set; } // Primary Key
        public int IssueId          { get; set; } // Foreign Key to the Issues Table
        public int? FDOTUserId      { get; set; } // Foreign Key to the FDOTUsers Table

        [Column("IssueCode")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string IssueCode { get; set; } // Candidate Key 

        [Column("StatusCode")]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        [Display(Name = "Status Code")]
        public string Status { get; set; }

        [Column("Comment")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Notes are Required")]
        [MaxLength(1000)]
        public string Comment { get; set; } // Make a note on what was changed and why?

        //[Column("Notes")]
        //[DataType(DataType.MultilineText)]
        //[MaxLength(1000)]
        //public string Notes { get; set; } // Comment on what was changed and why?

        [Column("ModifiedDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; } // Date the Issue was last Modified

        // Navigation Properties for Foreign Key
        [Column("FDOTUserId")]
        public virtual FDOTUser FDOTUser { get; set; }
    }
}