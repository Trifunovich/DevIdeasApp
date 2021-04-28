using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  [Table("Cars")]
  internal class MongoCar : MongoDbDataModelBase, ICarBase
  {
    public List<MongoCarUser> CarUsers { get; set; }

    public List<ICarUserBase> GetUsers => CarUsers.OfType<ICarUserBase>().ToList();
  }
}
