using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Abstractions;
using DataAccess.Core.Validation;
using DateAccess.RavenDb.Abstractions;
using DateAccess.RavenDb.Context;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SharedCodeLibrary;

namespace DateAccess.RavenDb.Repositories
{
  internal class RavenDbRepository<T> : DataRepositoryBase<T> where T : RavenDbDataModelBase
  {
    protected readonly IRavenDbContextFactory Context;

    public RavenDbRepository(IRavenDbContextFactory context, ILogger<RavenDbRepository<T>> logger, IRepositoryInputValidator inputValidator) : base(logger, inputValidator)
    {
      Context = context;
    }

    protected override Func<PagingParameters, bool?, Task<IList<T>>> GetPageFunction =>
      async (PagingParameters pager, bool? isActive) =>
      {
        IList<T> results = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            results = await session.Query<T>()
              .Where(x => isActive == null || x.IsActive == isActive)
              .Skip(pager.FirstElementPosition)   // skip 2 pages worth of products
              .Take(pager.PageSize)   // take up to 10 products
              .ToListAsync(TokenSource.Token);  // execute query

            return true;
          }
          catch (Exception e)
          {
            // ignored
            return false;
          }
        }

        await Context.RunThroughSession(Func);
        return results;
      };

    protected override Func<bool?, Task<IList<T>>> GetAllFunction =>
      async (bool? isActive) =>
      {
        IList<T> results = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            results = await session.Query<T>()
              .Where(x => isActive == null || x.IsActive == true)
              .Select(x => x)
              .ToListAsync(TokenSource.Token);
            return true;
          }
          catch (Exception e)
          {
            // ignored
            return false;
          }
        }

        await Context.RunThroughSession(Func);

        return results;
      };


    protected override Func<DateTime, DateTime?, bool?, Task<IList<T>>> GetAllFunctionWithDates =>
      async (DateTime createdAfter, DateTime? createdBefore, bool? isActive) =>
      {
       
        IList<T> results = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            results = await session.Query<T>()
              .Where(x => (isActive == null || x.IsActive == isActive.Value) && x.CreatedOn >= createdAfter && x.CreatedOn <= (createdBefore ?? DateTime.Now))
              .Select(x => x)
              .ToListAsync(TokenSource.Token);
          }
          catch (Exception e)
          {
            // ignored
          }
          return true;
        }

        await Context.RunThroughSession(Func);
        return results;
      };

    protected override Func<string, bool?, Task<T>> GetByLabelFunction =>
      async (label, isActive) =>
      {
        IList<T> results = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            results = await session
              .Query<T>()
              .Where(x => (isActive == null || x.IsActive == isActive.Value) && x.Label.Equals(label, StringComparison.InvariantCultureIgnoreCase))
              .ToListAsync(TokenSource.Token);
            return true;
          }
          catch (Exception e)
          {
            return false;
          }
        }

        await Context.RunThroughSession(Func);
        return results?.FirstOrDefault();
      };

    protected override Func<IEnumerable<T>, Task<int?>> GetInsertFunction =>
      async (records) =>
      {
        int? result = null;
        var listToInsert = records.ToList();

        async Task<bool> Func(BulkInsertOperation bulkInsert)
        {
          try
          {
            try
            {
              foreach (var rec in listToInsert)
              {
                try
                {
                  await bulkInsert.StoreAsync(rec);

                  if (result is null)
                  {
                    result = 1;
                  }
                  else
                  {
                    result++;
                  }
                }
                catch (Exception e)
                {
                  Console.WriteLine(e);
                  throw;
                }
              }
            }
            finally
            {
              if (bulkInsert != null)
              {
                await bulkInsert.DisposeAsync().ConfigureAwait(false);
              }
            }
            return true;
          }
          catch (Exception e)
          {
            return false;
          }
        }

        await Context.RunThroughBulkInsert(Func, TokenSource);
        return result;
      };


    protected override Func<T, Task<int?>> GetAddFunction =>
      async (record) =>
      {
        int? result = null;
        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            await session.StoreAsync(record, TokenSource.Token);
            result = 1;
            return true;
          }
          catch (Exception e)
          {
            return false;
          }
        }

        await Context.RunThroughSession(Func);
        return result;
      };

    protected override Func<T, Task<int?>> GetHardDeleteByValueFunction =>
      async (record) => await HardDeleteById(record.Id);

    protected override Func<T, Task<int?>> GetUpdateFunction =>
      async (record) => await Apply();

    protected override Func<Task<int?>> GetSaveChangesFunction =>
      async () =>
      {
        int? result = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            await session.SaveChangesAsync(TokenSource.Token);
            result = 1;
            return true;
          }
          catch (Exception)
          {
            return false;
          }
        }

        await Context.RunThroughSession(Func);
        return result;
      };


    protected override Func<string, bool?, Task<T>> GetByIdFunction =>
      async (id, isActive) =>
      {
        IList<T> results = null;

        async Task<bool> Func(IAsyncDocumentSession session)
        {
          try
          {
            results = await session
              .Query<T>()
              .Where(x => (isActive == null || x.IsActive == isActive.Value) && x.Id.Equals(id))
              .ToListAsync(TokenSource.Token);
            return true;
          }
          catch (Exception e)
          {
            return false;
          }
        }

        await Context.RunThroughSession(Func);
        return results?.FirstOrDefault();
      };
    
    public override async Task<int?> Apply()
    {
      return await base.Apply();
    }

    public override void Dispose()
    {
      base.Dispose();
      Context?.DisposeStore();
      LogDisposedRepo();
    }

    protected override async Task Rollback()
    {
      //await _session.AbortTransactionAsync(TokenSource.Token);
      await base.Rollback();
    }
  }


}