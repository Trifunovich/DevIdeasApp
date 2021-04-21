using System;
using Autofac;
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

    private static void ResolveRepos<T>(ContainerBuilder builder) where T : MongoDbDataModelBase
    {
      builder.RegisterType<MongoDbRepository<T>>().As<IDataRepository<T>>();
    }

    public static ContainerBuilder AddMongoDbDataAccessInternals(this ContainerBuilder builder)
    {
      builder.RegisterGeneric(typeof(MongoDbContextFactory<>)).As(typeof(IMongoDbContextFactory<>)).SingleInstance();
      ResolveRepos<MongoCar>(builder);
      ResolveRepos<MongoCarPicture>(builder);
      ResolveRepos<MongoCarUser>(builder);
      ResolveRepos<MongoCarDocument>(builder);
      ResolveRepos<MongoCarDocumentHistory>(builder);

      return builder;
    }
  }
}