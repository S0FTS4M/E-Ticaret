namespace E_Ticaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "comment");
        }
    }
}
