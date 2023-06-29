using AuthService.Providers.Cryptography;
using AuthService.Providers.UniqueIdentify;
using AuthService.Repositories;
using AuthService.Repositories.Interface;

namespace AuthService.Extensions
{
    public static class RepositoriesAndProvidersExtension
    {
        public static IServiceCollection AddRepositoriesAndProviders(this IServiceCollection services)
        {
            #region providers

            services.AddTransient<CryptographyProvider>();
            services.AddTransient<UniqueIdentifyProvider>();

            #endregion

            #region Repositories

            services.AddTransient<LazyLoadingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
