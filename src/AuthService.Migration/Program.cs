// See https://aka.ms/new-console-template for more information

using AuthService.Migration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("**** Execucao de Migrations ****");

IConfigurationBuilder builder = new ConfigurationBuilder()    
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = builder.Build();

var services = new ServiceCollection();
Startup.ConfigureServices(services, configuration);

services
    .BuildServiceProvider()
    .GetService<ConsoleApp>()?
    .Run(args);