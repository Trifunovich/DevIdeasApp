using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApiTemplate.Models
{
  public class FooFile : Foo
  {
    /// <summary>
    /// Gets or set the file content.
    /// </summary>
    public IFormFile File { get; set; }
  }
}