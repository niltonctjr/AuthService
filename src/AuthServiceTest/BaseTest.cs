﻿using AuthService.Repositories.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace AuthServiceTest.UseCase
{
    public abstract class BaseTest
    {
        protected IConfiguration _config { get; private set; }
        [SetUp]
        public virtual void Setup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _config = builder.Build();

            var services = new ServiceCollection();
            services.AddScoped<RegisterFluentDapper>();
            services.AddLogging(loggingBuilder => loggingBuilder.AddNLog("nlog.config"));
            var provider = services.BuildServiceProvider();


            var fluentMapper = provider.GetService<RegisterFluentDapper>();
            if (fluentMapper != null) fluentMapper.Register();
        }
    }
}
