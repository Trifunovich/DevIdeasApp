using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggerLibrary.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddLoggingInternals(this IServiceCollection services)
    {
      var provider = services.BuildServiceProvider();
      ConfigureLogger(provider);
      return services;
    }

    private static void ConfigureLogger(ServiceProvider provider)
    {
      var config = provider.GetService<IConfiguration>();
      Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config)
        .CreateLogger();

      var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
      loggerFactory.AddSerilog();
    }
 
  }
}