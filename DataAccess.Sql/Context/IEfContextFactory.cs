namespace DataAccess.Sql.Context
{
  internal interface IEfContextFactory
  {
    SqlEfContext CreateEfContext();
  }
}