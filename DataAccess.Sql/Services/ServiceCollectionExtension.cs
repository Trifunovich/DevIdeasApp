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
  }
}