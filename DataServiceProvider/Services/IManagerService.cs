using System.Threading.Tasks;
using DateAccess.RavenDb.Services;

namespace DataServiceProvider.Services
{
  public interface IManagerService
  {
    Task<string> DisplayEmbeddedRavenServer();
  }

  class ManagerService : IManagerService
  {
    private readonly IDataManagementService _managementService;

    public ManagerService(IDataManagementService managementService)
    {
      _managementService = managementService;
    }
    public async Task<string> DisplayEmbeddedRavenServer()
    {
      return await _managementService.DisplayRavenEmbeddedServerInTheBrowser();
    }
  }
}