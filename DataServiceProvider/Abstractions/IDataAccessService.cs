using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapperSharedLibrary.Models;
using SharedCodeLibrary;

namespace DataServiceProvider.Abstractions
{
  public interface IDataAccessService<in TInputDto, TOutputDto> : IDisposable, IChangeAppliable 
    where TInputDto : IInputDto
    where TOutputDto : IOutputDto
  {
    Task<TOutputDto> GetById(string id, bool? isActive = true);
    Task<int?> DeleteById(string id);
    Task<int?> HardDeleteById(string id);

    Task<IEnumerable<TOutputDto>> GetPage(PagingParameters pagingParameters, bool? isActive = true);

    Task<IEnumerable<TOutputDto>> GetAll(bool? isActive = true);

    Task<IEnumerable<TOutputDto>> GetAll(DateTime createdAfter, DateTime? createdBefore = null, bool? isActive = true);

    Task<TOutputDto> GetByLabel(string label, bool? isActive = true);

    Task<int?> Insert(IEnumerable<TInputDto> records);

    Task<int?> Add(TInputDto record);

    Task<int?> DeleteByValue(TInputDto record);

    Task<int?> HardDeleteByValue(TInputDto record);

    Task<int?> UpdateRecord(TInputDto record);

    Task ThrowError(string message);
  }
}