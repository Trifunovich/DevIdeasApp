using System.IO;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WpfUi.NF.DataServicesLoader.ViewModel;

namespace WpfUi.NF.DataServicesLoader
{
  public class Bootstrapper : AutofacBootstrapper<ShellViewModel>
  {
    /// <summary>
    /// Override to add your own types to the IoC container.
    /// </summary>
    protected override void ConfigureIoC(ContainerBuilder builder)
    {
      base.ConfigureIoC(builder);
      DiResolver.ServiceCollectionExtension.AddWpfAutofacServices(builder, "appsettings.json");
    }
  }
}