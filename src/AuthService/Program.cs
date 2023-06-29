using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using AuthService.Migration.Extensions;
using AuthService.Extensions.InjectDependencies;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSetting(builder.Configuration)
        .AddJWTBearer(builder.Configuration)
        .AddMigration(builder.Configuration)
        .AddHealth(builder.Configuration)
        .AddEndpointsApiExplorer()
        .AddControllers();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthCheck", Version = "v1" });
    });

    builder.Host.ConfigureLogging(logging => {
        logging.ClearProviders();
        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        logging.AddConsole();
    }).UseNLog();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger()
           .UseSwaggerUI();
    }

    app.UseHttpsRedirection()
       .UseAuthorization()
       .UseHealth();

    app.MapControllers();
    app.MigrateDatabase(logger);

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
