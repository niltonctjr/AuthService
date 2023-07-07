using AuthService.Domain.Models;
using AuthService.Domain.Utils.Cryptography;

namespace AuthService.Repositories.Migrations
{
    [FluentMigrator.Migration(20230703)]
    public class Migration_20230703_EmailValid : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Column("is_valid").FromTable("users");               
        }

        public override void Up()
        {
            Alter.Table("users")
                .AddColumn("is_valid").AsBoolean().Nullable();
        }

    }
}
