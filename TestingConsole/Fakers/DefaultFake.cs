using System;
using Bogus;
using DataServiceProvider.Models;

namespace TestingConsole.Fakers
{
  public class DefaultFake<T> : Faker<T> where T : DtoBase
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