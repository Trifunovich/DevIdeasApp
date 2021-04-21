using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public interface ICarPictureBase : IDataModelBase
  {
    string Picture { get; set; }

    int CarId { get; set; }

    int CarUserId { get; set; }
  }
}