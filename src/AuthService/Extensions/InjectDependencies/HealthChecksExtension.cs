using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace AuthService.Extensions.InjectDependencies
{
    public static class HealthChecksExtension
    {
        public static IServiceCollection AddHealth(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddHealthChecks()
                .AddNpgSql($"{configuration.GetConnectionString("AuthService")}",
                    name: "DataBase AuthService", tags: new string[] { "db", "data", "sql" });
            // .AddSqlServer($"{configuration.GetConnectionString("AuthService")}",
            //     name: "DataBase AuthService", tags: new string[] { "db", "data", "sql" });

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(5);
                options.MaximumHistoryEntriesPerEndpoint(10);
                options.AddHealthCheckEndpoint("AuthService", "/health");
            })
            .AddInMemoryStorage(); //Aqui adicionamos o banco em memória

            return services;
        }

        public static IApplicationBuilder UseHealth(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => { options.UIPath = "/monitor"; });

            return app;
        }
    }
}
