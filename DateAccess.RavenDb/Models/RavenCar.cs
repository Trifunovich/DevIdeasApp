using DataAccess.Core.Attributes;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  [Table("Cars")]
  internal class RavenCar : RavenDbDataModelBase, ICarBase
  {
 
  }
}
