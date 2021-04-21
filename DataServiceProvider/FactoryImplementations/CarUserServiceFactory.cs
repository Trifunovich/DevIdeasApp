using System;
using Autofac;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarUserServiceFactory : AutoFacScopedServiceFactoryBase<ICarUserService>, ICarUserServiceFactory
  {
    public CarUserServiceFactory(IComponentContext serviceProvider) : base(serviceProvider)
    {

    }
  }
}