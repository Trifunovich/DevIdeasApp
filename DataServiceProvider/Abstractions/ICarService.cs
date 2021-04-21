using MapperSharedLibrary.Models;

namespace DataServiceProvider.Abstractions
{

  public interface ICarService : IDataAccessService<CarInputDto, CarOutputDto>
  {
    
  }
}