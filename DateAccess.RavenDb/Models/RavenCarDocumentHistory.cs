using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  internal class RavenCarDocumentHistory : RavenDbDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<RavenCarDocument> Docs { get; set; }

    public List<ICarDocumentBase> GetDocs => Docs.OfType<ICarDocumentBase>().ToList();
  }
}