using System;
using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarUserBase : DataModelBase, ICarUserBase
  {
    public string EMail { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    protected abstract void AddCar(ICarBase doc);

    public abstract List<ICarBase> AllCars { get; set; }
  };
}