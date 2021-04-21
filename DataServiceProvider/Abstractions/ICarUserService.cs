using MapperSharedLibrary.Models;

namespace DataServiceProvider.Abstractions
{
  public interface ICarUserService : IDataAccessService<CarUserInputDto, CarUserOutputDto>
  {
  
  }
}