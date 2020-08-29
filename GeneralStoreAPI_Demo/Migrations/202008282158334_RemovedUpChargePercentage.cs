namespace GeneralStoreAPI_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUpChargePercentage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "UpChargePercentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "UpChargePercentage", c => c.Double(nullable: false));
        }
    }
}
