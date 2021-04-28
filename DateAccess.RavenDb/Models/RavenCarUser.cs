using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  [Table("CarUsers")]
  internal class RavenCarUser : RavenDbDataModelBase, ICarUserBase
  {
    public string EMail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<RavenCar> Cars { get; set; }

    public List<ICarBase> GetCars => Cars.OfType<ICarBase>().ToList();
  }
}