namespace E_Ticaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Amount = c.Int(nullable: false),
                        Image = c.String(maxLength: 255),
                        Date = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(maxLength: 255),
                        InMainPage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
