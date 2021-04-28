using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  [Table("Cars")]
  internal class RavenCar : RavenDbDataModelBase, ICarBase
  {
    public List<RavenCarUser> CarUsers { get; set; }

    public List<ICarUserBase> GetUsers => CarUsers.OfType<ICarUserBase>().ToList();
  }
}
