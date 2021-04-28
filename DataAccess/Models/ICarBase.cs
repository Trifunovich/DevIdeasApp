using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public interface  ICarBase : IDataModelBase
  {
    List<ICarUserBase> GetUsers { get; }
  }
}
