using System;

namespace MapperSharedLibrary.Models
{
  public interface ICarUserDto
  {
    string EMail { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string MiddleName { get; set; }
    DateTime DateOfBirth { get; set; }
  }
}