using DataAccess.Core.Abstractions;
using DateAccess.RavenDb.Abstractions;
using DateAccess.RavenDb.Context;
using DateAccess.RavenDb.Models;
using DateAccess.RavenDb.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DateAccess.RavenDb.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddRavenDbDataAccessInternals (this IServiceCollection services)
    {
      services.AddSingleton(typeof(IRavenDbContextFactory), typeof(RavenDbContextFactory));
    
      services.AddSingleton<IDataManagementService, DataManagementService>();

      ResolveRepos<RavenCar>(services);
      ResolveRepos<RavenCarPicture>(services);
      ResolveRepos<RavenCarUser>(services);
      ResolveRepos<RavenCarDocument>(services);
      ResolveRepos<RavenCarDocumentHistory>(services);

      return services;
    }

    private static void ResolveRepos<T>(IServiceCollection services) where T : RavenDbDataModelBase
    {
      services.AddScoped<IDataRepository<T>, RavenDbRepository<T>>();
    }
  }
}