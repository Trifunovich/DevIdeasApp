using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  internal class CarDocument : SqlDataModelBase, ICarDocumentBase
  {
     public double Mileage { get; set; }

     public DocumentType TypeOfDoc { get; set; }
  }
}