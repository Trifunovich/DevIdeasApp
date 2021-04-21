using System.Linq;
using System.Threading;
using DataAccess.Core.Attributes;
using DataAccess.MongoDb.Abstractions;
using DataAccess.MongoDb.Extensions;
using DataAccess.MongoDb.Helpers;
using MongoDB.Driver;

namespace DataAccess.MongoDb.Context
{
  internal class MongoDbContextFactory<T> : IMongoDbContextFactory<T> where  T: MongoDbDataModelBase
  {
    private readonly MongoClient _dbClient = new MongoClient(ConnectionHelper.MongoDbConnectionString);
    private IMongoDatabase _database => _dbClient.GetDatabase("DefaultDb");
    private IClientSession _session;

    public void StartSession(CancellationTokenSource tokenSource)
    {
      _session = _dbClient.StartSession(cancellationToken: tokenSource.Token);
      //_session.StartTransaction();
    }

    public void EndSession()
    {
      _session?.Dispose();
    }

    public IMongoCollection<T> GetCollection => _database.GetCollection<T>(typeof(T)?.GetCustomAttributesFromClass<TableAttribute>(false).FirstOrDefault()?.Name ?? string.Empty);
  }
}