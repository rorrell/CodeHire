namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResumeAndJobHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summary = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.JobHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        Company = c.String(nullable: false),
                        Location = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Resume_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id)
                .Index(t => t.Resume_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobHistories", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.Resumes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.JobHistories", new[] { "Resume_Id" });
            DropIndex("dbo.Resumes", new[] { "User_Id" });
            DropTable("dbo.JobHistories");
            DropTable("dbo.Resumes");
        }
    }
}
