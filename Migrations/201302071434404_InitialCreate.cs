namespace Bug_Lite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueId = c.Int(nullable: false, identity: true),
                        IssueTypeId = c.Int(nullable: false),
                        WindowsOperatingSystemId = c.Int(nullable: false),
                        CADDSoftwareProductId = c.Int(),
                        FDOTReleaseVersionId = c.Int(),
                        ECSOSupportCategoryId = c.Int(),
                        ECSOSupportProductId = c.Int(),
                        FDOTUserId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        PriorityCodeId = c.Int(nullable: false),
                        StatusCodeId = c.Int(nullable: false),
                        ContactId = c.Int(),
                        IssueCode = c.String(maxLength: 10),
                        Subject = c.String(nullable: false, maxLength: 100),
                        FPID = c.String(maxLength: 15),
                        Details = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IssueId)
                .ForeignKey("dbo.IssueTypes", t => t.IssueTypeId)
                .ForeignKey("dbo.WindowsOperatingSystems", t => t.WindowsOperatingSystemId)
                .ForeignKey("dbo.CADDSoftwareProducts", t => t.CADDSoftwareProductId)
                .ForeignKey("dbo.FDOTReleaseVersions", t => t.FDOTReleaseVersionId)
                .ForeignKey("dbo.ECSOSupportCategories", t => t.ECSOSupportCategoryId)
                .ForeignKey("dbo.ECSOSupportProducts", t => t.ECSOSupportProductId)
                .ForeignKey("dbo.FDOTUsers", t => t.FDOTUserId)
                .ForeignKey("dbo.FDOTUsers", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.PriorityCodes", t => t.PriorityCodeId)
                .ForeignKey("dbo.StatusCodes", t => t.StatusCodeId)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .Index(t => t.IssueTypeId)
                .Index(t => t.WindowsOperatingSystemId)
                .Index(t => t.CADDSoftwareProductId)
                .Index(t => t.FDOTReleaseVersionId)
                .Index(t => t.ECSOSupportCategoryId)
                .Index(t => t.ECSOSupportProductId)
                .Index(t => t.FDOTUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.PriorityCodeId)
                .Index(t => t.StatusCodeId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.IssueTypes",
                c => new
                    {
                        IssueTypeId = c.Int(nullable: false, identity: true),
                        IssueType = c.String(nullable: false, maxLength: 25),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IssueTypeId);
            
            CreateTable(
                "dbo.WindowsOperatingSystems",
                c => new
                    {
                        WindowsOperatingSystemId = c.Int(nullable: false, identity: true),
                        WindowsOperatingSystem = c.String(nullable: false, maxLength: 25),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WindowsOperatingSystemId);
            
            CreateTable(
                "dbo.CADDSoftwareProducts",
                c => new
                    {
                        CADDSoftwareProductId = c.Int(nullable: false, identity: true),
                        CADDSoftwareProduct = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CADDSoftwareProductId);
            
            CreateTable(
                "dbo.FDOTReleaseVersions",
                c => new
                    {
                        FDOTReleaseVersionId = c.Int(nullable: false, identity: true),
                        CADDSoftwareProductId = c.Int(nullable: false),
                        FDOTReleaseVersion = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FDOTReleaseVersionId)
                .ForeignKey("dbo.CADDSoftwareProducts", t => t.CADDSoftwareProductId)
                .Index(t => t.CADDSoftwareProductId);
            
            CreateTable(
                "dbo.ECSOSupportCategories",
                c => new
                    {
                        ECSOSupportCategoryId = c.Int(nullable: false, identity: true),
                        ECSOSupportCategory = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ECSOSupportCategoryId);
            
            CreateTable(
                "dbo.ECSOSupportProducts",
                c => new
                    {
                        ECSOSupportProductId = c.Int(nullable: false, identity: true),
                        ECSOSupportCategoryId = c.Int(nullable: false),
                        DeveloperId = c.Int(),
                        PrimarySupportId = c.Int(),
                        SecondarySupportId = c.Int(),
                        ECSOSupportProduct = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ECSOSupportProductId)
                .ForeignKey("dbo.ECSOSupportCategories", t => t.ECSOSupportCategoryId)
                .ForeignKey("dbo.FDOTUsers", t => t.DeveloperId)
                .ForeignKey("dbo.FDOTUsers", t => t.PrimarySupportId)
                .ForeignKey("dbo.FDOTUsers", t => t.SecondarySupportId)
                .Index(t => t.ECSOSupportCategoryId)
                .Index(t => t.DeveloperId)
                .Index(t => t.PrimarySupportId)
                .Index(t => t.SecondarySupportId);
            
            CreateTable(
                "dbo.FDOTUsers",
                c => new
                    {
                        FDOTUserId = c.Int(nullable: false, identity: true),
                        UserSectionId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Username = c.String(maxLength: 20),
                        Password = c.String(maxLength: 20),
                        PhoneNumber = c.String(nullable: false, maxLength: 25),
                        EmailAddress = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FDOTUserId)
                .ForeignKey("dbo.UserSections", t => t.UserSectionId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId)
                .Index(t => t.UserSectionId)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserSections",
                c => new
                    {
                        UserSectionId = c.Int(nullable: false, identity: true),
                        UserSection = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserSectionId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        Role = c.String(nullable: false, maxLength: 10),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.PriorityCodes",
                c => new
                    {
                        PriorityCodeId = c.Int(nullable: false, identity: true),
                        PriorityCode = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PriorityCodeId);
            
            CreateTable(
                "dbo.StatusCodes",
                c => new
                    {
                        StatusCodeId = c.Int(nullable: false, identity: true),
                        StatusCode = c.String(nullable: false, maxLength: 30),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StatusCodeId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        ContactName = c.String(nullable: false, maxLength: 50),
                        District = c.String(maxLength: 20),
                        Company = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        ContactNumber = c.String(nullable: false, maxLength: 25),
                        EmailAddress = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 100),
                        ContentType = c.String(maxLength: 20),
                        FileData = c.Binary(),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
            CreateTable(
                "dbo.IssueHistories",
                c => new
                    {
                        IssueHistoryId = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        FDOTUserId = c.Int(),
                        IssueCode = c.String(maxLength: 10),
                        StatusCode = c.String(maxLength: 30),
                        Comment = c.String(maxLength: 1000),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IssueHistoryId)
                .ForeignKey("dbo.FDOTUsers", t => t.FDOTUserId)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.FDOTUserId)
                .Index(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.IssueHistories", new[] { "IssueId" });
            DropIndex("dbo.IssueHistories", new[] { "FDOTUserId" });
            DropIndex("dbo.Attachments", new[] { "IssueId" });
            DropIndex("dbo.FDOTUsers", new[] { "UserRoleId" });
            DropIndex("dbo.FDOTUsers", new[] { "UserSectionId" });
            DropIndex("dbo.ECSOSupportProducts", new[] { "SecondarySupportId" });
            DropIndex("dbo.ECSOSupportProducts", new[] { "PrimarySupportId" });
            DropIndex("dbo.ECSOSupportProducts", new[] { "DeveloperId" });
            DropIndex("dbo.ECSOSupportProducts", new[] { "ECSOSupportCategoryId" });
            DropIndex("dbo.FDOTReleaseVersions", new[] { "CADDSoftwareProductId" });
            DropIndex("dbo.Issues", new[] { "ContactId" });
            DropIndex("dbo.Issues", new[] { "StatusCodeId" });
            DropIndex("dbo.Issues", new[] { "PriorityCodeId" });
            DropIndex("dbo.Issues", new[] { "CreatedById" });
            DropIndex("dbo.Issues", new[] { "FDOTUserId" });
            DropIndex("dbo.Issues", new[] { "ECSOSupportProductId" });
            DropIndex("dbo.Issues", new[] { "ECSOSupportCategoryId" });
            DropIndex("dbo.Issues", new[] { "FDOTReleaseVersionId" });
            DropIndex("dbo.Issues", new[] { "CADDSoftwareProductId" });
            DropIndex("dbo.Issues", new[] { "WindowsOperatingSystemId" });
            DropIndex("dbo.Issues", new[] { "IssueTypeId" });
            DropForeignKey("dbo.IssueHistories", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.IssueHistories", "FDOTUserId", "dbo.FDOTUsers");
            DropForeignKey("dbo.Attachments", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.FDOTUsers", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.FDOTUsers", "UserSectionId", "dbo.UserSections");
            DropForeignKey("dbo.ECSOSupportProducts", "SecondarySupportId", "dbo.FDOTUsers");
            DropForeignKey("dbo.ECSOSupportProducts", "PrimarySupportId", "dbo.FDOTUsers");
            DropForeignKey("dbo.ECSOSupportProducts", "DeveloperId", "dbo.FDOTUsers");
            DropForeignKey("dbo.ECSOSupportProducts", "ECSOSupportCategoryId", "dbo.ECSOSupportCategories");
            DropForeignKey("dbo.FDOTReleaseVersions", "CADDSoftwareProductId", "dbo.CADDSoftwareProducts");
            DropForeignKey("dbo.Issues", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Issues", "StatusCodeId", "dbo.StatusCodes");
            DropForeignKey("dbo.Issues", "PriorityCodeId", "dbo.PriorityCodes");
            DropForeignKey("dbo.Issues", "CreatedById", "dbo.FDOTUsers");
            DropForeignKey("dbo.Issues", "FDOTUserId", "dbo.FDOTUsers");
            DropForeignKey("dbo.Issues", "ECSOSupportProductId", "dbo.ECSOSupportProducts");
            DropForeignKey("dbo.Issues", "ECSOSupportCategoryId", "dbo.ECSOSupportCategories");
            DropForeignKey("dbo.Issues", "FDOTReleaseVersionId", "dbo.FDOTReleaseVersions");
            DropForeignKey("dbo.Issues", "CADDSoftwareProductId", "dbo.CADDSoftwareProducts");
            DropForeignKey("dbo.Issues", "WindowsOperatingSystemId", "dbo.WindowsOperatingSystems");
            DropForeignKey("dbo.Issues", "IssueTypeId", "dbo.IssueTypes");
            DropTable("dbo.IssueHistories");
            DropTable("dbo.Attachments");
            DropTable("dbo.Contacts");
            DropTable("dbo.StatusCodes");
            DropTable("dbo.PriorityCodes");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserSections");
            DropTable("dbo.FDOTUsers");
            DropTable("dbo.ECSOSupportProducts");
            DropTable("dbo.ECSOSupportCategories");
            DropTable("dbo.FDOTReleaseVersions");
            DropTable("dbo.CADDSoftwareProducts");
            DropTable("dbo.WindowsOperatingSystems");
            DropTable("dbo.IssueTypes");
            DropTable("dbo.Issues");
        }
    }
}
