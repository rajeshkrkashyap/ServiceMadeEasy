namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Chapter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(),
                        SubjectId = c.Int(),
                        ChapterId = c.Int(),
                        TopicId = c.Int(),
                        PassageGroupId = c.Int(),
                        TabId = c.String(),
                        Text = c.String(),
                        PassageText = c.String(),
                        IsMultipleAnswer = c.Boolean(nullable: false),
                        IsReviewed = c.Boolean(nullable: false),
                        QuestionTypeId = c.Int(),
                        DificultyLevelId = c.Int(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapter", t => t.ChapterId)
                .ForeignKey("dbo.Class", t => t.ClassId)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .ForeignKey("dbo.DificultyLevel", t => t.DificultyLevelId)
                .ForeignKey("dbo.QuestionType", t => t.QuestionTypeId)
                .ForeignKey("dbo.Topic", t => t.TopicId)
                .Index(t => t.ClassId)
                .Index(t => t.SubjectId)
                .Index(t => t.ChapterId)
                .Index(t => t.TopicId)
                .Index(t => t.QuestionTypeId)
                .Index(t => t.DificultyLevelId);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(),
                        Name = c.String(),
                        NameWithClass = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Class", t => t.ClassId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.DificultyLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExamQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exam", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Exam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(),
                        AnswerCounter = c.Int(),
                        Answer = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.QuestionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Solution",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        SolutionText = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChapterId = c.Int(),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapter", t => t.ChapterId)
                .Index(t => t.ChapterId);
            
            CreateTable(
                "dbo.ImageResource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarkingScheme",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SingleRight = c.Single(nullable: false),
                        SingleWrong = c.Single(nullable: false),
                        IsMultiplePartial = c.Single(nullable: false),
                        MultipleRight = c.Single(nullable: false),
                        MultipleWrong = c.Single(nullable: false),
                        IntegerRight = c.Single(nullable: false),
                        IntegerWrong = c.Single(nullable: false),
                        IsPassagePartial = c.Single(nullable: false),
                        PassageRight = c.Single(nullable: false),
                        PassageWrong = c.Single(nullable: false),
                        MatchRight = c.Single(nullable: false),
                        MatchWrong = c.Single(nullable: false),
                        MarkingSchemeHtml = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestPaper",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MarkingSchemeId = c.Int(nullable: false),
                        SyllabusId = c.Int(nullable: false),
                        Name = c.String(),
                        Minutes = c.Int(nullable: false),
                        SingleType = c.Int(nullable: false),
                        SingleTypeEasy = c.Int(nullable: false),
                        SingleTypeMedium = c.Int(nullable: false),
                        SingleTypeHard = c.Int(nullable: false),
                        MultipleType = c.Int(nullable: false),
                        MultipleTypeEasy = c.Int(nullable: false),
                        MultipleTypeMedium = c.Int(nullable: false),
                        MultipleTypeHard = c.Int(nullable: false),
                        PassageType = c.Int(nullable: false),
                        PassageEasy = c.Int(nullable: false),
                        PassageMedium = c.Int(nullable: false),
                        PassageHard = c.Int(nullable: false),
                        MatchType = c.Int(nullable: false),
                        MatchTypeEasy = c.Int(nullable: false),
                        MatchTypeMedium = c.Int(nullable: false),
                        MatchTypeHard = c.Int(nullable: false),
                        IntegerType = c.Int(nullable: false),
                        IntegerTypeEasy = c.Int(nullable: false),
                        IntegerTypeMedium = c.Int(nullable: false),
                        IntegerTypeHard = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarkingScheme", t => t.MarkingSchemeId, cascadeDelete: true)
                .Index(t => t.MarkingSchemeId);
            
            CreateTable(
                "dbo.Syllabus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        ChapterId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                        TestPaper_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestPaper", t => t.TestPaper_Id)
                .Index(t => t.TestPaper_Id);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserPackage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        MID = c.String(),
                        TXNID = c.String(),
                        ORDERID = c.String(),
                        BANKTXNID = c.String(),
                        TXNAMOUNT = c.String(),
                        CURRENCY = c.String(),
                        PAYMENTMODE = c.String(),
                        TXNDATE = c.String(),
                        STATUS = c.String(),
                        RESPCODE = c.String(),
                        RESPMSG = c.String(),
                        GATEWAYNAME = c.String(),
                        BANKNAME = c.String(),
                        CHECKSUMHASH = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Package", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.PackageId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TestPaperPackage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(nullable: false),
                        TestPaperId = c.Int(nullable: false),
                        TestPaperReleaseDate = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Package", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.TestPaper", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.PackageId)
                .Index(t => t.TestPaperId);
            
            CreateTable(
                "dbo.TestPaperQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestPaperId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.TestPaper", t => t.TestPaperId, cascadeDelete: true)
                .Index(t => t.TestPaperId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Pincode = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Pincode = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Sponsor",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Pincode = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(),
                        PresentClass = c.String(),
                        Stream = c.String(),
                        School = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Pincode = c.String(),
                        Country = c.String(),
                        Religion = c.String(),
                        Cast = c.String(),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Pincode = c.String(),
                        Created = c.DateTime(),
                        Updated = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUser", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Sponsor", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Employee", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Admin", "Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserRole", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserLogin", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserClaim", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.TestPaperQuestion", "TestPaperId", "dbo.TestPaper");
            DropForeignKey("dbo.TestPaperQuestion", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.TestPaperPackage", "TestPaperId", "dbo.TestPaper");
            DropForeignKey("dbo.TestPaperPackage", "PackageId", "dbo.Package");
            DropForeignKey("dbo.ApplicationUserPackage", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserPackage", "PackageId", "dbo.Package");
            DropForeignKey("dbo.ApplicationUserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Syllabus", "TestPaper_Id", "dbo.TestPaper");
            DropForeignKey("dbo.TestPaper", "MarkingSchemeId", "dbo.MarkingScheme");
            DropForeignKey("dbo.Question", "TopicId", "dbo.Topic");
            DropForeignKey("dbo.Topic", "ChapterId", "dbo.Chapter");
            DropForeignKey("dbo.Solution", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "QuestionTypeId", "dbo.QuestionType");
            DropForeignKey("dbo.Option", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.ExamQuestion", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.ExamQuestion", "ExamId", "dbo.Exam");
            DropForeignKey("dbo.Question", "DificultyLevelId", "dbo.DificultyLevel");
            DropForeignKey("dbo.Question", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Subject", "ClassId", "dbo.Class");
            DropForeignKey("dbo.Chapter", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Question", "ClassId", "dbo.Class");
            DropForeignKey("dbo.Question", "ChapterId", "dbo.Chapter");
            DropIndex("dbo.Teacher", new[] { "Id" });
            DropIndex("dbo.ApplicationUser", new[] { "Id" });
            DropIndex("dbo.Sponsor", new[] { "Id" });
            DropIndex("dbo.Employee", new[] { "Id" });
            DropIndex("dbo.Admin", new[] { "Id" });
            DropIndex("dbo.TestPaperQuestion", new[] { "QuestionId" });
            DropIndex("dbo.TestPaperQuestion", new[] { "TestPaperId" });
            DropIndex("dbo.TestPaperPackage", new[] { "TestPaperId" });
            DropIndex("dbo.TestPaperPackage", new[] { "PackageId" });
            DropIndex("dbo.ApplicationUserPackage", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserPackage", new[] { "PackageId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.Syllabus", new[] { "TestPaper_Id" });
            DropIndex("dbo.TestPaper", new[] { "MarkingSchemeId" });
            DropIndex("dbo.Topic", new[] { "ChapterId" });
            DropIndex("dbo.Solution", new[] { "QuestionId" });
            DropIndex("dbo.Option", new[] { "QuestionId" });
            DropIndex("dbo.ExamQuestion", new[] { "ExamId" });
            DropIndex("dbo.ExamQuestion", new[] { "QuestionId" });
            DropIndex("dbo.Subject", new[] { "ClassId" });
            DropIndex("dbo.Question", new[] { "DificultyLevelId" });
            DropIndex("dbo.Question", new[] { "QuestionTypeId" });
            DropIndex("dbo.Question", new[] { "TopicId" });
            DropIndex("dbo.Question", new[] { "ChapterId" });
            DropIndex("dbo.Question", new[] { "SubjectId" });
            DropIndex("dbo.Question", new[] { "ClassId" });
            DropIndex("dbo.Chapter", new[] { "SubjectId" });
            DropIndex("dbo.ApplicationUserRole", new[] { "RoleId" });
            DropIndex("dbo.ApplicationUserRole", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserLogin", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserClaim", new[] { "UserId" });
            DropIndex("dbo.ApplicationUser", "UserNameIndex");
            DropTable("dbo.Teacher");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Sponsor");
            DropTable("dbo.Employee");
            DropTable("dbo.Admin");
            DropTable("dbo.TestPaperQuestion");
            DropTable("dbo.TestPaperPackage");
            DropTable("dbo.ApplicationUserPackage");
            DropTable("dbo.Role");
            DropTable("dbo.Package");
            DropTable("dbo.Syllabus");
            DropTable("dbo.TestPaper");
            DropTable("dbo.MarkingScheme");
            DropTable("dbo.ImageResource");
            DropTable("dbo.Topic");
            DropTable("dbo.Solution");
            DropTable("dbo.QuestionType");
            DropTable("dbo.Option");
            DropTable("dbo.Exam");
            DropTable("dbo.ExamQuestion");
            DropTable("dbo.DificultyLevel");
            DropTable("dbo.Subject");
            DropTable("dbo.Class");
            DropTable("dbo.Question");
            DropTable("dbo.Chapter");
            DropTable("dbo.ApplicationUserRole");
            DropTable("dbo.ApplicationUserLogin");
            DropTable("dbo.ApplicationUserClaim");
            DropTable("dbo.ApplicationUser");
        }
    }
}
