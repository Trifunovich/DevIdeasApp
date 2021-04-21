using MapperSharedLibrary.Models;

namespace DataServiceProvider.Abstractions
{
  public interface ICarDocumentService : IDataAccessService<CarDocumentInputDto, CarDocumentOutputDto>
  {

  }
}