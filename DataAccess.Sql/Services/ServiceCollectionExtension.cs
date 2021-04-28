using System;
using System.Collections.Generic;
using Autofac;
using DataAccess.Core.Abstractions;
using DataAccess.Sql.Abstractions;
using DataAccess.Sql.Context;
using DataAccess.Sql.Helpers;
using DataAccess.Sql.Models;
using DataAccess.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Sql.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddSqlDataAccessInternals (this IServiceCollection services)
    {
      services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
      services.AddSingleton( typeof(IDapperResolver<>), typeof(DapperResolver<>));

      ResolveRepos<Car>(services);
      ResolveRepos<CarPicture>(services);
      ResolveRepos<CarUser>(services);
      ResolveRepos<CarDocument>(services);
      ResolveRepos<CarDocumentHistory>(services);

      return services;
    }

    private static void ResolveRepos<T>(IServiceCollection services) where T : SqlDataModelBase
    {
      services.AddScoped(typeof(IDataRepository<T>), ConnectionHelper.Orm == Enums.OrmType.Dapper ? typeof(DapperDataRepository<T>) : typeof(EfDataRepository<T>));
    }

    private static void ResolveRepos<T>(ContainerBuilder builder) where T : SqlDataModelBase
    {
      builder.RegisterType(ConnectionHelper.Orm == Enums.OrmType.Dapper ? typeof(DapperDataRepository<T>) : typeof(EfDataRepository<T>)).As<IDataRepository<T>>();
    }

    public static ContainerBuilder AddSqlDataAccessInternals(this ContainerBuilder builder)
    {
      builder.RegisterType<EfContextFactory>().As<IEfContextFactory>().SingleInstance();
      builder.RegisterType(typeof(SqlConnectionFactory)).As(typeof(ISqlConnectionFactory)).SingleInstance();
      builder.RegisterGeneric(typeof(DapperResolver<>)).As(typeof(IDapperResolver<>)).SingleInstance();
      
      RegisterContext<SqlEfContext>(builder);

      ResolveRepos<Car>(builder);
      ResolveRepos<CarPicture>(builder);
      ResolveRepos<CarUser>(builder);
      ResolveRepos<CarDocument>(builder);
      ResolveRepos<CarDocumentHistory>(builder);

      return builder;
    }


    private static void RegisterContext<TContext>(ContainerBuilder builder)
      where TContext : DbContext
    {
      builder.Register(componentContext =>
        {
          var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
          var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
            .UseSqlite(ConnectionHelper.SqlConnectionString);

          return optionsBuilder.Options;
        }).As<DbContextOptions<TContext>>()
        .InstancePerLifetimeScope();

      builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
        .As<DbContextOptions>()
        .InstancePerLifetimeScope();

      builder.RegisterType<TContext>()
        .AsSelf()
        .InstancePerLifetimeScope();
    }
  }
}