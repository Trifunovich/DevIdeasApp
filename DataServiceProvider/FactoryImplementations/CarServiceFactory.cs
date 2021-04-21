using Autofac;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarServiceFactory : AutoFacScopedServiceFactoryBase<ICarService>, ICarServiceFactory
  {
    public CarServiceFactory(IComponentContext serviceProvider) : base(serviceProvider)
    {
    }
  }
}