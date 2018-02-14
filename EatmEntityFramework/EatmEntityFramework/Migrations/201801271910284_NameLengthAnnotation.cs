namespace EatmEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameLengthAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "AccountHolderName", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "AccountHolderName", c => c.String());
        }
    }
}
