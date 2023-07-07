using AuthService.Providers.Mail;
using AuthService.Providers.Mail.MailTrap;
using AuthService.Repositories;
using AuthService.Repositories.Interface;

namespace AuthService.Extensions.InjectDependencies
{
    public static class RepositoriesAndProvidersExtension
    {
        public static IServiceCollection AddRepositoriesAndProviders(this IServiceCollection services)
        {
            #region providers
            services.AddTransient<IMailProvider, MailTrapProvider>();

            #endregion

            #region Repositories

            services.AddTransient<LazyLoadingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion

            return services;
        }
    }
}
