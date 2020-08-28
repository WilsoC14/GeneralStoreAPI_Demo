namespace GeneralStoreAPI_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedProductAndCustomerLists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCustomers",
                c => new
                    {
                        Product_SKU = c.String(nullable: false, maxLength: 128),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_SKU, t.Customer_Id })
                .ForeignKey("dbo.Products", t => t.Product_SKU, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Product_SKU)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.ProductCustomers", "Product_SKU", "dbo.Products");
            DropIndex("dbo.ProductCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.ProductCustomers", new[] { "Product_SKU" });
            DropTable("dbo.ProductCustomers");
        }
    }
}
