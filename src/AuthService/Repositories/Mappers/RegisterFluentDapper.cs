using AuthService.Repositories.Customs.Dommel;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dommel;

namespace AuthService.Repositories.Mappers
{
    public class RegisterFluentDapper
    {
        private readonly ILogger<RegisterFluentDapper> _logger;
        public RegisterFluentDapper(ILogger<RegisterFluentDapper> logger)
        {
            _logger = logger;
        }

        public void Register()
        {
            FluentMapper.EntityMaps.Clear();
            FluentMapper.Initialize(c => {
                c.AddMap(new UserMap());
                c.ForDommel();
            });

            DommelMapper.LogReceived = msg => _logger.LogInformation(msg);
            DommelMapperCustom.KeyNotIdentity = true;
            DommelMapper.SetPropertyResolver(new CustomPropertyResolver());
        }
    }
}
