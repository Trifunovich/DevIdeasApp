using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharedCodeLibrary;

namespace DataAccess.Core.Abstractions
{
  public interface IDataRepository<T> : IDisposable where T : IDataModelBase
  {
    Task<T> GetById(string id, bool? isActive = true);

    Task<int?> DeleteById(string id);

    Task<int?> HardDeleteById(string id);

    Task<IEnumerable<T>> GetPage(PagingParameters pagingParameters, bool? isActive = true);

    Task<IEnumerable<T>> GetAll(bool? isActive = true);

    Task<IEnumerable<T>> GetAll(DateTime createdAfter, DateTime? createdBefore = null, bool? isActive = true);

    Task<T> GetByLabel(string label, bool? isActive = true);

    Task<int?> Insert(IEnumerable<T> records);

    Task<int?> Add(T record);

    Task<int?> DeleteByValue(T record);

    Task<int?> HardDeleteByValue(T record);

    Task<int?> UpdateRecord(T record);

    Task<int?> SaveChanges();

    Task<int?> Apply();

    void SetCancellationToken(CancellationTokenSource cancellationTokenSource);

    Task<int?> RevertAll();

  }
}
