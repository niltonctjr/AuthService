using AuthService.Repositories.Interface;
using AuthService.Repositories.Migrations;
using AuthService.Repositories;
using AuthService.UseCases;
using Microsoft.Extensions.Options;
using AuthService.Settings;
using AuthService.Providers.Mail.MailTrap;

namespace AuthService.Extensions.InjectDependencies
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddSetting(this IServiceCollection services, IConfiguration config)
        {
            services.GetSetting<AuthSetting>(config);
            services.GetSetting<MailTrapSetting>(config);

            return services;
        }

        public static void GetSetting<T>(this IServiceCollection services, IConfiguration config) where T : class =>
           services.Configure<T>(config.GetSection(typeof(T).Name));


        public static T GetSetting<T>(this IConfiguration config) where T : class
        {
            var setting = Activator.CreateInstance(typeof(T));
            if (setting == null)
                throw new Exception($"Não foi possivel criar a instancia tipo {typeof(T).FullName} para obter a sua configuração");

            var section = config.GetSection(typeof(T).Name);
            var configOption = new ConfigureFromConfigurationOptions<T>(section);
            configOption.Configure((T)setting);

            return (T)setting;
        }

        public static IOptions<T> GetSettingIOptions<T>(this IConfiguration config) where T : class
        {
            var setting = Activator.CreateInstance(typeof(T));
            if (setting == null)
                throw new Exception($"Não foi possivel criar a instancia tipo {typeof(T).FullName} para obter a sua configuração");

            var section = config.GetSection(typeof(T).Name);
            var configOption = new ConfigureFromConfigurationOptions<T>(section);
            configOption.Configure((T)setting);

            return Options.Create<T>((T)setting);
        }
    }
}

