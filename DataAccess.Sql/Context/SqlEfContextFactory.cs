using System;
using System.IO;
using System.Linq;
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
      builder.UseSqlite("Data Source=LocalSqliteDb.db;");
      return new SqlEfContext(builder.Options, null);
    }
  }
}