namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeToJobListingsApplicationUsers : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JobListingApplicationUsers");
            AddColumn("dbo.JobListingApplicationUsers", "Type", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.JobListingApplicationUsers", new[] { "ApplicationUser_Id", "JobListing_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.JobListingApplicationUsers");
            DropColumn("dbo.JobListingApplicationUsers", "Type");
            AddPrimaryKey("dbo.JobListingApplicationUsers", new[] { "JobListing_Id", "ApplicationUser_Id" });
        }
    }
}
