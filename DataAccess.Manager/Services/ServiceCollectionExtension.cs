using System;
using DataAccess.Core.Abstractions;
using DataAccess.Core.Services;
using DataAccess.Manager.Helpers;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataAccess.MongoDb.Models;
using DataAccess.MongoDb.Repositories;
using DataAccess.MongoDb.Services;
using DataAccess.Services;
using DataAccess.Sql.Models;
using DataAccess.Sql.Repositories;
using DataAccess.Sql.Services;
using DateAccess.RavenDb.Models;
using DateAccess.RavenDb.Repositories;
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
    

    
  }
}