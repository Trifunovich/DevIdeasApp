using System;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarUserServiceFactory : ScopedServiceFactoryBase<ICarUserService>, ICarUserServiceFactory
  {
    public CarUserServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {

    }
  }
}