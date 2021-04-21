using DataAccess.Models;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.FakeModels
{
  public class FakeCarDocument : FakeDataModelBase, ICarDocumentBase
  {
     public double Mileage { get; set; }

     public DocumentType TypeOfDoc { get; set; }
  }
}