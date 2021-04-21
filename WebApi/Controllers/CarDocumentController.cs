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
  public class CarDocumentController : DataControllerBase<ICarDocumentServiceFactory, CarDocumentInputDto, CarDocumentOutputDto, CarDocumentInputDtoFile>
  {
    public CarDocumentController(ErrorHandlingHelper errorHelper, ILogger<CarDocumentController> logger, ICarDocumentServiceFactory factory) : base(errorHelper, logger, factory)
    {
    }
  }
}