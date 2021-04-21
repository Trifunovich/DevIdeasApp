using System;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarServiceFactory : ScopedServiceFactoryBase<ICarService>, ICarServiceFactory
  {
    public CarServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }
  }
}