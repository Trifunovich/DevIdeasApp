using AutoMapper;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataServiceProvider.Services
{
  internal class CarPictureService : DataAccessService<ICarPictureBase, CarPictureInputDto, CarPictureOutputDto>, ICarPictureService
  {
    public CarPictureService(IAdaptedDataRepository<ICarPictureBase> repository, IMapper mapper, ILogger<CarPictureService> logger) : base(repository, mapper, logger)
    {
    }
  }
}