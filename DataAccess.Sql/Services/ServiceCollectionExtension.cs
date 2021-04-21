using Autofac;
using DataAccess.Core.Abstractions;
using DataAccess.Sql.Abstractions;
using DataAccess.Sql.Context;
using DataAccess.Sql.Helpers;
using DataAccess.Sql.Models;
using DataAccess.Sql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Sql.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddSqlDataAccessInternals (this IServiceCollection services)
    {
      services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
      services.AddSingleton( typeof(IDapperResolver<>), typeof(DapperResolver<>));

      ResolveRepos<SqlCar>(services);
      ResolveRepos<SqlCarPicture>(services);
      ResolveRepos<SqlCarUser>(services);
      ResolveRepos<SqlCarDocument>(services);
      ResolveRepos<SqlCarDocumentHistory>(services);

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
      builder.RegisterType(typeof(SqlConnectionFactory)).As(typeof(ISqlConnectionFactory)).SingleInstance();
      builder.RegisterGeneric(typeof(DapperResolver<>)).As(typeof(IDapperResolver<>)).SingleInstance();
      ResolveRepos<SqlCar>(builder);
      ResolveRepos<SqlCarPicture>(builder);
      ResolveRepos<SqlCarUser>(builder);
      ResolveRepos<SqlCarDocument>(builder);
      ResolveRepos<SqlCarDocumentHistory>(builder);

      return builder;
    }
  }
}