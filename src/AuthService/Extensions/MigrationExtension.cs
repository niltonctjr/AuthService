using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace AuthService.Extensions
{
    public static class MigrationExtension
    {
        public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration) 
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t=> typeof(Migration) == t.BaseType)
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

        public static IHost MigrateDatabase(this IHost host)
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
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return host;
        }
    }
}
