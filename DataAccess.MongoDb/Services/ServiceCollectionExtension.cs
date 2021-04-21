using System;
using DataAccess.Core.Abstractions;
using DataAccess.MongoDb.Abstractions;
using DataAccess.MongoDb.Context;
using DataAccess.MongoDb.Models;
using DataAccess.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.MongoDb.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddMongoDbDataAccessInternals (this IServiceCollection services)
    {
      services.AddSingleton(typeof(IMongoDbContextFactory<>),typeof(MongoDbContextFactory<>));
      ResolveRepos<MongoCar>(services);
      ResolveRepos<MongoCarPicture>(services);
      ResolveRepos<MongoCarUser>(services);
      ResolveRepos<MongoCarDocument>(services);
      ResolveRepos<MongoCarDocumentHistory>(services);

      return services;
    }

    private static void ResolveRepos<T>(IServiceCollection services) where T : MongoDbDataModelBase
    {
      services.AddScoped<IDataRepository<T>, MongoDbRepository<T>>();
    }
  }
}