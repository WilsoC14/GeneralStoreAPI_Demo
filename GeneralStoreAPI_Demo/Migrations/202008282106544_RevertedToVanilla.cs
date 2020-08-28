namespace GeneralStoreAPI_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertedToVanilla : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "SalesTax");
            DropColumn("dbo.Transactions", "DateOfTransaction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "DateOfTransaction", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "SalesTax", c => c.Double(nullable: false));
        }
    }
}
