namespace EatmEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BalanceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Balance");
        }
    }
}
