using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Abstractions;
using DataAccess.Core.Validation;
using DataAccess.MongoDb.Abstractions;
using DataAccess.MongoDb.Context;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SharedCodeLibrary;

namespace DataAccess.MongoDb.Repositories
{
  internal class MongoDbRepository<T> : DataRepositoryBase<T> where T : MongoDbDataModelBase
  {
    private readonly IMongoDbContextFactory<T> _context;

    //todo: make factory
    private IMongoCollection<T> Collection => _context.GetCollection;

    protected override Func<PagingParameters, bool?, Task<IList<T>>> GetPageFunction =>
      async (PagingParameters pager, bool? isActive) =>
      {
        var results = await Collection.Find(x => isActive == null || x.IsActive == isActive).Skip(pager.FirstElementPosition).Limit(pager.PageSize).ToListAsync(TokenSource.Token);
     
        return results;
      };

    protected override Func<bool?, Task<IList<T>>> GetAllFunction =>
     async (bool? isActive) =>
     {
       return await Collection.Find(p => (isActive == null || p.IsActive == isActive.Value)).ToListAsync(cancellationToken: TokenSource.Token);
     };

    protected override Func<DateTime, DateTime?, bool?, Task<IList<T>>> GetAllFunctionWithDates =>
      async (DateTime createdAfter, DateTime? createdBefore, bool? isActive) =>
      {
        var res = await Collection.Find(p => (isActive == null || p.IsActive == isActive.Value) && p.CreatedOn >= createdAfter && (p.CreatedOn <= (createdBefore ?? DateTime.Now))).ToListAsync(TokenSource.Token);
        LogGotAll(res.Count);
        return res;
      };

    protected override Func<string, bool?, Task<T>> GetByLabelFunction =>
      async (label, isActive) =>
      {
        var res = (await Collection.Find(p => (isActive == null || p.IsActive == isActive.Value) && p.Label == label).ToListAsync(TokenSource.Token)).FirstOrDefault();
        LogGotOneFromDatabase(res);
        return res; 
      };

    protected override Func<IEnumerable<T>, Task<int?>> GetInsertFunction =>
      async (records) =>
      {
        var noSqlDataModels = records.ToList();
        await Collection.InsertManyAsync(noSqlDataModels, cancellationToken: TokenSource.Token);
        LogInsertedIntoDb(noSqlDataModels.Count());
        return await Task.FromResult(noSqlDataModels.Count);
      };

    protected override Func<T, Task<int?>> GetAddFunction =>
      async (record) =>
      {
        await Collection.InsertOneAsync(record, cancellationToken: TokenSource.Token);
        return await Task.FromResult(1);
      };

    protected override Func<T, Task<int?>> GetHardDeleteByValueFunction =>
      async (record) => await HardDeleteById(record.GetId);

    protected override Func<T, Task<int?>> GetUpdateFunction =>
      async (record) =>
      {
        var filter = Builders<T>.Filter.Eq(x => x.Id, record.Id);
        var doc = record.ToBsonDocument();
        var res = await Collection.UpdateOneAsync(filter, doc, null, TokenSource.Token);
        return await Task.FromResult((int)res.ModifiedCount);
      };

    protected override Func<Task<int?>> GetSaveChangesFunction =>
      async () => await Task.FromResult(1);



    protected override Func<string, bool?, Task<T>> GetByIdFunction =>
      async (id, isActive) =>
      {
        return await Collection.Find(p => (isActive == null || p.IsActive == isActive.Value) && p.Id == Guid.Parse(id)).FirstOrDefaultAsync(cancellationToken: TokenSource.Token);
      };

    public override async Task<int?> Apply()
    {
      return await base.Apply();
      //await _session.CommitTransactionAsync();
    }

    public override void Dispose()
    {
      base.Dispose();
      _context.EndSession();
      LogDisposedRepo();
    }

    protected override async Task Rollback()
    {
      //await _session.AbortTransactionAsync(TokenSource.Token);
      await base.Rollback();
    }

    public MongoDbRepository(IMongoDbContextFactory<T> context, ILogger<MongoDbRepository<T>> logger, IRepositoryInputValidator inputValidator) : base(logger, inputValidator)
    {
      _context = context;
      _context.StartSession(TokenSource);
      LogStartedRepo();
    }
  }
}