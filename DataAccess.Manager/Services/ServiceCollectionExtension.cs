using Autofac;
using Autofac.Builder;
using DataAccess.Core.Services;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataAccess.MongoDb.Services;
using DataAccess.Services;
using DataAccess.Sql.Services;
using DateAccess.RavenDb.Services;
using Microsoft.Extensions.DependencyInjection;
using static DataAccess.Manager.Services.DiResolver;

namespace DataAccess.Manager.Services
{
  public static class ServiceCollectionExtension
  {


    /// <summary>
    /// Adds MS DI dependencies from this library
    /// </summary>
    public static IServiceCollection AddDataAccessManagerInternals(this IServiceCollection services)
    {

      services.AddDataAccessCoreInternals();
      services.AddDataAccessInternals();
      services.AddMongoDbDataAccessInternals();
      services.AddRavenDbDataAccessInternals();
      services.AddSqlDataAccessInternals();
      

      services.AddScoped(ResolveCarAdapter);
      services.AddScoped(ResolveCarUserAdapter);
      services.AddScoped(ResolveCarPictureAdapter);
      services.AddScoped(ResolveCarDocumentAdapter);
      services.AddScoped(ResolveCarDocumentHistoryAdapter);
    
      return services;
    }

    /// <summary>
    /// Adds Autofac dependencies from this library
    /// </summary>
    public static ContainerBuilder AddDataAccessManagerInternals(this ContainerBuilder builder)
    {

      builder.AddDataAccessCoreInternals();
      builder.AddDataAccessInternals();
      builder.AddMongoDbDataAccessInternals();
      builder.AddRavenDbDataAccessInternals();
      builder.AddSqlDataAccessInternals();
    

      builder.Register(ctx => ResolveCarAdapter(ctx)).As<IAdaptedDataRepository<ICarBase>>();
        
        //(ctx => builder.RegisterInstance(ResolveCarAdapter(ctx)));
      builder.Register(ctx => builder.RegisterInstance(ResolveCarUserAdapter(ctx)));
      builder.Register(ctx => builder.RegisterInstance(ResolveCarPictureAdapter(ctx)));
      builder.Register(ctx => builder.RegisterInstance(ResolveCarDocumentAdapter(ctx)));
      builder.Register(ctx => builder.RegisterInstance(ResolveCarDocumentHistoryAdapter(ctx)));

      //  builder.RegisterType<IAdaptedDataRepository<ICarBase>>().As(ctx =>
      //  {
      //    return ResolveCarAdapter(ctx);
      //  });

      //builder.Register(ctx =>
      //  {

      //  });

      return builder;
    }



  }
}