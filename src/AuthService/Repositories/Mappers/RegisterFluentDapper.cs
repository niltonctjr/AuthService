using AuthService.Repositories.Customs.Dommel;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dommel;

namespace AuthService.Repositories.Mappers
{
    public class RegisterFluentDapper
    {
        public void Register()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(c => {
                c.AddMap(new UserMap());
                c.ForDommel();
            });

            DommelMapper.LogReceived = msg =>
            {
                Console.WriteLine(msg);
            };
            DommelMapper.SetPropertyResolver(new CustomPropertyResolver());
            DommelMapper.SetTableNameResolver(new Dapper.FluentMap.Dommel.Resolvers.DommelTableNameResolver());
            DommelMapper.SetColumnNameResolver(new Dapper.FluentMap.Dommel.Resolvers.DommelColumnNameResolver());
            DommelMapper.SetKeyPropertyResolver(new Dapper.FluentMap.Dommel.Resolvers.DommelKeyPropertyResolver());
        }
    }
}
