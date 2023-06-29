using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;

namespace AuthService.Migration.Extensions
{
    public static class MigrationExtension
    {
        public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration) 
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t=> typeof(FluentMigrator.Migration) == t.BaseType)
                .Select(t=> t.Assembly).ToArray();

            services.AddFluentMigratorCore()
                .ConfigureRunner(cfg => cfg
                    .AddSqlServer()
                .WithGlobalConnectionString(configuration.GetConnectionString("AuthService"))
                    .ScanIn(assembly).For.Migrations()
                )
                .AddLogging(cfg => cfg.AddFluentMigratorConsole());

            return services;
        }

        public static IHost MigrateDatabase(this IHost host, Logger logger)
        {
            using (var scope = host.Services.CreateScope())
            {
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch (Exception e)
                {
                    logger.Error(e, e.Message);
                    throw;
                }
            }
            return host;
        }
    }
}
