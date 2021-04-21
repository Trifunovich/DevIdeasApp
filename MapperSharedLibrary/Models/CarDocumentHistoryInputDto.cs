﻿using System.Collections.Generic;

namespace MapperSharedLibrary.Models
{
  public class CarDocumentHistoryInputDto : DtoBase, ICarDocumentHistoryDto, IInputDto
  {
    public int CarId { get; set; }
    public List<ICarDocumentDto> Docs { get; set; }
  }
}