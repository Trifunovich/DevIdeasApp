using System;
using Autofac;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarDocumentHistoryServiceFactory : AutoFacScopedServiceFactoryBase<ICarDocumentHistoryService>, ICarDocumentHistoryServiceFactory
  {
    public CarDocumentHistoryServiceFactory(IComponentContext serviceProvider) : base(serviceProvider)
    {
    }
  }
}