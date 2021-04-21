using DataAccess.Core.Attributes;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  [Table("Cars")]
  public class FakeCar : FakeDataModelBase, ICarBase
  {
 
  }
}
