using System;
using System.Threading;
using System.Threading.Tasks;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Session;

namespace DateAccess.RavenDb.Context
{
  public interface IRavenDbContextFactory
  {
    Task DisposeStore();
    Task RunThroughSession(Func<IAsyncDocumentSession, Task<bool>> function);
    Task RunThroughBulkInsert(Func<BulkInsertOperation, Task<bool>> function, CancellationTokenSource tokenSource);
  }
}