
using System.IO;
using Autofac;
using LoggingLibrary.Enrichers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;
using Serilog.Filters;
using Serilog.Formatting.Compact;

namespace LoggingLibrary.DiResolver
{
  public static class ServiceCollectionExtension
  {
    private static IConfigurationRoot _currentConfig;

    public static IServiceCollection AddLoggingServices(this IServiceCollection services)
    {
      services.AddLogging();
      return services;
    }

    public static ContainerBuilder AddLoggingServices(this ContainerBuilder builder, string fileName)
    {
      builder.RegisterInstance(new LoggerFactory())
        .As<ILoggerFactory>();
      builder.ResolveLogger(fileName);
      return builder;
    }

    public static ContainerBuilder ResolveLogger(this ContainerBuilder builder, string fileName)
    {
      var filter = Matching.WithProperty("XProp");
      var config = new LoggerConfiguration().ReadFrom.Configuration(Configure(fileName))
        .WriteTo.Logger(l => l.Enrich.With<TxtLogFilePathEnricher>()
          .Filter.ByIncludingOnly(filter)
          .WriteTo.Map(LogFilePathEnricherBase.LogFilePathPropertyName,
            (logFilePath, wt) => wt.File($"{logFilePath}.txt"), sinkMapCountLimit: 1))
        .WriteTo.Logger(l => l.Enrich.With<JsonLogFilePathEnricher>()
          .Filter.ByIncludingOnly(filter)
        .WriteTo.Map(LogFilePathEnricherBase.LogFilePathPropertyName,
          (logFilePath, wt) => wt.File(new CompactJsonFormatter(), $"{logFilePath}.json"), sinkMapCountLimit: 1));

      builder.RegisterSerilog(config);

      return builder;
    }

    public static ServiceProvider ResolveLogger(this ServiceProvider provider, string fileName)
    {
      var config = new LoggerConfiguration().ReadFrom.Configuration(Configure(fileName));

      Log.Logger = config
        .CreateLogger();

      var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
      loggerFactory.AddSerilog();

      var a = provider.GetService<ILogger<LoggerWrapper>>();
      a.LogDebug("a");
      return provider;
    }

    private static IConfiguration Configure(string fileName)
    {
      var dir = Directory.GetCurrentDirectory();
      var builder = new ConfigurationBuilder()
        .SetBasePath(dir)
        .AddJsonFile(fileName);
      var built = builder.Build();
      _currentConfig = built;
      return built;
    }

    private static IConfiguration GetConfiguration => _currentConfig;

    public static void AddNewFileToTheCurrentLogger(string filePath)
    {
      filePath = System.IO.Path.ChangeExtension(filePath, null);
      LogFilePathEnricherBase.SetPath(filePath);
    }
  }
}