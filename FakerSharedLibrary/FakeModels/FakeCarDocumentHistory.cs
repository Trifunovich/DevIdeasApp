using System.Collections.Generic;
using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  public class FakeCarDocumentHistory : FakeDataModelBase, ICarDocumentHistoryBase
  {
    public int CarId { get; set; }

    public List<ICarDocumentBase> Docs { get; set; }
  }
}