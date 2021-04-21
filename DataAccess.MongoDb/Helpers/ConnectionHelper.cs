using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DataAccess.MongoDb.Helpers
{
  internal class ConnectionHelper
  {
    public static readonly string CurrDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

    private static readonly string SettingsFileName = @"data_access_mongodb_appsettings.json";

    private static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
      .SetBasePath(CurrDir)
      .AddJsonFile(SettingsFileName)
      .Build();


    public static string MongoDbConnectionString => ConfigRoot.GetConnectionString("MongoDb");

  }
}