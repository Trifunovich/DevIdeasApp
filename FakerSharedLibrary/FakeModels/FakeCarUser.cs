using System;
using System.Collections.Generic;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  [Table("CarUsers")]
  public class FakeCarUser : FakeDataModelBase, ICarUserBase
  {
    public string EMail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<ICarBase> Cars { get; set; }
  }
}