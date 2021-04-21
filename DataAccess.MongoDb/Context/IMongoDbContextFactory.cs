using System.Threading;
using DataAccess.MongoDb.Abstractions;
using MongoDB.Driver;

namespace DataAccess.MongoDb.Context
{
  internal interface IMongoDbContextFactory<T> where T : MongoDbDataModelBase
  {
    void StartSession(CancellationTokenSource tokenSource);

    void EndSession();

    IMongoCollection<T> GetCollection { get; }
  }
}