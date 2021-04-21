using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthenticationLibrary.Context
{
  /// <summary>
  /// This is needed for package manager console
  /// </summary>
  internal class SqlEfContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
    {
      var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
      builder.UseSqlite("Data Source=AuthenticationDb.db;;");
      return new ApplicationDbContext(builder.Options);
    }
  }
}