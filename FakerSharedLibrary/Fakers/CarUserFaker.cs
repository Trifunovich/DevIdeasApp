using System;
using System.Linq;
using Bogus.Extensions;
using DataAccess.Models;
using FakerSharedLibrary.FakeModels;

namespace FakerSharedLibrary.Fakers
{
  public sealed class CarUserFaker : DefaultFake<FakeCarUser>
  {
    private Random rnd = new Random();
    private CarFaker carFaker = new CarFaker();

    public CarUserFaker()
    {
      var fakedCars = carFaker.GenerateBetween(5, 25).Select(x => x as ICarBase).ToList(); 
      var take = fakedCars.OrderBy(x => rnd.Next()).Take(rnd.Next(0, fakedCars.Count - 1)).ToList();
  
      RuleFor(fake => fake.DateOfBirth, f => new DateTime(rnd.Next(1950, 2020), rnd.Next(1, 12), rnd.Next(1, 28)));
      RuleFor(fake => fake.EMail, f => f.Internet.Email());
      RuleFor(fake => fake.FirstName, f => f.Person.FirstName);
      RuleFor(fake => fake.MiddleName, f => f.Person.FirstName);
      RuleFor(fake => fake.LastName, f => f.Person.LastName);
     
      RuleFor(fake => fake.AllCars, take);
    }
  }
}