using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Core.Validation;
using DataAccess.Models;
using DataAccess.Sql.Models;
using Microsoft.Extensions.Logging;
using SharedCodeLibrary;

namespace DataAccess.Sql.Repositories
{
  class CarUserDapperDataRepository : DapperDataRepository<CarUser>
  {
    public CarUserDapperDataRepository(IDapperResolver<CarUser> resolver, ILogger<DapperDataRepository<CarUser>> logger, IRepositoryInputValidator inputValidator) : base(resolver, logger, inputValidator)
    {
    }

    protected override Func<bool?, Task<IList<CarUser>>> GetAllFunction =>
      async (bool? isActive) =>
      {
        var param = isActive == null ? null : new { IsActive = isActive };
        return await GetImplicit(param);
      };
    
    
    private Func<CarUser, Car, CarUser> GetCarMapping()
    {
      var lookup = new Dictionary<int, CarUser>();
      return new Func<CarUser, Car, CarUser>((s, a) =>
      {
        if (!lookup.TryGetValue(s.Id, out var carUser))
        {
          lookup.Add(s.Id, carUser = s);
        }
        
        carUser.CarCarUsers.Add(new CarCarUser{Car = a, CarUser = s});
        return carUser;
      });
    }

    protected override Func<PagingParameters, bool?, Task<IList<CarUser>>> GetPageFunction =>
      async (PagingParameters pager, bool? isActive) =>
      {
        string activeCheck = GetActiveCheckString(isActive, "cu.", true);
        var sql = ($@" {GetQuery}{activeCheck}
                       ORDER BY cu.{nameof(CarUser.Id)}
                       OFFSET      @FirstElementPosition ROWS 
                       FETCH NEXT  @PageSize   ROWS ONLY");
        
        var results = (await Resolver.Connection.QueryAsync(sql, GetCarMapping(), param: new DapperPagingParams(pager, isActive), transaction: Transaction)).ToList();

        return results;
      };

    protected override Func<DateTime, DateTime?, bool?, Task<IList<CarUser>>> GetAllFunctionWithDates =>
      async (DateTime createdAfter, DateTime? createdBefore, bool? isActive) =>
      {
        QueryParams param = isActive == null
          ? new QueryParams(createdAfter)
          : new ActiveQueryParams(createdAfter, isActive.Value);
        return await GetImplicit(param);
      };

    private static string GetQuery = @"
                SELECT cu.*, c.*
                FROM CarUsers cu
                INNER JOIN Cars c on cu.Id = c.CarUserId";
    

    private class QueryParams
    {
      public QueryParams(DateTime createdAfter)
      {
        CreatedAfter = createdAfter;
      }

      public DateTime CreatedAfter { get; }
      public DateTime CreatedBefore { get; set; } = DateTime.Now;
    }

    private class ActiveQueryParams : QueryParams
    {
      public bool IsActive { get; }

      public ActiveQueryParams(DateTime createdAfter, bool isActive = true) : base(createdAfter)
      {
        IsActive = isActive;
      }
    }
    
    private async Task<IList<CarUser>> GetImplicit(object param)
    {
      var lookup = new Dictionary<int, CarUser>();
      await Resolver.Connection.QueryAsync(GetQuery, GetCarMapping(), param, transaction: Transaction);
      var resultList = lookup.Values;
      LogGotAll(lookup.Count);
      return resultList.AsList();
    }
    
  }
}