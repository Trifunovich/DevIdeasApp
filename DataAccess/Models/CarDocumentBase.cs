using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarDocumentBase :  DataModelBase, ICarDocumentBase
  {
     public double Mileage { get; set; }

     public DocumentType TypeOfDoc { get; set; }
  }
}