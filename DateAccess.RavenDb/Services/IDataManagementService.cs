using System.Threading.Tasks;
using DateAccess.RavenDb.Context;

namespace DateAccess.RavenDb.Services
{
  public interface IDataManagementService
  {
    Task<string> DisplayRavenEmbeddedServerInTheBrowser();
  }

  internal class DataManagementService : IDataManagementService
  {
    public async Task<string> DisplayRavenEmbeddedServerInTheBrowser()
    {
      return await RavenDbContextFactory.DisplayServerInBrowser();
    }
  }
}