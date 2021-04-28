using System.Collections.Generic;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  public class FakeCarDocumentHistory : FakeDataModelBase, ICarDocumentHistoryBase
  {
    private List<ICarDocumentBase> _getDocs;
    public int CarId { get; set; }

    public List<ICarDocumentBase> GetDocs => _getDocs;

    public List<string> DocIds { get; set; }
  }
}