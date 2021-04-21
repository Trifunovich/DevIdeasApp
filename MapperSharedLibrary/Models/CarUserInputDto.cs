using System;
using System.Collections.Generic;

namespace MapperSharedLibrary.Models
{
  public class CarUserInputDto : DtoBase, ICarUserDto, IInputDto
  {
    public string EMail { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public List<CarInputDto> Cars { get; set; }
  }
}