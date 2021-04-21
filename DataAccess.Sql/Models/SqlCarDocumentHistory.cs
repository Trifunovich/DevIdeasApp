using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  internal class SqlCarDocumentHistory : SqlDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<ICarDocumentBase> Docs { get; set; }
  }
}