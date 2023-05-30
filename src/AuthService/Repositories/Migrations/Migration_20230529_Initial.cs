using AuthService.Models;
using AuthService.Providers.Cryptography;
using AuthService.Providers.UniqueIdentify;
using FluentMigrator;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Repositories.Migrations
{
    [Migration(20230529)]
    public class Migration_20230529_Initial : Migration
    {
        public override void Down()
        {
            Delete.Table("users");
        }

        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("state").AsInt16().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("modified_at").AsDateTime().Nullable()
                .WithColumn("created_by_id").AsGuid().NotNullable()
                .WithColumn("modified_by_id").AsGuid().Nullable()
                .WithColumn("email").AsString(255).NotNullable()
                .WithColumn("password").AsString(255).Nullable();

            Create.ForeignKey("FK_User_Modify")
                .FromTable("users").ForeignColumn("modified_by_id")
                .ToTable("users").PrimaryColumn("id");

            CreateAdminUser();

            Create.ForeignKey("FK_User_Create")
                .FromTable("users").ForeignColumn("created_by_id")
                .ToTable("users").PrimaryColumn("id");

        }

        private void CreateAdminUser()
        {
            var cryp = new CryptographyProvider();
            var admin = new UserModel() {
                Email = "admin@authservice.com",
                Password = cryp.Encryp("senha123")
            };            

            Insert.IntoTable("users").Row(new
            {
                id = admin.Id,
                state = (Int16)admin.State,
                created_at = admin.CreatedAt,
                created_by_id = admin.Id,
                email = admin.Email,
                password = admin.Password,
            });
        }
    }
}
