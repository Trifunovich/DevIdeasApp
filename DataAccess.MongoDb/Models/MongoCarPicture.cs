using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  [Table("CarPictures")]
  internal class MongoCarPicture : MongoDbDataModelBase, ICarPictureBase
  {
    public string Picture { get; set; }

    public int CarId { get; set; }

    public int CarUserId { get; set; }
  }
}