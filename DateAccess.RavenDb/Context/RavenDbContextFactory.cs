using System;
using System.Threading;
using System.Threading.Tasks;
using DateAccess.RavenDb.Helpers;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Embedded;

namespace DateAccess.RavenDb.Context
{
  internal class RavenDbContextFactory : IRavenDbContextFactory
  {
    private readonly ILogger<RavenDbContextFactory> _logger;
    private static bool _isServerRunning;

    private static readonly DatabaseOptions DbOptions = new DatabaseOptions(new DatabaseRecord
    {
      DatabaseName = "DemoDb"
    });

    public RavenDbContextFactory(ILogger<RavenDbContextFactory> logger)
    {
      _logger = logger;
      if (!_isServerRunning)
      {
        StartServer();
      }
    }
    
    private void StartServer()
    {
      try
      {
        var serverOptions = new ServerOptions
        {
          ServerDirectory = $"{ConnectionHelper.CurrDir}\\RavenDBServer"
        };

        EmbeddedServer.Instance.StartServer(serverOptions);
        _logger.LogInformation("RavenDb server started");
        
        _isServerRunning = true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public static bool DisplayServerInBrowser()
    {
      if (_isServerRunning)
      {
        EmbeddedServer.Instance.OpenStudioInBrowser();
        return true;
      }

      return false;
    }

    public async Task RunThroughSession(Func<IAsyncDocumentSession, Task<bool>> function)
    {
      await RunThroughStore(async store =>
      {
        using (var session = store.OpenAsyncSession())
        {
          return await function.Invoke(session);
        }
      });
    }

    public async Task RunThroughBulkInsert(Func<BulkInsertOperation, Task<bool>> function, CancellationTokenSource tokenSource)
    {
      await RunThroughStore(async store =>
      {
        var bulkInsert = store.BulkInsert(null, tokenSource.Token);
        return await function.Invoke(bulkInsert);
      });
    }

    private async Task RunThroughStore(Func<IDocumentStore, Task<bool>> function)
    {
      using (IDocumentStore store = await EmbeddedServer.Instance.GetDocumentStoreAsync(DbOptions))
      {
        store.Initialize();
        Console.WriteLine("Store initialized!");
        var result = await function.Invoke(store);
      }
    }

    public async Task DisposeStore()
    {
      await Task.CompletedTask;
    }
  }
}