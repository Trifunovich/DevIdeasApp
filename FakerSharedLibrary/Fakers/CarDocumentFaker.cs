using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using FakerSharedLibrary.FakeModels;

namespace FakerSharedLibrary.Fakers
{
  public sealed class CarDocumentFaker : DefaultFake<FakeCarDocument>
  {
    private List<DocumentType> docTypes => Enum.GetValues(typeof(DocumentType)).OfType<DocumentType>().ToList();

    public CarDocumentFaker()
    {
      RuleFor(fake => fake.Mileage, fake => fake.Random.Int(1000, 10000000));
      RuleFor(fake => fake.TypeOfDoc, fake => docTypes[fake.Random.Int(0, docTypes.Count-1)] );
    }
  }

  public sealed class CarDocumentHistoryFaker : DefaultFake<FakeCarDocumentHistory>
  {
    private CarDocumentFaker docFaker = new CarDocumentFaker();
    private Random rnd = new Random();

    public CarDocumentHistoryFaker()
    { 
      var fakeDocs = docFaker.Generate(25).OfType<ICarDocumentBase>().ToList();
      var take = fakeDocs.OrderBy(x => rnd.Next()).Take(rnd.Next(0, fakeDocs.Count - 1)).ToList();
      var ids = take.Select(s => s.GetId).ToList();
      RuleFor(fake => fake.CarId, 0);
      RuleFor(fake => fake.DocIds , ids);
      RuleFor(fake => fake.Docs, take);
    }
  }
}