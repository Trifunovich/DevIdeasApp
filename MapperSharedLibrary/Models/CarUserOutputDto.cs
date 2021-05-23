using System;
using System.Collections.Generic;

namespace MapperSharedLibrary.Models
{
  public class CarUserOutputDto : DtoBase, ICarUserDto, IOutputDto
  {
    public string EMail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<CarOutputDto> AllCars { get; set; }

    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    
  }
}