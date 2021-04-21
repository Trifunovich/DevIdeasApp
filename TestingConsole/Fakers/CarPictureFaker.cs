using System;
using DataServiceProvider.Models;

namespace TestingConsole.Fakers
{
  public sealed class CarPictureFaker : DefaultFake<CarPictureInputDto>
  {
    public CarPictureFaker(int minCarId, int maxCarId, int minCarUserId, int maxCarUserId)
    {
      RuleFor(fake => fake.Id, () => Guid.Empty);
      RuleFor(fake => fake.CarId, fake => fake.Random.Int(minCarId, maxCarId));
      RuleFor(fake => fake.CarUserId, fake => fake.Random.Int(minCarUserId, maxCarUserId));
      RuleFor(fake => fake.Picture, fake => fake.Internet.Url());
    }
  }
}
