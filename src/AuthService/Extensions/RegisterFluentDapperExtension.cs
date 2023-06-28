using AuthService.Providers.Cryptography;
using AuthService.Providers.UniqueIdentify;
using AuthService.Repositories.Interface;
using AuthService.Repositories.Migrations;
using AuthService.Repositories;
using AuthService.UseCases;
using AuthService.Repositories.Mappers;

namespace AuthService.Extensions
{
    public static class RegisterFluentDapperExtension
    {
        public static void AddRegisterFluentDapper(this IServiceCollection services)
        {
            services.AddScoped<RegisterFluentDapper>();

            var provider = services.BuildServiceProvider();

            var fluentMapper = provider.GetService<RegisterFluentDapper>();
            if (fluentMapper != null) fluentMapper.Register();
        }
    }
}
