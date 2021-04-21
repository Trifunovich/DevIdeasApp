using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Core.Abstractions;
using DataAccess.Manager.TestingStuff;
using DataAccess.Sql.Abstractions;
using FakerSharedLibrary.FakeAbstractions;
using SharedCodeLibrary;

namespace DataServiceProvider.Tests
{
  /// <summary>
  /// manual mock for the repository, instead of moq framework
  /// </summary>
  internal class DataRepoMock<T, TU> : IAdaptedDataRepository<T> where T : IDataModelBase
  where TU : FakeDataModelBase, T
  {
    private List<T> _fakeRepo = new List<T>();

    public List<T> GetRepoObjects => _fakeRepo;

    public void FillWithFakes()
    {
      var faker = new FakerSharedLibrary.Fakers.DefaultFake<TU>();
      _fakeRepo.AddRange(faker.Generate(150));
    }

    public void ClearAll()
    {
      _fakeRepo.Clear();
    }

    public void Dispose()
    {
      //
    }

    public Task<T> GetById(string id, bool? isActive)
    {
      return Task.FromResult(_fakeRepo.FirstOrDefault(x => x.GetId.Equals(id) && (isActive == null || x.IsActive == isActive.Value)));
    }

    public async Task<int?> DeleteById(string id)
    {
      var t = await GetById(id, null);
      t.IsActive = false;
      return 1;
    }

    public async Task<int?> HardDeleteById(string id)
    {
      var t = await GetById(id, null);
      _fakeRepo.Remove(t);
      return 1;
    }

    public async Task<IEnumerable<T>> GetPage(PagingParameters pagingParameters, bool? isActive)
    {
      return _fakeRepo.Where(x => isActive == null || x.IsActive == isActive).Skip(pagingParameters.FirstElementPosition).Take(pagingParameters.PageSize).ToList();
    }

    public async Task<IEnumerable<T>> GetAll(bool? isActive)
    {
      return _fakeRepo.Where(x => isActive == null || x.IsActive == isActive);
    }

    public async Task<IEnumerable<T>> GetAll(DateTime createdAfter, DateTime? createdBefore = null, bool? isActive = true)
    {
      return _fakeRepo.Where(x => (isActive == null || x.IsActive == isActive) && x.CreatedOn >= createdAfter && x.CreatedOn <= (createdBefore ?? DateTime.Now));
    }

    public async Task<T> GetByLabel(string label, bool? isActive)
    {
      return _fakeRepo.FirstOrDefault(x => (isActive == null || x.IsActive == isActive) && x.Label.Equals(label));
    }

    public Task<int?> Insert(IEnumerable<T> records)
    {
      _fakeRepo.AddRange(records);
      return Task.FromResult<int?>(1);
    }

    public Task<int?> Add(T record)
    {
      _fakeRepo.Add(record);
      return Task.FromResult<int?>(1);
    }

    public Task<int?> DeleteByValue(T record)
    {
      record.IsActive = false;
      return Task.FromResult<int?>(1);
    }

    public Task<int?> HardDeleteByValue(T record)
    {
      _fakeRepo.Remove(record);
      return Task.FromResult<int?>(1);
    }

    public Task<int?> UpdateRecord(T record)
    {
      var old = _fakeRepo.FirstOrDefault(x => x.GetId.Equals(record.GetId));
      _fakeRepo.Remove(old);
      _fakeRepo.Add(record);
      return Task.FromResult<int?>(1);
    }

    public Task<int?> SaveChanges()
    {
      return Task.FromResult<int?>(1);
    }

    public Task<int?> Apply()
    {
      return Task.FromResult<int?>(1);
    }

    public void SetCancellationToken(CancellationTokenSource cancellationTokenSource)
    {
      //
    }

    public Task<int?> RevertAll()
    {
      ClearAll();
      FillWithFakes();
      return Task.FromResult<int?>(1);
    }
  }
}