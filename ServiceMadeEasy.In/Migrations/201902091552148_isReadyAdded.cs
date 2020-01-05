namespace ServiceMadeEasy.In.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isReadyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestPaper", "IsReady", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestPaper", "IsReady");
        }
    }
}
