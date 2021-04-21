using System;

namespace MapperSharedLibrary.Models
{
  public interface IDto
  {
    string Label { get; set; }

    bool IsActive { get; set; }

    DateTime UpdatedOn { get; set; }

    DateTime CreatedOn { get; set; }
  }
}