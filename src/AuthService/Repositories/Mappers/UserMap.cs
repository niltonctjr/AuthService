using AuthService.Domain.Models;
using Dapper.FluentMap.Mapping;

namespace AuthService.Repositories.Mappers
{
    internal class UserMap : BaseMap<UserModel>
    {
        internal UserMap(): base()
        {
            ToTable("users");            
            Map(e => e.Email).ToColumn("email");
            Map(e => e.Password).ToColumn("password");
        }
    }
}