using DataAccess.Models;
using DateAccess.RavenDb.Abstractions;

namespace DateAccess.RavenDb.Models
{
  internal class RavenCarDocument : RavenDbDataModelBase, ICarDocumentBase
  {
     public double Mileage { get; set; }

     public DocumentType TypeOfDoc { get; set; }
  }
}