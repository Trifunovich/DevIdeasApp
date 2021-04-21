using System.Threading.Tasks;
using DateAccess.RavenDb.Services;

namespace DataServiceProvider.Services
{
  public interface IManagerService
  {
    Task<bool> DisplayEmbeddedRavenServer();
  }

  class ManagerService : IManagerService
  {
    private readonly IDataManagementService _managementService;

    public ManagerService(IDataManagementService managementService)
    {
      _managementService = managementService;
    }
    public Task<bool> DisplayEmbeddedRavenServer()
    {
      return _managementService.DisplayRavenEmbeddedServerInTheBrowser();
    }
  }
}