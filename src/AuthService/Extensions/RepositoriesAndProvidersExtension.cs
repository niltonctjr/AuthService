using AuthService.Repositories;
using AuthService.Repositories.Interface;

namespace AuthService.Extensions
{
    public static class RepositoriesAndProvidersExtension
    {
        public static IServiceCollection AddRepositoriesAndProviders(this IServiceCollection services)
        {
            #region providers


            #endregion

            #region Repositories

            services.AddTransient<LazyLoadingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
