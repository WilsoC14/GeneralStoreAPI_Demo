namespace GeneralStoreAPI_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImprovedTransaction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            AddColumn("dbo.Transactions", "UpChargePercentage", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "SalesTax", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Customer_Id", c => c.Int());
            DropColumn("dbo.Transactions", "SalesTax");
            DropColumn("dbo.Transactions", "UpChargePercentage");
            CreateIndex("dbo.Products", "Customer_Id");
            AddForeignKey("dbo.Products", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
