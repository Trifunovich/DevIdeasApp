using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public abstract class CarDocumentHistoryBase : DataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    protected abstract void AddDoc(ICarDocumentBase doc);
    
    public abstract List<ICarDocumentBase> GetDocs { get; }
  }
}