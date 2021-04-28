﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Validation;
using DataAccess.Sql.Context;
using DataAccess.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedCodeLibrary;

namespace DataAccess.Sql.Repositories
{
  class CarUserEfrDataRepository : EfDataRepository<CarUser>
  {
    protected override Func<bool?, Task<IList<CarUser>>> GetAllFunction =>
      async (bool? isActive) =>
      {
        return await DataSet.Where(cu => (isActive == null) || cu.IsActive == isActive).ToListAsync();
        //return await DataSet.Include(x => x.Cars.Where(c => (isActive == null) || c.IsActive == isActive)).Where(cu => (isActive == null) || cu.IsActive == isActive).ToListAsync();
      };

    protected override Func<PagingParameters, bool?, Task<IList<CarUser>>> GetPageFunction =>
      async (PagingParameters pager, bool? isActive) =>
      {
        var results = await DataSet.Where(x => isActive == null || x.IsActive == isActive).Skip(pager.FirstElementPosition).Take(pager.PageSize).ToListAsync(TokenSource.Token);
        //var results = await DataSet.Include(x => x.Cars).Where(x => isActive == null || x.IsActive == isActive).Skip(pager.FirstElementPosition).Take(pager.PageSize).ToListAsync(TokenSource.Token);
        return results;
      };

    protected override Func<DateTime, DateTime?, bool?, Task<IList<CarUser>>> GetAllFunctionWithDates =>
      async (DateTime createdAfter, DateTime? createdBefore, bool? isActive) =>
      {
        return await DataSet.Where(cu => (isActive == null || cu.IsActive == isActive) && cu.CreatedOn >= createdAfter && cu.CreatedOn <= (createdBefore ?? DateTime.Now)).ToListAsync();
        //return await DataSet.Include(x => x.Cars.Where(c => (isActive == null) || c.IsActive == isActive)).Where(cu => (isActive == null || cu.IsActive == isActive) && cu.CreatedOn >= createdAfter && cu.CreatedOn <= (createdBefore ?? DateTime.Now)).ToListAsync();
      };

    public CarUserEfrDataRepository(IEfContextFactory contextFactory, ILogger<EfDataRepository<CarUser>> logger, IRepositoryInputValidator inputValidator) : base(contextFactory, logger, inputValidator)
    {
    }
  }
}