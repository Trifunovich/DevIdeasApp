using Autofac;
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

    private static void ResolveRepos<T>(ContainerBuilder builder) where T : RavenDbDataModelBase
    {
      builder.RegisterType<RavenDbRepository<T>>().As<IDataRepository<T>>();
    }

    public static ContainerBuilder AddRavenDbDataAccessInternals(this ContainerBuilder builder)
    {
      builder.RegisterType(typeof(RavenDbContextFactory)).As(typeof(IRavenDbContextFactory)).SingleInstance();
      builder.RegisterType(typeof(DataManagementService)).As(typeof(IDataManagementService)).SingleInstance();
      ResolveRepos<RavenCar>(builder);
      ResolveRepos<RavenCarPicture>(builder);
      ResolveRepos<RavenCarUser>(builder);
      ResolveRepos<RavenCarDocument>(builder);
      ResolveRepos<RavenCarDocumentHistory>(builder);

      return builder;
    }
  }
}