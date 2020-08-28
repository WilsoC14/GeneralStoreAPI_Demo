namespace GeneralStoreAPI_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedListOfCustomersToListOfStringsInProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCustomers", "Product_SKU", "dbo.Products");
            DropForeignKey("dbo.ProductCustomers", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ProductCustomers", new[] { "Product_SKU" });
            DropIndex("dbo.ProductCustomers", new[] { "Customer_Id" });
            AddColumn("dbo.Products", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Products", "Customer_Id");
            AddForeignKey("dbo.Products", "Customer_Id", "dbo.Customers", "Id");
            DropTable("dbo.ProductCustomers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCustomers",
                c => new
                    {
                        Product_SKU = c.String(nullable: false, maxLength: 128),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_SKU, t.Customer_Id });
            
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            DropColumn("dbo.Products", "Customer_Id");
            CreateIndex("dbo.ProductCustomers", "Customer_Id");
            CreateIndex("dbo.ProductCustomers", "Product_SKU");
            AddForeignKey("dbo.ProductCustomers", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCustomers", "Product_SKU", "dbo.Products", "SKU", cascadeDelete: true);
        }
    }
}
