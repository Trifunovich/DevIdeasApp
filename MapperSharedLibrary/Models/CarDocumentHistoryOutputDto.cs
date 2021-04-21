using System.Collections.Generic;

namespace MapperSharedLibrary.Models
{
  public class CarDocumentHistoryOutputDto : DtoBase, ICarDocumentHistoryDto, IOutputDto
  {
    public int CarId { get; set; }
    public List<ICarDocumentDto> Docs { get; set; }
  }
}