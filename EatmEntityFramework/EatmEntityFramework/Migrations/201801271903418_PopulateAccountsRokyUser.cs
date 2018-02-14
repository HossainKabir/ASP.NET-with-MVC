namespace EatmEntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateAccountsRokyUser : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Accounts(AccountHolderName,CardNumber,Password) VALUES('Reyajul Roky',4,456)");

        }

        public override void Down()
        {
        }
    }
}
