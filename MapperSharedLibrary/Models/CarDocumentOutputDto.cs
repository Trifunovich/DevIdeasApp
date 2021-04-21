namespace MapperSharedLibrary.Models
{
  public class CarDocumentOutputDto : DtoBase, ICarDocumentDto, IOutputDto
  {
    public double Mileage { get; set; }
    public DocumentType TypeOfDoc { get; set; }
  }
}