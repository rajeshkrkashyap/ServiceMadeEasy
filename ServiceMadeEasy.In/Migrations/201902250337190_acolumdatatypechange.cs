namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acolumdatatypechange : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplicationUserPackage", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ApplicationUserPackage", "ApplicationUserId");
            RenameColumn(table: "dbo.ApplicationUserPackage", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.ApplicationUserPackage", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplicationUserPackage", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApplicationUserPackage", new[] { "ApplicationUserId" });
            AlterColumn("dbo.ApplicationUserPackage", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ApplicationUserPackage", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.ApplicationUserPackage", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ApplicationUserPackage", "ApplicationUser_Id");
        }
    }
}
