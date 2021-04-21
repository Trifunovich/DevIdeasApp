using AutoMapper;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataServiceProvider.Services
{
  internal class CarDocumentService : DataAccessService<ICarDocumentBase, CarDocumentInputDto, CarDocumentOutputDto>, ICarDocumentService
  {
    public CarDocumentService(IAdaptedDataRepository<ICarDocumentBase> repository, IMapper mapper, ILogger<CarDocumentService> logger) : base(repository, mapper, logger)
    {
    }
  }
}