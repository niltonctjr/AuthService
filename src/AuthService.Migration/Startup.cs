using AuthService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Migration
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services,
            IConfiguration configuration)
        {
            Console.WriteLine("Configurando recursos...");

            services.AddLogging(configure => configure.AddConsole());
            services.AddLogging(loggingBuilder => loggingBuilder.AddNLog("nlog.config"));

            services.AddMigration(configuration);

            services.AddTransient<ConsoleApp>();
        }
    }
}
