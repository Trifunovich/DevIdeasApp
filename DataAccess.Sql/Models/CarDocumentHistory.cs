using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DataAccess.Models;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Models
{
  internal class CarDocumentHistory : SqlDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<ICarDocumentBase> Docs { get; set; }
    
  }
}