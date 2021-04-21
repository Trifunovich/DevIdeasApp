using DataAccess.Sql.Helpers;
using DataAccess.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Sql.Context
{
  class SqlEfContext : DbContext
  {
    private readonly ISqlConnectionFactory _connectionFactory;

    public SqlEfContext(DbContextOptions options, ISqlConnectionFactory connectionFactory) : base(options)
    {
      _connectionFactory = connectionFactory;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlServer(_connectionFactory?.CreateSqlDbConnection()?.ConnectionString ?? ConnectionHelper.SqlConnectionString);
    }

    public DbSet<SqlCar> Cars { get; set; }
    public DbSet<SqlCarUser> CarUsers { get; set; }
    public DbSet<SqlCarDocument> CarDocuments { get; set; }
    public DbSet<SqlCarDocumentHistory> CarDocumentHistories { get; set; }
    public DbSet<SqlCarPicture> CarPictures { get; set; }
  }
}
