namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestPaperSubjectTableAdded1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestPaperSubjects", newName: "TestPaperSubject");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TestPaperSubject", newName: "TestPaperSubjects");
        }
    }
}
