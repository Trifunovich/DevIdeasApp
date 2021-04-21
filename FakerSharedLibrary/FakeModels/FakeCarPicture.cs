using DataAccess.Core.Attributes;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  [Table("CarPictures")]
  public class FakeCarPicture : FakeDataModelBase, ICarPictureBase
  {
    public string Picture { get; set; }

    public int CarId { get; set; }

    public int CarUserId { get; set; }
  }
}