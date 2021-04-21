using MapperSharedLibrary.Models;

namespace DataServiceProvider.Abstractions
{
  public interface ICarPictureService : IDataAccessService<CarPictureInputDto, CarPictureOutputDto>
  {
    
  }
}