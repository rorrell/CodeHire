namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResumeAndJobHistory1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserJobListings", newName: "JobListingApplicationUsers");
            DropPrimaryKey("dbo.JobListingApplicationUsers");
            AddPrimaryKey("dbo.JobListingApplicationUsers", new[] { "JobListing_Id", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.JobListingApplicationUsers");
            AddPrimaryKey("dbo.JobListingApplicationUsers", new[] { "ApplicationUser_Id", "JobListing_Id" });
            RenameTable(name: "dbo.JobListingApplicationUsers", newName: "ApplicationUserJobListings");
        }
    }
}
