using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  [Core.Attributes.Table("CarUsers")]
  internal class CarUser : SqlDataModelBase, ICarUserBase
  {
    public CarUser()
    {
      CarCarUsers = new HashSet<CarCarUser>();
    }

    [MaxLength(100, ErrorMessage = "Email too long")]
    public string EMail { get; set; }

    [MaxLength(100, ErrorMessage = "First name too long")]
    public string FirstName { get; set; }

    [MaxLength(100, ErrorMessage = "Last name too long")]
    public string LastName { get; set; }

    [MaxLength(100, ErrorMessage = "Middle name too long")]
    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<CarCarUser> CarCarUsers { get; }

    public List<ICarBase> GetCars => CarCarUsers.Select(c => c.Car as ICarBase).ToList();
  }
}