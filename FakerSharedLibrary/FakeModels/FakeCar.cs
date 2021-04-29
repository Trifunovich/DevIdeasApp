using System.Collections.Generic;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  [Table("Cars")]
  public class FakeCar : FakeDataModelBase, ICarBase
  {
    public FakeCar()
    {
      GetUsers = new List<ICarUserBase>();
    }

    public List<ICarUserBase> GetUsers { get; set; }
  }
}
