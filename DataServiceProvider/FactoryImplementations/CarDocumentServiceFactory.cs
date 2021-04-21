using System;
using DataServiceProvider.Abstractions;

namespace DataServiceProvider.FactoryImplementations
{
  internal class CarDocumentServiceFactory : ScopedServiceFactoryBase<ICarDocumentService>, ICarDocumentServiceFactory
  {
    public CarDocumentServiceFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}