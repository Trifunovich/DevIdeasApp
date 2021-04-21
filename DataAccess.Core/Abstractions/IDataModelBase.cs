using System;

namespace DataAccess.Core.Abstractions
{
  public interface IDataModelBase
  {
    string GetId { get; }
    string Label { get; set; }
    bool IsActive { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime UpdatedOn { get; set; }
  }
}