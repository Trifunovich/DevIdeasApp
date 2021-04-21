using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public enum DocumentType
  {
    Registration,
    TechnicalCheckUp
  }

  public interface ICarDocumentBase : IDataModelBase
  {
     double Mileage { get; set; }

     DocumentType TypeOfDoc { get; set; }
  }
}