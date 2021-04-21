using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Models;
using DataServiceProvider.Abstractions;
using DataServiceProvider.FactoryImplementations;
using MapperSharedLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedCodeLibrary;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
  [Authorize]
  public class DataControllerBase<TFactory, TInputDto, TOutputDto, TInputDtoFile>
    : ApiControllerBase
    where TFactory : IScopedServiceFactoryBase<IDataAccessService<TInputDto, TOutputDto>>
    where TInputDto : IInputDto
    where TOutputDto : IOutputDto
    where TInputDtoFile : IInputDtoFile, TInputDto
  {

    private readonly ILogger<DataControllerBase<TFactory, TInputDto, TOutputDto, TInputDtoFile>> _logger;
    private readonly TFactory _factory;

    public DataControllerBase(ErrorHandlingHelper errorHelper, ILogger<DataControllerBase<TFactory, TInputDto, TOutputDto, TInputDtoFile>> logger, TFactory factory) : base(errorHelper)
    {
      _logger = logger;
      _factory = factory;
    }

    /// <summary>
    /// Tries to get available objects.
    /// </summary>
    /// <response code="200">All available objects retrieved.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet, ResponseCache(CacheProfileName = "default")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(500)]
    public virtual async Task<IActionResult> Get()
    {
      Func<Task<ResponseModel>> func = async () =>
      {
        using var service = _factory.CreateService();
        var response = await service.GetAll();
        _logger.LogInformation("Get got {count} {type}", response.Count(), typeof(TOutputDto).Name);
        return new ResponseModel(ResponseType.Successful) { ResponseData = response };
      };

      return await RunStandardComplexCode(func);
    }

    /// <summary>
    /// Tries to create a new instance.
    /// </summary>
    /// <param name="input">Instance of <see cref="TInputDto"/>.</param>
    /// <response code="200">Instance created.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(int), 201)]
    [ProducesResponseType(500)]
    public virtual async Task<IActionResult> Post([FromBody] TInputDto input)
    {
      Func<Task<ResponseModel>> func = async () =>
      {
        if (input is null)
        {
          return null;
        }

        using var service = _factory.CreateService();
        var response = await service.Add(input);
        if (response is not null)
        {
          _logger.LogInformation("Created {count} input", response.Value);
          await service.SaveChanges();
          return new ResponseModel(ResponseType.Successful) { ResponseData = response }; 
        }

        return new ResponseModel(ResponseType.OperationFailed);
      };

      return await RunStandardComplexCode(func);
    }

    /// <summary>
    /// Tries to create a new instance from a file.
    /// </summary>
    /// <param name="input">Instance of <see cref="IInputDtoFile"/>.</param>
    /// <response code="200">Instance created.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("content")]
    [ProducesResponseType(typeof(int), 201)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostFile([FromBody] TInputDtoFile input)
    {
      if (input is null)
      {
        return NoContent();
      }

      await using (var memoryStream = new MemoryStream())
      {
        await input.File.OpenReadStream().CopyToAsync(memoryStream);
        var fileName = Guid.NewGuid().ToString("N");
        var path = Path.Combine(Path.GetTempPath(), fileName);
        await System.IO.File.WriteAllBytesAsync(path, memoryStream.ToArray());
      }

      return await Post(input);
    }

    /// <summary>
    /// Tests for errors
    /// </summary>
    /// <param name="input"></param>
    /// <returns>The service that called the error</returns>
    [HttpPost("throw")]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Throw([FromBody] TInputDto input)
    {
      Func<Task<ResponseModel>> func = async () =>
      {
        _ = input ?? throw new ArgumentNullException(nameof(input));
        using var service = _factory.CreateService();
        await service.ThrowError(input.ToString());
        return new ResponseModel(ResponseType.Successful){ResponseData = service};
      };

      return await RunStandardComplexCode(func);
    }

    /// <summary>
    /// Tries to update the input.
    /// </summary>
    /// <param name="input">Instance of <see cref="TInputDto"/> that holds values that we want updated.</param>
    /// <response code="200">Instance updated successfully.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Patch([FromBody] TInputDto input)
    {
      Func<Task<ResponseModel>> func = async () =>
      {
        using var service = _factory.CreateService();
        var response = await service.UpdateRecord(input);

        switch (response)
        {
          case null:
            return new ResponseModel(ResponseType.OperationFailed);
          default:
            await service.SaveChanges();
            return new ResponseModel(ResponseType.Successful) { ResponseData = response };
        }
      };

      return await RunStandardComplexCode(func);
    }

    /// <summary>
    /// Tries to retrieve instances using paging.
    /// </summary>
    /// <param name="page">Number of page.</param>
    /// <param name="pageSize">Number of elements in a page.</param>
    /// <param name="offset">The difference from the counted position.</param>
    /// <response code="200">Instances successfully retrieved.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{page:int:min(1)}/{pageSize:int:min(1)}/{offset:int:min(-0)}", Name = "[Controller]/byPage")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Get(int page, int pageSize = 10, int offset = 0)
    {
      Func<Task<ResponseModel>> func = async () =>
      {
        using var service = _factory.CreateService();
        var response = await service.GetPage(new PagingParameters(page, pageSize, offset), true);

        switch (response)
        {
          case null:
            return new ResponseModel(ResponseType.OperationFailed);
          default:
            await service.SaveChanges();
            return new ResponseModel(ResponseType.Successful) { ResponseData = response };
        }
      };
      return await RunStandardComplexCode(func);
    }

    /// <summary>
    /// Tries to retrieve specified instance.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <response code="200">Instance successfully retrieved.</response>
    /// <response code="404">Specified input user doesn't exist.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id:int:min(1)}", Name = "[Controller]/getById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(string id)
    {
      using var service = _factory.CreateService();
      var response = await service.GetById(id);

      if (response == null)
        return NotFound(id);

      await service.SaveChanges();
      return Ok(response);
    }

    /// <summary>
    /// Tires to delete specified input.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <response code="200">Instance successfully.</response>
    /// <response code="500">Internal server error.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Delete(string id)
    {
      using var service = _factory.CreateService();
      var response = await service.DeleteById(id);

      if (response == null)
        return NotFound(id);

      await service.SaveChanges();
      return Ok(response);
    }
  }
}