using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DateAccess.RavenDb.Helpers
{
  internal class ConnectionHelper
  {
    public static readonly string CurrDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

    private static readonly string SettingsFileName = @"data_access_appsettings.json";

    private static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
      .SetBasePath(CurrDir)
      .AddJsonFile(SettingsFileName)
      .Build();
  }
}