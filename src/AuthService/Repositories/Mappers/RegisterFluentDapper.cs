using Dapper.FluentMap;

namespace AuthService.Repositories.Mappers
{
    public class RegisterFluentDapper
    {
        public void Register()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(c => {
                c.AddMap(new UserMap());
            });
        }
    }
}
