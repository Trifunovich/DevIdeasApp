using AutoMapper;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataServiceProvider.Services
{
  internal class CarDocumentHistoryService : DataAccessService<ICarDocumentHistoryBase, CarDocumentHistoryInputDto, CarDocumentHistoryOutputDto>, ICarDocumentHistoryService
  {
    public CarDocumentHistoryService(IAdaptedDataRepository<ICarDocumentHistoryBase> repository, IMapper mapper, ILogger<CarDocumentHistoryService> logger) : base(repository, mapper, logger)
    {
    }
  }
}