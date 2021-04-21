using DataAccess.Models;
using FakerSharedLibrary.FakeModels;

namespace FakerSharedLibrary.Fakers
{
  public sealed class CarPictureFaker : DefaultFake<FakeCarPicture>
  {
    public CarPictureFaker(int minCarId, int maxCarId, int minCarUserId, int maxCarUserId)
    {
      RuleFor(fake => fake.CarId, fake => fake.Random.Int(minCarId, maxCarId));
      RuleFor(fake => fake.CarUserId, fake => fake.Random.Int(minCarUserId, maxCarUserId));
      RuleFor(fake => fake.Picture, fake => fake.Internet.Url());
    }
  }
}
