using DataServiceProvider.Abstractions;
using MapperSharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CarDocumentHistoryController : DataControllerBase<ICarDocumentHistoryServiceFactory, CarDocumentHistoryInputDto, CarDocumentHistoryOutputDto, CarDocumentHistoryInputDtoFile>
  {
    public CarDocumentHistoryController(ErrorHandlingHelper errorHelper, ILogger<CarDocumentHistoryController> logger, ICarDocumentHistoryServiceFactory factory) : base(errorHelper, logger, factory)
    {
    }
  }
}