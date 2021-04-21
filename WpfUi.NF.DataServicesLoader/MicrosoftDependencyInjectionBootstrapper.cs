using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Stylet;

namespace WpfUi.NF.DataServicesLoader
{
  public class MicrosoftDependencyInjectionBootstrapper<TRootViewModel> : BootstrapperBase where TRootViewModel : IScreen
  {
    private ServiceProvider _serviceProvider;

    private TRootViewModel _rootViewModel;

    private IServiceCollection _collection;
    protected virtual TRootViewModel RootViewModel => _rootViewModel = (TRootViewModel)GetInstance(typeof(TRootViewModel));
    
    protected override void ConfigureBootstrapper()
    {
      var services = new ServiceCollection();
      DefaultConfigureIoC(services);
      ConfigureIoC(services);
      _collection = services;
    }

    /// <summary>
    /// Carries out default configuration of the IoC container. Override if you don't want to do this
    /// </summary>
    protected virtual void DefaultConfigureIoC(IServiceCollection services)
    {
      var viewManagerConfig = new ViewManagerConfig
      {
        ViewFactory = Activator.CreateInstance,
        ViewAssemblies = new List<Assembly> { GetType().Assembly }
      };

      services.AddSingleton<IViewManager>(new ViewManager(viewManagerConfig));
      services.AddTransient<MessageBoxView>();

      services.AddSingleton<IWindowManagerConfig>(this);
      services.AddSingleton<IWindowManager, WindowManager>();
      services.AddSingleton<IEventAggregator, EventAggregator>();
      services.AddTransient<IMessageBoxViewModel, MessageBoxViewModel>(); // Not singleton!
                                                                          
      // // Also need a factory
      services.AddSingleton<Func<IMessageBoxViewModel>>(() => new MessageBoxViewModel());
    }

    /// <summary>
    /// Override to add your own types to the IoC container.
    /// </summary>
    protected virtual void ConfigureIoC(IServiceCollection services) { }

    public override object GetInstance(Type type)
    {
      if (_serviceProvider == null)
      {
        _serviceProvider = _collection.BuildServiceProvider();
        //_serviceProvider.ResolveLogger("appsettings.json");
      }

      return _serviceProvider.GetRequiredService(type);
    }

    protected override void Launch()
    {
      base.DisplayRootView(RootViewModel);
    }

    public override void Dispose()
    {
      base.Dispose();

      ScreenExtensions.TryDispose(_rootViewModel);
      _serviceProvider?.Dispose();
    }
  }
}