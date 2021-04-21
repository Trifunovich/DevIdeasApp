namespace MapperSharedLibrary.Models
{
  public class CarPictureOutputDto : DtoBase, ICarPictureDto, IOutputDto
  {
    public string Picture { get; set; }
    public int CarId { get; set; }
    public int CarUserId { get; set; }
  }
}