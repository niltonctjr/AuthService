using AuthService.Models;
using Dapper.FluentMap.Mapping;

namespace AuthService.Repositories.Mappers
{
    internal class UserMap : BaseMap<UserModel>
    {
        internal UserMap(): base()
        {            
            Map(e => e.Email).ToColumn("email");
            Map(e => e.Password).ToColumn("password");
        }
    }
}