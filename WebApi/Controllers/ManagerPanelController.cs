using System.Threading.Tasks;
using AuthenticationLibrary.Models;
using DataServiceProvider.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [Authorize(Roles = nameof(UserRole.Admin))]
  public class ManagerPanelController : ApiControllerBase
  {
    private readonly IManagerService _service;

    public ManagerPanelController(ErrorHandlingHelper errorHelper, IManagerService service) : base(errorHelper)
    {
      _service = service;
    }

    /// <summary>
    /// Tries to display raven browser for embedded database.
    /// </summary>
    /// <response code="200">Browser displayed.</response>
    /// <response code="500">Db offline or unavailable.</response>
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(417)]
    public async Task<IActionResult> GetRavenBrowser()
    {
      var result = await _service.DisplayEmbeddedRavenServer();
      return result ? Ok() :  StatusCode(StatusCodes.Status417ExpectationFailed, "Couldn't find the server");
    }
  }
}
