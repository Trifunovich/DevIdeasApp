using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using static DataAccess.Manager.Enums;

namespace DataAccess.Manager.Helpers
{
  internal class ConnectionHelper
  {
    public static readonly string CurrDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

    private static readonly string SettingsFileName = @"data_access_manager_appsettings.json";

    private static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
      .SetBasePath(CurrDir)
      .AddJsonFile(SettingsFileName)
      .Build();
    public static OrmType Orm => !ConfigRoot?.GetSection("PreferredOrm").Value?.Equals(OrmType.Dapper.ToString(), StringComparison.InvariantCultureIgnoreCase) ?? true
      ? OrmType.EfCore
      : OrmType.Dapper;

  

    public static DatabaseProvider GetDataBaseProvider()
    {
      string configValue = ConfigRoot?.GetSection("DatabaseProvider").Value;

      DatabaseProvider value;
      switch (configValue)
      {
        case "Sql":
          value = DatabaseProvider.Sql;
          break;
        case "RavenDb":
          value = DatabaseProvider.RavenDb;
          break;
        case "MongoDb":
          value = DatabaseProvider.MongoDb;
          break;
        default:
          value = DatabaseProvider.RavenDb;
          break;
      }

      return value;
    }
  }
}