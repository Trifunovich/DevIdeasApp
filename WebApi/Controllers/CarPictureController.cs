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
  public class CarPictureController : DataControllerBase<ICarPictureServiceFactory, CarPictureInputDto, CarPictureOutputDto, CarPictureInputDtoFile>
  {
    public CarPictureController(ErrorHandlingHelper errorHelper, ILogger<CarPictureController> logger, ICarPictureServiceFactory factory) : base(errorHelper, logger, factory)
    {
    }
  }
}
