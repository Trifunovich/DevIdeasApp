using System;

namespace MapperSharedLibrary.Models
{
  public abstract class DtoBase : IDto
  {
    protected DtoBase()
    {
      IsActive = true;
      UpdatedOn = DateTime.Now;
    }

    public string Id { get; set; }

    public override string ToString()
    {
      return $"{GetType().Name} [l:{Label}, a:{IsActive}, c:{CreatedOn}]";
    }
    
    public string Label { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedOn { get; set; }

    public DateTime CreatedOn { get; set; }
  }
}