using AuthService.Repositories.Interface;
using AuthService.Repositories.Migrations;
using AuthService.Repositories;
using AuthService.UseCases;

namespace AuthService.Extensions
{
    public static class UseCaseExtension
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            #region Login
            services.AddTransient<SignUpUsecase>();
            #endregion

            return services;
        }
    }
}
