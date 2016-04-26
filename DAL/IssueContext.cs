using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Bug_Lite.Models
{
    public class IssueContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }

        public DbSet<WindowsOperatingSystem> OperatingSystems { get; set; }

        public DbSet<FDOTReleaseVersion> ReleaseVersions { get; set; }

        public DbSet<CADDSoftwareProduct> CADDSoftwareProducts { get; set; }

        public DbSet<ECSOSupportCategory> SupportCategories { get; set; }

        public DbSet<ECSOSupportProduct> SupportProducts { get; set; }

        public DbSet<FDOTUser> FDOTUsers { get; set; }

        public DbSet<UserSection> UserSections { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<IssueType> IssueTypes { get; set; }

        public DbSet<PriorityCode> PriorityCodes { get; set; }

        public DbSet<StatusCode> StatusCodes { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<IssueHistory> IssueHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use the fluent API to let Code First know about the foreign key properties for the Issues table
            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.IssueType)
                .WithMany()
                .HasForeignKey(i => i.IssueTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.WindowsOperatingSystem)
                .WithMany()
                .HasForeignKey(i => i.WindowsOperatingSystemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasOptional(i => i.CADDSoftwareProduct)
                .WithMany()
                .HasForeignKey(i => i.CADDSoftwareProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasOptional(i => i.FDOTReleaseVersion)
                .WithMany()
                .HasForeignKey(i => i.FDOTReleaseVersionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasOptional(i => i.ECSOSupportCategory)
                .WithMany()
                .HasForeignKey(i => i.ECSOSupportCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasOptional(i => i.ECSOSupportProduct)
                .WithMany()
                .HasForeignKey(i => i.ECSOSupportProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.FDOTUser)
                .WithMany()
                .HasForeignKey(i => i.FDOTUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.PriorityCode)
                .WithMany()
                .HasForeignKey(i => i.PriorityCodeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.StatusCode)
                .WithMany()
                .HasForeignKey(i => i.StatusCodeId)
                .WillCascadeOnDelete(false);

            // Use the fluent API to let Code First know about the foreign key properties for the FDOTUsers table
            modelBuilder.Entity<FDOTUser>()
                .HasRequired(u => u.UserSection)
                .WithMany()
                .HasForeignKey(u => u.UserSectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FDOTUser>()
                .HasRequired(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.UserRoleId)
                .WillCascadeOnDelete(false);
            
            // Use the fluent API to let Code First know about the foreign key properties for the FDOTReleaseVersions table
            modelBuilder.Entity<FDOTReleaseVersion>()
                .HasRequired(f => f.CADDSoftwareProduct)
                .WithMany(c => c.FDOTReleaseVersions)
                .HasForeignKey(f => f.CADDSoftwareProductId)
                .WillCascadeOnDelete(false);

            // Use the fluent API to let Code First know about the foreign key properties for the ECSOSupportProducts table
            modelBuilder.Entity<ECSOSupportProduct>()
                .HasRequired(e => e.ECSOSupportCategory)
                .WithMany(c => c.ECSOSupportProducts)
                .HasForeignKey(e => e.ECSOSupportCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ECSOSupportProduct>()
                .HasOptional(e => e.ApplicationDeveloper)
                .WithMany()
                .HasForeignKey(e => e.DeveloperId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ECSOSupportProduct>()
                .HasOptional(e => e.PrimarySupport)
                .WithMany()
                .HasForeignKey(e => e.PrimarySupportId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ECSOSupportProduct>()
                .HasOptional(e => e.SecondarySupport)
                .WithMany()
                .HasForeignKey(e => e.SecondarySupportId)
                .WillCascadeOnDelete(false);

            // Use the fluent API to let Code First know about the foreign key properties for the IssueHistory table
            modelBuilder.Entity<IssueHistory>()
                .HasOptional(h => h.FDOTUser)
                .WithMany()
                .HasForeignKey(h => h.FDOTUserId)
                .WillCascadeOnDelete(false);
        }
    }
}