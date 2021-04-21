using System;
using Microsoft.Extensions.DependencyInjection;
using WpfUi.NetFramework.DataServicesLoader.ViewModel;

namespace WpfUi.NetFramework.DataServicesLoader.Factories
{
  public interface IDisplayDataControlViewModelFactory
  {
    IDisplayDataControlViewModel CreateViewModel();
  }

  public class DisplayDataControlViewModelFactory : IDisplayDataControlViewModelFactory
  {
    private readonly IServiceProvider _container;


    public DisplayDataControlViewModelFactory(IServiceProvider container)
    {
      _container = container;
    }

    public IDisplayDataControlViewModel CreateViewModel()
    {
      return _container.GetRequiredService < IDisplayDataControlViewModel>();
    }
  }
}