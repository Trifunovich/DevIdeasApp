namespace MapperSharedLibrary.Models
{
  public class CarDocumentInputDto : DtoBase, ICarDocumentDto, IInputDto
  {
    public double Mileage { get; set; }
    public DocumentType TypeOfDoc { get; set; }
  }
}