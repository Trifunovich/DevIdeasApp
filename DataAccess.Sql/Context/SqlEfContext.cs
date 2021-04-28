using System;
using System.IO;
using System.Reflection;
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
      //optionsBuilder.UseSqlite(_connectionFactory?.CreateSqlDbConnection()?.ConnectionString);
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarUser> CarUsers { get; set; }
    public DbSet<CarDocument> CarDocuments { get; set; }
    public DbSet<CarDocumentHistory> CarDocumentHistories { get; set; }
    public DbSet<CarPicture> CarPictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<CarCarUser>()
        .HasKey(bc => new { bc.CarId, bc.CarUserId });
      modelBuilder.Entity<CarCarUser>()
        .HasOne(bc => bc.Car)
        .WithMany(b => b.CarCarUsers)
        .HasForeignKey(bc => bc.CarId);
      modelBuilder.Entity<CarCarUser>()
        .HasOne(bc => bc.CarUser)
        .WithMany(c => c.CarCarUsers)
        .HasForeignKey(bc => bc.CarUserId);
    }
  }
}
