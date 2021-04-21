using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using WpfUi.NF.DataServicesLoader.ViewModel;

namespace WpfUi.NF.DataServicesLoader.Factories
{
  public interface IDisplayDataControlViewModelFactory
  {
    IDisplayDataControlViewModel CreateViewModel();
  }

  public class DisplayDataControlViewModelFactory : IDisplayDataControlViewModelFactory
  {
    private readonly IComponentContext _container;


    public DisplayDataControlViewModelFactory(IComponentContext container)
    {
      _container = container;
    }

    public IDisplayDataControlViewModel CreateViewModel()
    {
      return _container.Resolve<IDisplayDataControlViewModel>();
    }
  }
}