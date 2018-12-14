namespace E_Ticaret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerAccount : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "CustomerAccount");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CustomerAccount", newName: "AspNetUsers");
        }
    }
}
