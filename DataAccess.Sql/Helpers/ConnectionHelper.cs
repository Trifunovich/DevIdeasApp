﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Sql.Helpers
{
  internal class ConnectionHelper
  {
    public static readonly string CurrDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

    public static readonly string SettingsFileName = @"data_access_appsettings.json";

    private static readonly IConfigurationRoot ConfigRoot = new ConfigurationBuilder()
      .SetBasePath(CurrDir)
      .AddJsonFile(SettingsFileName)
      .Build();

    public static Enums.OrmType Orm => !ConfigRoot?.GetSection("PreferredOrm").Value?.Equals(Enums.OrmType.Dapper.ToString(), StringComparison.InvariantCultureIgnoreCase) ?? true
      ? Enums.OrmType.EfCore
      : Enums.OrmType.Dapper;

    public static string SqlConnectionString => ConfigRoot.GetConnectionString("MSSql");
    
  }
}