using AuthService.Repositories.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace AuthServiceTest.UseCase
{
    public abstract class BaseTest
    {
        protected IConfiguration _config { get; private set; }
        protected ServiceCollection _services { get; private set; }
        [SetUp]
        public virtual void Setup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
            _services = new ServiceCollection();
            var provider = LoadServices().BuildServiceProvider();

            var fluentMapper = provider.GetService<RegisterFluentDapper>();
            if (fluentMapper != null) fluentMapper.Register();
        }

        public virtual ServiceCollection LoadServices() 
        {
            _services.AddScoped<RegisterFluentDapper>();
            _services.AddLogging(loggingBuilder => loggingBuilder.AddNLog("nlog.config"));

            return _services;
        }
    }
}
