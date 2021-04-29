using System;
using System.IO;
using System.Reflection;
using DataAccess.Sql.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Sql.Context
{
  /// <summary>
  /// This is needed for package manager console
  /// </summary>
  internal class SqlEfContextFactory : IDesignTimeDbContextFactory<SqlEfContext>
  {
    SqlEfContext IDesignTimeDbContextFactory<SqlEfContext>.CreateDbContext(string[] args)
    {
      var builder = new DbContextOptionsBuilder<SqlEfContext>();
      string path = VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName + @"\DataAccess.Sql\LocalSqliteDb.db";
   
      File.WriteAllText(@"F:\Logs\WriteText.txt", path);

      builder.UseSqlServer($@"Data Source=localhost\SQLEXPRESS;Initial Catalog=TestDb;Integrated Security=True");
      return new SqlEfContext(builder.Options, null);
    }
  }
}