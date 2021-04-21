namespace MapperSharedLibrary.Models
{
  public class CarPictureInputDto : DtoBase, ICarPictureDto, IInputDto
  {
    public string Picture { get; set; }

    public int CarId { get; set; }

    public int CarUserId { get; set; }
  }
}