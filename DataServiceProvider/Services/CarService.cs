using AutoMapper;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataServiceProvider.Services
{
  internal class CarService : DataAccessService<ICarBase, CarInputDto, CarOutputDto>, ICarService
  {
    public CarService(IAdaptedDataRepository<ICarBase> repository, IMapper mapper, ILogger<CarService> logger) : base(repository, mapper, logger)
    {
    }
  }
}