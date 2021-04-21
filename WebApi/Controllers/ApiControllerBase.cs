using System;
using System.Threading.Tasks;
using AuthenticationLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers
{
  public class ApiControllerBase : ControllerBase
  {
    protected readonly ErrorHandlingHelper ErrorHelper;

    public ApiControllerBase(ErrorHandlingHelper errorHelper)
    {
      ErrorHelper = errorHelper;
    }

    protected ObjectResult ConvertResponseModelTypeToStatusCode(ResponseModel result)
    {
      int statusCode = result.ResponseType switch
      {
        ResponseType.UserCreationFailed => StatusCodes.Status500InternalServerError,
        ResponseType.UserExists => StatusCodes.Status500InternalServerError,
        ResponseType.LoggedOut => StatusCodes.Status200OK,
        ResponseType.UserSuccessfullyCreated => StatusCodes.Status200OK,
        ResponseType.UserDoesntExist => StatusCodes.Status410Gone,
        ResponseType.UnableToAuthenticate => StatusCodes.Status500InternalServerError,
        ResponseType.OperationFailed => StatusCodes.Status500InternalServerError,
        ResponseType.CredentialsMissing => StatusCodes.Status422UnprocessableEntity,
        ResponseType.Successful => StatusCodes.Status200OK,
        _ => throw new ArgumentOutOfRangeException()
      };

      return StatusCode(statusCode, result);
    }
    
    protected async  Task<IActionResult> RunStandardErrorHandling(Task<ResponseModel> task)
    {
      var result = await ErrorHelper.HandleAsync(task);
      return result is null ? BadRequest() : ConvertResponseModelTypeToStatusCode(result);
    }

    protected async Task<IActionResult> RunStandardComplexCode(Func<Task<ResponseModel>> code)
    {
      var result = await ErrorHelper.RunStandardComplexCode(code);
      return result is null ? BadRequest() : ConvertResponseModelTypeToStatusCode(result);
    }

  }
}