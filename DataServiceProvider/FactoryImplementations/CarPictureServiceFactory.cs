using System;
using Autofac;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarPictureServiceFactory : AutoFacScopedServiceFactoryBase<ICarPictureService>, ICarPictureServiceFactory
  {
    public CarPictureServiceFactory(IComponentContext serviceProvider) : base(serviceProvider)
    {
    }
  }
}