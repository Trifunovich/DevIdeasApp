using DataServiceProvider.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Core.Abstractions;
using Microsoft.Extensions.Logging;
using SharedCodeLibrary;
using MapperSharedLibrary.Models;

namespace DataServiceProvider.Services
{
  abstract class DataAccessService<T, TInputDto, TOutputDto> : IDataAccessService<TInputDto, TOutputDto>
    where T : IDataModelBase
    where TInputDto : IInputDto
    where TOutputDto : IOutputDto
  {

    protected CancellationTokenSource TokenSource = new CancellationTokenSource();

    public virtual void Dispose()
    {
      TokenSource?.Dispose();
      Repository?.Dispose();
    }

    protected readonly IDataRepository<T> Repository;
    protected readonly IMapper Mapper;
    protected readonly ILogger<DataAccessService<T, TInputDto, TOutputDto>> Logger;

    protected DataAccessService(IDataRepository<T> repository, IMapper mapper, ILogger<DataAccessService<T, TInputDto, TOutputDto>> logger)
    {
      Repository = repository;
      Mapper = mapper;
      Logger = logger;
      Repository.SetCancellationToken(TokenSource);
    }

    public async Task SaveChanges()
    {
      await Repository?.SaveChanges();
    }

    protected void LogMappedOutput(int count)
    {
      Logger?.LogInformation("Mapped {count} {TypeName}s to {OutputDtoName}s", count, typeof(T).Name, typeof(TOutputDto).Name);
    }

    protected void LogMappedOneOutput()
    {
      Logger?.LogInformation("Mapped {TypeName} to {OutputDtoName}", typeof(T).Name, typeof(TOutputDto).Name);
    }

    protected void LogMappedFromInput(int count)
    {
      Logger?.LogInformation("Mapped {count} {InputDtoName}s to {TypeName}s", count, typeof(TInputDto).Name, typeof(T).Name);
    }

    protected void LogMappedOneFromInput()
    {
      Logger?.LogInformation("Mapped {InputDtoName} to {TypeName}", typeof(TInputDto).Name, typeof(T).Name);
    }

    protected void LogGotCount(int count)
    {
      Logger?.LogInformation("Got {Count} from {Name}", count, Repository.GetType().Name);
    }

    protected void LogGotOne(T one)
    {
      Logger?.LogInformation("Got {One} from {Name}", one, Repository.GetType().Name);
    }

    public async Task<TOutputDto> GetById(string id, bool? isActive = true)
    {
      Logger.LogInformation("Getting by Id: {Id}, if active: {IsActive} the type {TypeName}", id, isActive, typeof(T).Name);
      var found = await Repository.GetById(id, isActive);
      LogGotOne(found);
      var mapped = MapOneToOutput(found);
      return mapped;
    }

    public async Task<int?> DeleteById(string id)
    {
      return await LoggedIdInputActions(id, ActionType.SoftDeleting);
    }

    public async Task<int?> HardDeleteById(string id)
    {
      return await LoggedIdInputActions(id, ActionType.HardDeleting);
    }

    protected async Task<int?> LoggedIdInputActions(string id, ActionType type)
    {
      Tuple<string, string> actionStrings = GetActionTypeString(type);

      int? res;
      switch (type)
      {
        case ActionType.HardDeleting:
          res = await Repository.HardDeleteById(id);
          break;
        case ActionType.SoftDeleting:
          res = await Repository.DeleteById(id);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

      if (res != null)
      {
        Logger.LogInformation("{ActionType} {InputDtoName} from {RepoName}}", actionStrings.Item2, typeof(TInputDto).Name, Repository.GetType().Name);
      }

      return res;
    }

    public async Task<IEnumerable<TOutputDto>> GetPage(PagingParameters pagingParameters, bool? isActive = true)
    {
      Logger?.LogInformation("Getting paged data, Page size {PageSize}, Page position {PagePosition}, Position offset {PositionOffset} active:{isActive}", pagingParameters.PageSize, pagingParameters.FirstElementPosition, pagingParameters.Offset, isActive);
      var rawList = (await Repository.GetPage(pagingParameters, isActive)).ToList();
      LogGotCount(rawList.Count);
      var mapped = rawList.Select(x => Mapper.Map<TOutputDto>(x)).ToList();
      LogMappedOutput(rawList.Count);
      return mapped;
    }

    public virtual async Task<IEnumerable<TOutputDto>> GetAll(bool? isActive = true)
    {
      Logger?.LogInformation("Getting all, active:{isActive}", isActive);
      var rawList = (await Repository.GetAll(isActive)).ToList();
      LogGotCount(rawList.Count);
      var mapped = rawList.Select(x => Mapper.Map<TOutputDto>(x)).ToList();
      LogMappedOutput(rawList.Count);
      return mapped;
    }

    public virtual async Task<IEnumerable<TOutputDto>> GetAll(DateTime createdAfter, DateTime? createdBefore = null, bool? isActive = true)
    {
      Logger?.LogInformation("Getting all, active:{IsActive}, createdAfter:{CreatedAfter}, createdBefore:{CreatedBefore}", isActive, createdAfter, createdBefore);
      var rawList = (await Repository.GetAll(createdAfter, createdBefore, isActive)).ToList();
      return await Task.FromResult(MapAllToOutput(rawList));
    }

    protected IEnumerable<TOutputDto> MapAllToOutput(List<T> rawList)
    {
      LogGotCount(rawList.Count);
      var mapped = rawList.Select(MapOneToOutput).ToList();
      LogMappedOutput(mapped.Count);
      return mapped;
    }

    protected TOutputDto MapOneToOutput(T raw)
    {
      LogGotOne(raw);
      var mapped = Mapper.Map<TOutputDto>(raw);
      LogMappedOneOutput();
      return mapped;
    }

    protected T MapOneFromInput(TInputDto raw, bool logIt)
    {
      var mapped = Mapper.Map<T>(raw);

      if (logIt)
      {
        LogMappedOneFromInput();
      }

      return mapped;
    }

    protected IEnumerable<T> MapAllFromInput(List<TInputDto> rawList)
    {
      var mapped = rawList.Select(x => MapOneFromInput(x, false));
      var dataModels = mapped.ToList();
      LogMappedFromInput(dataModels.Count);
      return dataModels; ;
    }

    public virtual async Task<TOutputDto> GetByLabel(string label, bool? isActive = true)
    {
      Logger?.LogInformation("Getting all, active:{IsActive}, label:{Label}", isActive, label);
      var raw = await Repository.GetByLabel(label, isActive);
      return await Task.FromResult(MapOneToOutput(raw));
    }

    public async Task<int?> Insert(IEnumerable<TInputDto> records)
    {
      var inputDtos = records.ToList();
      Logger?.LogInformation("Inserting {Count} of {TypeName} to {RepoName}", inputDtos.Count(), typeof(T).Name, Repository.GetType().Name);
      var mapped = MapAllFromInput(inputDtos);
   
      var res = await Repository.Insert(mapped);

      if (res  != null)
      {
        Logger?.LogInformation("Inserted {Count} of {TypeName} /n", inputDtos.Count(), typeof(T).Name);
      }

      return res;
    }

    public async Task<int?> Add(TInputDto record)
    {
      Logger?.LogInformation("Adding {InputDtoName} to {RepoName}", typeof(TInputDto).Name, Repository.GetType().Name);
      var mapped = MapOneFromInput(record, true);
     
      var res = await Repository.Add(mapped);

      if (res  != null)
      {
        Logger?.LogInformation("Added {TypeName} to {RepoName}", typeof(T).Name, Repository.GetType().Name);
      }

      return res;
    }


    public async Task<int?> DeleteByValue(TInputDto record)
    {
      return  await LoggedInputActions(record, ActionType.SoftDeleting);
    }

    public async Task<int?> HardDeleteByValue(TInputDto record)
    {
      return await LoggedInputActions(record, ActionType.HardDeleting);
    }

    public async Task<int?> UpdateRecord(TInputDto record)
    {
      return  await LoggedInputActions(record, ActionType.Updating);
    }

    public Task ThrowError(string message)
    {
      throw new ApplicationException(message);
    }

    protected async Task<int?> LoggedInputActions(TInputDto record, ActionType type)
    {
      Tuple<string, string> actionStrings = GetActionTypeString(type);
      var dataModel = MapOneFromInput(record, true);
      int? res;
      switch (type)
      {
        case ActionType.Updating:
          res = await Repository.UpdateRecord(dataModel);
          break;
        case ActionType.HardDeleting:
          res = await Repository.HardDeleteByValue(dataModel);
          break;
        case ActionType.SoftDeleting:
          res = await Repository.DeleteByValue(dataModel);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

      if (res  != null)
      {
        Logger?.LogInformation("{ActionType} {InputDtoName} from {RepoName}}", actionStrings.Item2, typeof(TInputDto).Name, Repository.GetType().Name);
      }
     
      return res;
    }

    protected Tuple<string,string> GetActionTypeString(ActionType type)
    {
      switch (type)
      {
        case ActionType.Updating:
          return new Tuple<string, string>("Updating", "Updated");
        case ActionType.HardDeleting:
          return new Tuple<string, string>("Hard deleting", "Hard deleted");
        case ActionType.SoftDeleting:
          return new Tuple<string, string>("Soft deleting", "Soft deleted");
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }

    protected enum ActionType
    {
      Updating, HardDeleting, SoftDeleting
    }
  }
}