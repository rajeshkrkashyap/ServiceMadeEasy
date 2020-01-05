namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestPaperSubjectTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestPaperSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestPaperId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.TestPaper", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.TestPaperId)
                .Index(t => t.SubjectId);
            
            AddColumn("dbo.Syllabus", "SyllabusText", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestPaperSubjects", "TestPaperId", "dbo.TestPaper");
            DropForeignKey("dbo.TestPaperSubjects", "SubjectId", "dbo.Subject");
            DropIndex("dbo.TestPaperSubjects", new[] { "SubjectId" });
            DropIndex("dbo.TestPaperSubjects", new[] { "TestPaperId" });
            DropColumn("dbo.Syllabus", "SyllabusText");
            DropTable("dbo.TestPaperSubjects");
        }
    }
}
