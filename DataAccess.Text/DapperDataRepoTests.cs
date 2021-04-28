using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Validation;
using DataAccess.Models;
using DataAccess.Sql.Models;
using DataAccess.Sql.Repositories;
using FakerSharedLibrary.Fakers;
using Microsoft.Extensions.Logging;
using Moq;
using SharedCodeLibrary;
using Xunit;

namespace DataAccess.Test
{
  public class DapperDataRepoTests
  {
    private static readonly Mock<ILogger<DapperDataRepository<Car>>> LoggerMock = new Mock<ILogger<DapperDataRepository<Car>>>();

    public DapperDataRepoTests()
    {

    }

    /// <summary>
    /// Verifying that we call the database once and that we didn't change the sql string
    /// </summary>
    [Fact]
    public async Task GetAllWithBool_ValidCall()
    {
      string expectedSql = $@" select * from [dbo].[Cars] where IsActive = @IsActive
                       ORDER BY Id";

      var resolverMock = ResolverMock(expectedSql, false);

      var testingObject = GetTestingRepo(resolverMock);

      var toTestResult = await testingObject.GetAll(true);

      Assert.Equal(toTestResult?.Count(), _expectedResult?.Count);

      PostVerifications(resolverMock);
    }

    [Fact]
    public async Task GetPageFunction_ValidCall()
    {
      string expectedSql = $@" select * from [dbo].[Cars] where IsActive = @IsActive
                       ORDER BY Id
                       OFFSET      @FirstElementPosition ROWS 
                       FETCH NEXT  @PageSize   ROWS ONLY";

      var resolverMock = ResolverMock(expectedSql, true);

      var testingObject = GetTestingRepo(resolverMock);

      var toTestResult = await testingObject.GetPage(_defaultPagingParameters, true);

      Assert.Equal(toTestResult?.Count(), _expectedResult?.Count);

      PostVerifications(resolverMock);
    }

    private readonly List<ICarBase> _expectedResult = GetTestCars(20).Where(c => c.IsActive == true).ToList();
    private readonly PagingParameters _defaultPagingParameters = new PagingParameters(1);

    private Mock<IDapperResolver<Car>> ResolverMock(string expectedSql, bool usePagingParams)
    {
      var resolverMock = new Mock<IDapperResolver<Car>>();
      resolverMock.Setup(res => res.GetResults(expectedSql.Trim(), It.IsAny<object>()))
        .ReturnsAsync(_expectedResult.Select(x => x as Car).ToList());
      return resolverMock;
    }

    private Mock<IRepositoryInputValidator> ValidatorMock()
    {
      var t = new Mock<IRepositoryInputValidator>();
      t.Setup(v => v.ValidatePagingParams(It.IsAny<PagingParameters>())).Returns(true);
      t.Setup(v => v.ValidateValue<IList<Car>>(It.IsAny<List<Car>>())).Returns(true);
      return t;
    }

    private DapperDataRepository<Car> GetTestingRepo(Mock<IDapperResolver<Car>> resolverMock)
    {
      var resolver = resolverMock.Object;
      var logger = LoggerMock.Object;
      var validator = ValidatorMock().Object;
      return new DapperDataRepository<Car>(resolver, logger, validator);
    }

    private void PostVerifications(Mock<IDapperResolver<Car>> resolverMock)
    {
      resolverMock.Verify(v => v.GetResults(It.IsAny<string>(), It.IsAny<object>()));

      LoggerMock.Verify(
        x => x.Log(
          It.Is<LogLevel>(l => l == LogLevel.Information),
          It.IsAny<EventId>(),
          It.Is<It.IsAnyType>((v, t) => true),
          It.IsAny<Exception>(),
          It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
    }
    
    private static IList<ICarBase> GetTestCars(int count)
    {
      return (new CarFaker()).Generate(count).OfType<ICarBase>().ToList();
    }
  }
}