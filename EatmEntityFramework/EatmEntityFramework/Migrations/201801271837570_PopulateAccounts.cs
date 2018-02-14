namespace EatmEntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateAccounts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Accounts(AccountHolderName,CardNumber,Password) VALUES('Hossain Kabir',1,123)");
            Sql("INSERT INTO Accounts(AccountHolderName,CardNumber,Password) VALUES('Morsalin Munna',2,234)");
            Sql("INSERT INTO Accounts(AccountHolderName,CardNumber,Password) VALUES('Faisal Porag',3,345)");


        }

        public override void Down()
        {
        }
    }
}
