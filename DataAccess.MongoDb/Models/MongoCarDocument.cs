using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  internal class MongoCarDocument : MongoDbDataModelBase, ICarDocumentBase
  {
     public double Mileage { get; set; }

     public DocumentType TypeOfDoc { get; set; }
  }
}