using System.Collections.Generic;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  public class FakeCarDocumentHistory : FakeDataModelBase, ICarDocumentHistoryBase
  {
    public FakeCarDocumentHistory()
    {
      GetDocs = new List<ICarDocumentBase>();
    }

    public int CarId { get; set; }

    public List<ICarDocumentBase> GetDocs { get; set; }

    public List<string> DocIds { get; set; }
  }
}