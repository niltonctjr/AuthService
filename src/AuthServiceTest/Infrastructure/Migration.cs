using AuthService.Migration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServiceTest.Infrastructure
{
    [Order(0)]
    public class Migration
    {
        ServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            var configuration = builder.Build();

            _services = new ServiceCollection();
            Startup.ConfigureServices(_services, configuration);
        }

        [Test]
        [Order(0)]
        public void Down()
        {
            _services
            .BuildServiceProvider()
            .GetService<ConsoleApp>()?
            .Run(new string[] { "-cdown", "-v0" });
        }

        [Test]
        [Order(1)]
        public void Up()
        {
            _services
            .BuildServiceProvider()
            .GetService<ConsoleApp>()?
            .Run(new string[] { "-cup" });
        }

    }
}