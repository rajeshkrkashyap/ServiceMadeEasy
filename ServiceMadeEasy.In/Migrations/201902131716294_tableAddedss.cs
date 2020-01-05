namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableAddedss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserTestResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        TestPaperId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        SelectedOption = c.String(),
                        QuestionState = c.String(),
                        TimeSpendInSeconds = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId)
                .ForeignKey("dbo.TestPaper", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.TestPaperId)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserTestResult", "TestPaperId", "dbo.TestPaper");
            DropForeignKey("dbo.ApplicationUserTestResult", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserTestResult", "QuestionId", "dbo.Question");
            DropIndex("dbo.ApplicationUserTestResult", new[] { "QuestionId" });
            DropIndex("dbo.ApplicationUserTestResult", new[] { "TestPaperId" });
            DropIndex("dbo.ApplicationUserTestResult", new[] { "ApplicationUserId" });
            DropTable("dbo.ApplicationUserTestResult");
        }
    }
}
