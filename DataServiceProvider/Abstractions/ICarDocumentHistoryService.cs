using MapperSharedLibrary.Models;

namespace DataServiceProvider.Abstractions
{
  public interface ICarDocumentHistoryService : IDataAccessService<CarDocumentHistoryInputDto, CarDocumentHistoryOutputDto>
  {

  }
}