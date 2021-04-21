using System.Collections.Generic;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  internal class RavenCarDocumentHistory : RavenDbDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<ICarDocumentBase> Docs { get; set; }
  }
}