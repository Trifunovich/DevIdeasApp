using System.Threading.Tasks;
using DateAccess.RavenDb.Context;

namespace DateAccess.RavenDb.Services
{
  public interface IDataManagementService
  {
    Task<bool> DisplayRavenEmbeddedServerInTheBrowser();
  }

  internal class DataManagementService : IDataManagementService
  {
    public Task<bool> DisplayRavenEmbeddedServerInTheBrowser()
    {
      return Task.FromResult(RavenDbContextFactory.DisplayServerInBrowser());
    }
  }
}