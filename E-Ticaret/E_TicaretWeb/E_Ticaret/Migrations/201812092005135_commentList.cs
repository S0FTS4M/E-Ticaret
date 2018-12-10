namespace E_Ticaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentList : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Comments", "productId");
            AddForeignKey("dbo.Comments", "productId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "comment", c => c.String());
            DropForeignKey("dbo.Comments", "productId", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "productId" });
        }
    }
}
