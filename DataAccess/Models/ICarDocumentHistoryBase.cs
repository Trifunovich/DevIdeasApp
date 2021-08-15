using System.Collections.Generic;
using DataAccess.Core.Abstractions;

namespace DataAccess.Models
{
  public interface ICarDocumentHistoryBase : IDataModelBase
  {
    int CarId { get; set; }

    List<ICarDocumentBase> Docs { get; set; }
  }
}