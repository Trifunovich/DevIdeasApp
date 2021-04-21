using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarPictureBase : DataModelBase, ICarPictureBase
  {
    public string Picture { get; set; }

    public int CarId { get; set; }

    public int CarUserId { get; set; }
  }
}