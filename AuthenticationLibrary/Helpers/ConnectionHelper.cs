using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace AuthenticationLibrary.Helpers
{
  internal class ConnectionHelper
  {
    public static readonly string CurrDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

    private static readonly string SettingsFileName = @"authentication_library_appsettings.json";

    public static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
      .SetBasePath(CurrDir)
      .AddJsonFile(SettingsFileName)
      .Build();


    public static string SqlConnectionString => ConfigRoot.GetConnectionString("AuthDb");

    public ConnectionHelper()
    {

    }
  }
}