namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableRelactionChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Syllabus", "TestPaper_Id", "dbo.TestPaper");
            DropIndex("dbo.Syllabus", new[] { "TestPaper_Id" });
            CreateIndex("dbo.TestPaper", "SyllabusId");
            AddForeignKey("dbo.TestPaper", "SyllabusId", "dbo.Syllabus", "Id", cascadeDelete: true);
            DropColumn("dbo.Syllabus", "TestPaper_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Syllabus", "TestPaper_Id", c => c.Int());
            DropForeignKey("dbo.TestPaper", "SyllabusId", "dbo.Syllabus");
            DropIndex("dbo.TestPaper", new[] { "SyllabusId" });
            CreateIndex("dbo.Syllabus", "TestPaper_Id");
            AddForeignKey("dbo.Syllabus", "TestPaper_Id", "dbo.TestPaper", "Id");
        }
    }
}
