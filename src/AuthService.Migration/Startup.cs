using AuthService.Migration.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace AuthService.Migration
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services,
            IConfiguration configuration)
        {
            Console.WriteLine("Configurando recursos...");
            
            services.AddLogging(loggingBuilder => loggingBuilder.AddNLog("nlog.config"));

            services.AddMigration(configuration);

            services.AddTransient<ConsoleApp>();
        }
    }
}
