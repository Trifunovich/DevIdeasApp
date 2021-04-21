using System;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarPictureServiceFactory : ScopedServiceFactoryBase<ICarPictureService>, ICarPictureServiceFactory
  {
    public CarPictureServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}