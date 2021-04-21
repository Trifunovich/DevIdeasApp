using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Core.Abstractions;
using SharedCodeLibrary;

namespace DataAccess.Manager.TestingStuff
{
  class AdaptedDataRepository<T, U> : IAdaptedDataRepository<T> 
    where T : IDataModelBase
    where U : class, IDataModelBase, T
  {
    private readonly IDataRepository<U> _baseRepository;
    private readonly IMapper _mapper;

    public AdaptedDataRepository(IDataRepository<U> baseRepository,
      IMapper mapper)
    {
      _baseRepository = baseRepository;
      _mapper = mapper;
    }

    public void Dispose()
    {
      _baseRepository?.Dispose();
    }

    public async Task<T> GetById(string id, bool? isActive)
    {
      return  await _baseRepository.GetById(id, isActive);
    }

    public async  Task<int?> DeleteById(string id)
    {
      return await _baseRepository.DeleteById(id);
    }

    public async  Task<int?> HardDeleteById(string id)
    {
      return await _baseRepository.HardDeleteById(id);
    }

    public async  Task<IEnumerable<T>> GetPage(PagingParameters pagingParameters, bool? isActive)
    {
      return await _baseRepository.GetPage(pagingParameters, isActive);
    }

    public async  Task<IEnumerable<T>> GetAll(bool? isActive)
    {
      return await _baseRepository.GetAll(isActive);
    }

    public async  Task<IEnumerable<T>> GetAll(DateTime createdAfter, DateTime? createdBefore = null, bool? isActive = true)
    {
      return await _baseRepository.GetAll(createdAfter, createdBefore, isActive);
    }

    public async  Task<T> GetByLabel(string label, bool? isActive)
    {
      return await _baseRepository.GetByLabel(label, isActive);
    }

    public async  Task<int?> Insert(IEnumerable<T> records)
    {
      var mapped = records.Select(x => _mapper.Map<U>(x));
      return await _baseRepository.Insert(mapped);
    }

    public async  Task<int?> Add(T record)
    {
      var mapped = _mapper.Map<U>(record);
      return await _baseRepository.Add(mapped);
    }

    public async  Task<int?> DeleteByValue(T record)
    {
      var mapped = _mapper.Map<U>(record);
      return await _baseRepository.DeleteByValue(mapped);
    }

    public async  Task<int?> HardDeleteByValue(T record)
    {
      var mapped = _mapper.Map<U>(record);
      return await _baseRepository.HardDeleteByValue(mapped);
    }

    public async  Task<int?> UpdateRecord(T record)
    {
      var mapped = _mapper.Map<U>(record);
      return await _baseRepository.UpdateRecord(mapped);
    }

    public async Task<int?> SaveChanges()
    {
      return await _baseRepository.SaveChanges();
    }

    public async  Task<int?> Apply()
    {
      return await _baseRepository.Apply();
    }

    public void SetCancellationToken(CancellationTokenSource cancellationTokenSource)
    {
      _baseRepository.SetCancellationToken(cancellationTokenSource); 
    }

    public async Task<int?> RevertAll()
    {
      return await _baseRepository.RevertAll();
    }
  }
}