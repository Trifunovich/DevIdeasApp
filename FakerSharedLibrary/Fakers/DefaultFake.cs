using System;
using Bogus;
using DataAccess.Core.Abstractions;
using FakerSharedLibrary.FakeAbstractions;

namespace FakerSharedLibrary.Fakers
{
  public class DefaultFake<T> : Faker<T> where T : FakeDataModelBase
  {
    public DefaultFake()
    {
      RuleFor(fake => fake.IsActive, () => true);
      RuleFor(fake => fake.Label, f => f.Random.String2(25, 50));
      RuleFor(fake => fake.CreatedOn, f => DateTime.Now);
      RuleFor(fake => fake.UpdatedOn, f => DateTime.Now);
    }
  }
}