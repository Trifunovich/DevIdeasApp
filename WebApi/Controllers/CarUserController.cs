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
  public class CarUserController : DataControllerBase<ICarUserServiceFactory, CarUserInputDto, CarUserOutputDto, CarUserInputDtoFile>
  {
    public CarUserController(ErrorHandlingHelper errorHelper, ILogger<CarUserController> logger, ICarUserServiceFactory factory) : base(errorHelper, logger, factory)
    {
    }
  }
}
