using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  [Table("CarPictures")]
  internal class CarPicture : SqlDataModelBase, ICarPictureBase
  {
    public string Picture { get; set; }

    public int CarId { get; set; }

    public int CarUserId { get; set; }
  }
}