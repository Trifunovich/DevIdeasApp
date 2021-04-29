using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  [DataAccess.Core.Attributes.Table("CarUsers")]
  internal class RavenCarUser : RavenDbDataModelBase, ICarUserBase
  {
    public string EMail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<RavenCar> Cars { get; set; }

    [NotMapped]
    public List<ICarBase> AllCars { get; set; }
  }
}