using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  internal class MongoCarDocumentHistory : MongoDbDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }
    public List<ICarDocumentBase> Docs { get; set; }
  }
}