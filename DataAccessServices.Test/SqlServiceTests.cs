using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Sql.Models;
using DataServiceProvider.Services;
using FakerSharedLibrary.FakeModels;
using MapperSharedLibrary.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DataServiceProvider.Tests
{
  public class SqlServiceTests
  {

    private static readonly Mock<ILogger<CarService>> LoggerMock = new Mock<ILogger<CarService>>();
    /// <summary>
    /// Verifying that the database calls work properly with the service
    /// </summary>
    [Fact]
    public async Task GetAllWithBool_ValidCall()
    {
      var mockedRepo = new DataRepoMock<ICarBase, FakeCar>();
      mockedRepo.FillWithFakes();

      var mapperMock = new Mock<IMapper>();
      mapperMock.Setup(m => m.Map<SqlCar, CarOutputDto>(It.IsAny<SqlCar>())).Returns(new CarOutputDto());

      //oh
      var testingObject = new CarService(mockedRepo, mapperMock.Object, null );

      var expected = mockedRepo.GetRepoObjects.ToList();

      var toTestResult = await testingObject.GetAll(true);

      Assert.Equal(toTestResult?.Count(), expected?.Count);
    }
  }
}