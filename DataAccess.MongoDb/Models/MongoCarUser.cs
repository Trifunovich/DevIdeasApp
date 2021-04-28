using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Core.Attributes;
using DataAccess.Models;
using DataAccess.MongoDb.Abstractions;

namespace DataAccess.MongoDb.Models
{
  [Table("CarUsers")]
  internal class MongoCarUser : MongoDbDataModelBase, ICarUserBase
  {
    public string EMail { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<MongoCar> Cars { get; set; }
    public List<ICarBase> GetCars => Cars.OfType<ICarBase>().ToList();
  }
}