namespace E_Ticaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "mainPageUrl", c => c.String(maxLength: 255));
            DropColumn("dbo.Products", "InMainPage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "InMainPage", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "mainPageUrl");
        }
    }
}
