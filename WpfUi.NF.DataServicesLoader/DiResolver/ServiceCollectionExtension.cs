using Autofac;
using DataServiceProvider.Services;
using LoggingLibrary.DiResolver;
using Microsoft.Extensions.DependencyInjection;
using Stylet;
using WpfUi.NF.DataServicesLoader.Factories;
using WpfUi.NF.DataServicesLoader.ViewModel;

namespace WpfUi.NF.DataServicesLoader.DiResolver
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddWpfServices (this IServiceCollection services)
    {
      services.AddDataAccessServiceInternals();
      services.AddTransient<IShellViewModel, ShellViewModel>();
      services.AddTransient<IDisplayDataControlViewModel, DisplayDataControlViewModel>();
      services.AddTransient<IDisplayDataControlViewModelFactory, DisplayDataControlViewModelFactory>();
      services.AddLoggingServices();
      return services;
    }

    public static ContainerBuilder AddWpfAutofacServices(this ContainerBuilder builder, string fileName)
    {
      builder.AddDataAccessServiceInternals();
      builder.RegisterType<ShellViewModel>().As<IShellViewModel>();
      builder.RegisterType<DisplayDataControlViewModel>().As<IDisplayDataControlViewModel>();
      builder.RegisterType<DisplayDataControlViewModelFactory>().As<IDisplayDataControlViewModelFactory>();
      builder.AddLoggingServices(fileName);
      return builder;
    }
  }
}