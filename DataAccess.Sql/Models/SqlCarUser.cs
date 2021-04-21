using System;
using System.Collections.Generic;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  [Table("CarUsers")]
  internal class SqlCarUser : SqlDataModelBase, ICarUserBase
  {
    public string EMail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<ICarBase> Cars { get; set; }
  }
}