using DataServiceProvider.Models;

namespace TestingConsole.Fakers
{
  public sealed class CarFaker : DefaultFake<CarInputDto>
  {
    public CarFaker()
    {
      RuleFor(fake => fake.Id, () => 0);
    }
  }
}