using AuthService.Models;
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
                .WithColumn("created_by_id").AsDateTime().NotNullable()
                .WithColumn("modified_by_id").AsDateTime().Nullable()
                .WithColumn("email").AsDateTime().NotNullable()
                .WithColumn("password").AsDateTime().Nullable();

            Create.ForeignKey("FK_User_Modify")
                .FromTable("users").ForeignColumn("modified_by_id")
                .ToTable("Users").PrimaryColumn("id");

            CreateAdminUser();

            Create.ForeignKey("FK_User_Create")
                .FromTable("users").ForeignColumn("created_by_id")
                .ToTable("Users").PrimaryColumn("id");

        }

        private void CreateAdminUser()
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes("senha123"));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }            
            var uidProvider = new UniqueIdentifyProvider();

            var admin = new UserModel() {
                Email = "admin@authservice.com",
                Password = sBuilder.ToString(),                
            };            

            Insert.IntoTable("users").Row(new
            {
                id = admin.Id,
                state = admin.State,
                created_at = admin.CreatedAt,
                created_by_id = admin.Id,
                email = admin.Email,
                password = admin.Password,
            });
        }
    }
}
