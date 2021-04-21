using DataAccess.Sql.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
      builder.UseSqlServer(ConnectionHelper.SqlConnectionString);
      return new SqlEfContext(builder.Options, null);
    }
  }
}