using Dapper.FluentMap;
using static Org.BouncyCastle.Math.EC.ECCurve;

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
