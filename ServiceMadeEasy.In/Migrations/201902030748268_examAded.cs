namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examAded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestPaper", "ExamId", c => c.Int(nullable: false));
            CreateIndex("dbo.TestPaper", "ExamId");
            AddForeignKey("dbo.TestPaper", "ExamId", "dbo.Exam", "Id", cascadeDelete: true);
            DropColumn("dbo.TestPaperSubject", "SingleType");
            DropColumn("dbo.TestPaperSubject", "SingleTypeEasy");
            DropColumn("dbo.TestPaperSubject", "SingleTypeMedium");
            DropColumn("dbo.TestPaperSubject", "SingleTypeHard");
            DropColumn("dbo.TestPaperSubject", "MultipleType");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeEasy");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeMedium");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeHard");
            DropColumn("dbo.TestPaperSubject", "PassageType");
            DropColumn("dbo.TestPaperSubject", "PassageEasy");
            DropColumn("dbo.TestPaperSubject", "PassageMedium");
            DropColumn("dbo.TestPaperSubject", "PassageHard");
            DropColumn("dbo.TestPaperSubject", "MatchType");
            DropColumn("dbo.TestPaperSubject", "MatchTypeEasy");
            DropColumn("dbo.TestPaperSubject", "MatchTypeMedium");
            DropColumn("dbo.TestPaperSubject", "MatchTypeHard");
            DropColumn("dbo.TestPaperSubject", "IntegerType");
            DropColumn("dbo.TestPaperSubject", "IntegerTypeEasy");
            DropColumn("dbo.TestPaperSubject", "IntegerTypeMedium");
            DropColumn("dbo.TestPaperSubject", "IntegerTypeHard");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestPaperSubject", "IntegerTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleType", c => c.Int(nullable: false));
            DropForeignKey("dbo.TestPaper", "ExamId", "dbo.Exam");
            DropIndex("dbo.TestPaper", new[] { "ExamId" });
            DropColumn("dbo.TestPaper", "ExamId");
        }
    }
}
