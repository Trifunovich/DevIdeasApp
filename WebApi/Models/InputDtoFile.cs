using MapperSharedLibrary.Models;
using Microsoft.AspNetCore.Http;

namespace WebApi.Models
{
  public interface IInputDtoFile : IInputDto
  {
    public IFormFile File { get; set; }
  }

  public class CarUserInputDtoFile : CarUserInputDto, IInputDtoFile
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }

  public class CarInputDtoFile : CarInputDto, IInputDtoFile
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }

  public class CarPictureInputDtoFile : CarPictureInputDto, IInputDtoFile
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }

  public class CarDocumentInputDtoFile : CarDocumentInputDto, IInputDtoFile
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }

  public class CarDocumentHistoryInputDtoFile : CarDocumentHistoryInputDto, IInputDtoFile
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }
}