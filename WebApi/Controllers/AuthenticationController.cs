using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationLibrary.Authentication;
using AuthenticationLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthenticationController : ApiControllerBase
  {
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService, ErrorHandlingHelper errorHelper) : base(errorHelper)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("[controller]/login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
      return await RunStandardErrorHandling(_authenticationService.TryToLogin(model));
    }

    [HttpPost]
    [Route("[controller]/refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
      return await RunStandardErrorHandling(_authenticationService.TryToRefresh(refreshToken));
    }

    [HttpPost]
    [Route("[controller]/register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
      return await Registration(model, false);
    }

    [HttpPost]
    [Route("[controller]/register/admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
      return await Registration(model, true);
    }
    
    [HttpDelete]
    [Authorize]
    [Route("[controller]/logout")]
    public async Task<IActionResult> Logout()
    {
      var id = HttpContext.User.FindFirstValue("id");
      return await RunStandardErrorHandling(_authenticationService.LogOut(id));
    }

    private async Task<IActionResult> Registration(RegisterModel model, bool isAdmin)
    {
      return await RunStandardErrorHandling(isAdmin ? _authenticationService.RegisterAdmin(model) : _authenticationService.Register(model));
    }
  }
}
