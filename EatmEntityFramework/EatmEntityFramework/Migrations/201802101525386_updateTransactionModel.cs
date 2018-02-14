namespace EatmEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTransactionModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "AccountId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Transactions", "AccountId");
            AddForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts", "AccountId", cascadeDelete: true);
        }
    }
}
