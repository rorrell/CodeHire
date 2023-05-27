namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipBetweenUsersAndJobListings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserJobListings",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        JobListing_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.JobListing_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.JobListings", t => t.JobListing_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.JobListing_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserJobListings", "JobListing_Id", "dbo.JobListings");
            DropForeignKey("dbo.ApplicationUserJobListings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserJobListings", new[] { "JobListing_Id" });
            DropIndex("dbo.ApplicationUserJobListings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserJobListings");
        }
    }
}
