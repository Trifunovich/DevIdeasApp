using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Models;
using Microsoft.Extensions.Logging;

namespace WebApi.Helpers
{
  public class ErrorHandlingHelper
  {
    private readonly ILogger<ErrorHandlingHelper> _logger;

    public ErrorHandlingHelper(ILogger<ErrorHandlingHelper> logger)
    {
      _logger = logger;
    }

    public async Task<ResponseModel> HandleAsync(Task<ResponseModel> task)
    {
      try
      {
        var res = await task;
        _logger.LogTrace("Task successfully ran");
        return res;
      }
      catch (Exception e)
      {
        _logger.LogError("Error happened while handling task {taskId}", task.Id);
        _logger.LogError(e.Message);
        return new ResponseModel(ResponseType.OperationFailed);
      }
    }

    public async Task<ResponseModel> HandleTaskListAsync(IList<Task<ResponseModel>> tasks, bool sequential = true)
    {
      try
      {
        List<ResponseModel> responses = new List<ResponseModel>();

        if (sequential)
        {
          foreach (var task in tasks)
          {
            responses.Add(await task);
          }
        }
        else
        {
          await Task.WhenAll(tasks);
          responses = tasks.Select(t => t.Result).ToList();
        }

        return responses.All(r => r.ResponseType == ResponseType.Successful)
          ? new ResponseModel(ResponseType.Successful) {ResponseData = responses}
          : new ResponseModel(ResponseType.OperationFailed) {ResponseData = responses};
      }
      catch (Exception e)
      {
        _logger.LogError("Error happened while handling {count} tasks", tasks?.Count ?? 0);
        _logger.LogError(e.Message);
        return new ResponseModel(ResponseType.OperationFailed);
      }
    }

    public async Task<ResponseModel> RunStandardComplexCode(Func<Task<ResponseModel>> code)
    {
      try
      {
        return await code?.Invoke();
      }
      catch (Exception e)
      {
        _logger.LogError("Error happened while handling function");
        _logger.LogError(e.Message);
        return new ResponseModel(ResponseType.OperationFailed);
      }
    }
  }
}