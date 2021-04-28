using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  [Table("Cars")]
  internal class Car : SqlDataModelBase, ICarBase, ISqlDataModelBase
  {
    public Car()
    {
      CarCarUsers = new HashSet<CarCarUser>();
    }

    public virtual ICollection<CarCarUser> CarCarUsers { get; }

    public List<ICarUserBase> GetUsers => CarCarUsers.Select(c => c.CarUser as ICarUserBase).ToList();
  }
}
