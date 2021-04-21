using System.Runtime.CompilerServices;
using AutoMapper;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("DataServiceProvider.Tests")]
namespace DataServiceProvider.Services
{
  class CarUserService : DataAccessService<ICarUserBase, CarUserInputDto, CarUserOutputDto>, ICarUserService
  {
    public CarUserService(IAdaptedDataRepository<ICarUserBase> repository, IMapper mapper, ILogger<CarUserService> logger) : base(repository, mapper, logger)
    {
    }
  }
}