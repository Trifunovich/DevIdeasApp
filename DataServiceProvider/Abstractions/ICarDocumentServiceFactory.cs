using DataServiceProvider.FactoryImplementations;

namespace DataServiceProvider.Abstractions
{
  public interface ICarDocumentServiceFactory : IServiceFactoryBase, IScopedServiceFactoryBase<ICarDocumentService>
  {
  }
}