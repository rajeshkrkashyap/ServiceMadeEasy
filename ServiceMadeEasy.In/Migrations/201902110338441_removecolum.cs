namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecolum : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Syllabus", "SubjectId");
            DropColumn("dbo.Syllabus", "ClassId");
            DropColumn("dbo.Syllabus", "ChapterId");
            DropColumn("dbo.Syllabus", "TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Syllabus", "TopicId", c => c.Int(nullable: false));
            AddColumn("dbo.Syllabus", "ChapterId", c => c.Int(nullable: false));
            AddColumn("dbo.Syllabus", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.Syllabus", "SubjectId", c => c.Int(nullable: false));
        }
    }
}
