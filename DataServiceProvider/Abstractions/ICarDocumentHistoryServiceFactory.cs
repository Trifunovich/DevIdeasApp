using DataServiceProvider.FactoryImplementations;

namespace DataServiceProvider.Abstractions
{
  public interface ICarDocumentHistoryServiceFactory : IServiceFactoryBase, IScopedServiceFactoryBase<ICarDocumentHistoryService>
  {
  }
}