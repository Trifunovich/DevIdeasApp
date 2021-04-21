using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  [Table("Cars")]
  internal class SqlCar : SqlDataModelBase, ICarBase, ISqlDataModelBase
  {
 
  }
}
