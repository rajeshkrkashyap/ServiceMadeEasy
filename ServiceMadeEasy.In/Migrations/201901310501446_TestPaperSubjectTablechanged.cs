namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestPaperSubjectTablechanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestPaperSubject", "SingleType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "SingleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MultipleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "PassageHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "MatchTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaperSubject", "IntegerTypeHard", c => c.Int(nullable: false));
            DropColumn("dbo.TestPaper", "SingleType");
            DropColumn("dbo.TestPaper", "SingleTypeEasy");
            DropColumn("dbo.TestPaper", "SingleTypeMedium");
            DropColumn("dbo.TestPaper", "SingleTypeHard");
            DropColumn("dbo.TestPaper", "MultipleType");
            DropColumn("dbo.TestPaper", "MultipleTypeEasy");
            DropColumn("dbo.TestPaper", "MultipleTypeMedium");
            DropColumn("dbo.TestPaper", "MultipleTypeHard");
            DropColumn("dbo.TestPaper", "PassageType");
            DropColumn("dbo.TestPaper", "PassageEasy");
            DropColumn("dbo.TestPaper", "PassageMedium");
            DropColumn("dbo.TestPaper", "PassageHard");
            DropColumn("dbo.TestPaper", "MatchType");
            DropColumn("dbo.TestPaper", "MatchTypeEasy");
            DropColumn("dbo.TestPaper", "MatchTypeMedium");
            DropColumn("dbo.TestPaper", "MatchTypeHard");
            DropColumn("dbo.TestPaper", "IntegerType");
            DropColumn("dbo.TestPaper", "IntegerTypeEasy");
            DropColumn("dbo.TestPaper", "IntegerTypeMedium");
            DropColumn("dbo.TestPaper", "IntegerTypeHard");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestPaper", "IntegerTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "IntegerTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "IntegerTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "IntegerType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MatchTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MatchTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MatchTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MatchType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "PassageHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "PassageMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "PassageEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "PassageType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MultipleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MultipleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MultipleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "MultipleType", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "SingleTypeHard", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "SingleTypeMedium", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "SingleTypeEasy", c => c.Int(nullable: false));
            AddColumn("dbo.TestPaper", "SingleType", c => c.Int(nullable: false));
            DropColumn("dbo.TestPaperSubject", "IntegerTypeHard");
            DropColumn("dbo.TestPaperSubject", "IntegerTypeMedium");
            DropColumn("dbo.TestPaperSubject", "IntegerTypeEasy");
            DropColumn("dbo.TestPaperSubject", "IntegerType");
            DropColumn("dbo.TestPaperSubject", "MatchTypeHard");
            DropColumn("dbo.TestPaperSubject", "MatchTypeMedium");
            DropColumn("dbo.TestPaperSubject", "MatchTypeEasy");
            DropColumn("dbo.TestPaperSubject", "MatchType");
            DropColumn("dbo.TestPaperSubject", "PassageHard");
            DropColumn("dbo.TestPaperSubject", "PassageMedium");
            DropColumn("dbo.TestPaperSubject", "PassageEasy");
            DropColumn("dbo.TestPaperSubject", "PassageType");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeHard");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeMedium");
            DropColumn("dbo.TestPaperSubject", "MultipleTypeEasy");
            DropColumn("dbo.TestPaperSubject", "MultipleType");
            DropColumn("dbo.TestPaperSubject", "SingleTypeHard");
            DropColumn("dbo.TestPaperSubject", "SingleTypeMedium");
            DropColumn("dbo.TestPaperSubject", "SingleTypeEasy");
            DropColumn("dbo.TestPaperSubject", "SingleType");
        }
    }
}
