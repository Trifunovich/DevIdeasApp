using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DataServiceProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Stylet;
using WpfUi.NetFramework.DataServicesLoader.ViewModel;

namespace WpfUi.NetFramework.DataServicesLoader.TestBoot
{
  class Bootstrapper : Bootstrapper<ShellViewModel>
  {
    private ServiceProvider _serviceProvider;

    private ShellViewModel _rootViewModel;
    protected override ShellViewModel RootViewModel
    {
      get { return this._rootViewModel = this._rootViewModel ?? _serviceProvider.GetService<IShellViewModel>() as ShellViewModel; }
    }

    public static IServiceProvider ServiceProvider;

    /// <summary>
    /// Carries out default configuration of the IoC container. Override if you don't want to do this
    /// </summary>
    protected virtual void DefaultConfigureIoC(IServiceCollection services)
    {
      var viewManagerConfig = new ViewManagerConfig()
      {
        ViewFactory = Activator.CreateInstance,
        ViewAssemblies = new List<Assembly>() { this.GetType().Assembly }
      };



      services.AddSingleton<IViewManager>(new ViewManager(viewManagerConfig));
      services.AddTransient<MessageBoxView>();

      services.AddSingleton<IWindowManagerConfig>(this);
      services.AddSingleton<IWindowManager, WindowManager>();
      services.AddSingleton<IEventAggregator, EventAggregator>();
      services.AddTransient<IMessageBoxViewModel, MessageBoxViewModel>(); // Not singleton!

      // Also need a factory
      services.AddSingleton<Func<IMessageBoxViewModel>>(() => new MessageBoxViewModel());
      services.AddLogging()
        .AddDataAccessServiceInternals()
        .AddTransient<IShellViewModel, ShellViewModel>();
    }

    /// <summary>
    /// Override to add your own types to the IoC container.
    /// </summary>
    protected void ConfigureIoC(IServiceCollection services)
    {

    }

    public override object GetInstance(Type type)
    {
      return this._serviceProvider.GetRequiredService(type);
    }

    /// <summary>
    /// Called on application startup. This occur after this.Args has been assigned, but before the IoC container has been configured
    /// </summary>
    protected override void OnStart()
    {
      var services = new ServiceCollection();
      this.DefaultConfigureIoC(services);
      this.ConfigureIoC(services);
      this._serviceProvider = services.BuildServiceProvider();
      ServiceProvider = _serviceProvider;

      ConfigureLogger(_serviceProvider);

      var log = _serviceProvider.GetService<ILogger<App>>();
      log.LogInformation("App starting...");

      base.OnStart();
    }

    public override void Dispose()
    {
      base.Dispose();

      ScreenExtensions.TryDispose(this._rootViewModel);
      _serviceProvider?.Dispose();
    }

    private static void ConfigureLogger(ServiceProvider provider)
    {
      Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configure())
        .CreateLogger();

      var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
      loggerFactory.AddSerilog();
    }

    private static IConfiguration Configure()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      return builder.Build();
    }
  }
}