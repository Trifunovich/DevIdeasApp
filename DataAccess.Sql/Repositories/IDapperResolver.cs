using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DataAccess.Sql.Abstractions;

namespace DataAccess.Sql.Repositories
{
  public interface IDapperResolver<T> where T : SqlDataModelBase
  {
    IDbConnection Connection { get; }

    Task<int> ExecuteQuery<P>(string query, P param);

    Task<List<T>> GetResults<P>(string query, P param);

    Task<long> Insert(IList<T> records);

    Task<int> InsertOne(T record);

    Task<bool> Update(T record);

    DbTransaction SetTransactionAndOpenConnection();

    void CloseEverything();
  }
}