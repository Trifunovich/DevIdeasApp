namespace DataAccess.Sql.Context
{
  internal class EfContextFactory : IEfContextFactory
  {
    private readonly SqlEfContext _dbContext;

    public EfContextFactory(SqlEfContext dbContext)
    {
      _dbContext = dbContext;
    }

    public SqlEfContext CreateEfContext()
    {
      return _dbContext;
    }
  }
}