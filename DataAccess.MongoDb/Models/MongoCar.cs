using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  [Table("Cars")]
  internal class MongoCar : MongoDbDataModelBase, ICarBase
  {
 
  }
}
