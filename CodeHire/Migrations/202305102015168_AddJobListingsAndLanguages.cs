namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobListingsAndLanguages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobListings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Company = c.String(nullable: false, maxLength: 255),
                        Location = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ExpirationDate = c.DateTime(),
                        Wage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity:true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguageJobListings",
                c => new
                    {
                        Language_Id = c.Byte(nullable: false),
                        JobListing_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.JobListing_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.JobListings", t => t.JobListing_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.JobListing_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LanguageJobListings", "JobListing_Id", "dbo.JobListings");
            DropForeignKey("dbo.LanguageJobListings", "Language_Id", "dbo.Languages");
            DropIndex("dbo.LanguageJobListings", new[] { "JobListing_Id" });
            DropIndex("dbo.LanguageJobListings", new[] { "Language_Id" });
            DropTable("dbo.LanguageJobListings");
            DropTable("dbo.Languages");
            DropTable("dbo.JobListings");
        }
    }
}
