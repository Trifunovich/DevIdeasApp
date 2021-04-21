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
  public class CarController : DataControllerBase<ICarServiceFactory, CarInputDto, CarOutputDto, CarInputDtoFile>
  {
    public CarController(ErrorHandlingHelper errorHelper, ILogger<CarController> logger, ICarServiceFactory factory) : base(errorHelper, logger, factory)
    {
    }
  }
}
