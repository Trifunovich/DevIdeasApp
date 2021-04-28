using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarBase : DataModelBase, ICarBase
  {
    public abstract List<ICarUserBase> GetUsers { get; }
  }
}
