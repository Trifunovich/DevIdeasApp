using System;
using Autofac;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarDocumentServiceFactory : AutoFacScopedServiceFactoryBase<ICarDocumentService>, ICarDocumentServiceFactory
  {
    public CarDocumentServiceFactory(IComponentContext serviceProvider) : base(serviceProvider)
    {
    }
  }
}