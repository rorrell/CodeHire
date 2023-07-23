namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillResumes",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Resume_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Resume_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.Resume_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Resume_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillResumes", "Resume_Id", "dbo.Resumes");
            DropForeignKey("dbo.SkillResumes", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillResumes", new[] { "Resume_Id" });
            DropIndex("dbo.SkillResumes", new[] { "Skill_Id" });
            DropTable("dbo.SkillResumes");
            DropTable("dbo.Skills");
        }
    }
}
