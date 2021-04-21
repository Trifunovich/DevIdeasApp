using System;
using System.Collections.Generic;
using System.Linq;
using DataServiceProvider.Models;

namespace TestingConsole.Fakers
{
  public sealed class CarDocumentFaker : DefaultFake<CarDocumentInputDto>
  {
    private List<DocumentType> docTypes => Enum.GetValues<DocumentType>().OfType<DocumentType>().ToList();

    public CarDocumentFaker()
    {
      RuleFor(fake => fake.Id, () => default);
      RuleFor(fake => fake.Mileage, fake => fake.Random.Int(1000, 10000000));
      RuleFor(fake => fake.TypeOfDoc, fake => docTypes[fake.Random.Int(0, docTypes.Count-1)] );
      
    }
  }
}