using System;
using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public interface ICarUserBase : IDataModelBase
  {
    string EMail { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string MiddleName { get; set; }

    DateTime DateOfBirth { get; set; }

    List<ICarBase> Cars { get; set; }
  }
}