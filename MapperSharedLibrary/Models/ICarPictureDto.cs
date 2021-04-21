namespace MapperSharedLibrary.Models
{
  public interface ICarPictureDto
  {
    string Picture { get; set; }

    int CarId { get; set; }

    int CarUserId { get; set; }
  }
}