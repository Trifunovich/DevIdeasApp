using System;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarDocumentHistoryServiceFactory : ScopedServiceFactoryBase<ICarDocumentHistoryService>, ICarDocumentHistoryServiceFactory
  {
    public CarDocumentHistoryServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}