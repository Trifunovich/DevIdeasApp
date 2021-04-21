using System;
using Bogus.Extensions;
using DataServiceProvider.Models;

namespace TestingConsole.Fakers
{
  public sealed class CarUserFaker : DefaultFake<CarUserInputDto>
  {
    private Random rnd = new();
    private CarFaker carFaker = new CarFaker();

    public CarUserFaker()
    {
      RuleFor(fake => fake.Id, () => 0);
      RuleFor(fake => fake.DateOfBirth, f => new DateTime(rnd.Next(1950, 2020), rnd.Next(1, 12), rnd.Next(1, 28)));
      RuleFor(fake => fake.EMail, f => f.Internet.Email());
      RuleFor(fake => fake.FirstName, f => f.Person.FirstName);
      RuleFor(fake => fake.MiddleName, f => f.Person.FirstName);
      RuleFor(fake => fake.LastName, f => f.Person.LastName);
      RuleFor(fake => fake.Cars, f => carFaker.GenerateBetween(5, 25));
    }
  }
}