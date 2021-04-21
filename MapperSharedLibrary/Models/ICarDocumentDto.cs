

namespace MapperSharedLibrary.Models
{
  public enum DocumentType
  {
    Registration,
    TechnicalCheckUp
  }

  public interface ICarDocumentDto
  {
    double Mileage { get; set; }

    DocumentType TypeOfDoc { get; set; }
  }
}