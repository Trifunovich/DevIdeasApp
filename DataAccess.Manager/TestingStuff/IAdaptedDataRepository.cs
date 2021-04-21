using DataAccess.Core.Abstractions;
using DataAccess.Core.Validation;
using Microsoft.Extensions.Logging;

namespace DataAccess.Manager.TestingStuff
{
  /// <summary>
  /// Adapter pattern - we are adapting the implementations of repos for the communication with the outer world
  /// without knowing the concrete type 
  /// </summary>
  public interface IAdaptedDataRepository<T> : IDataRepository<T> where T : IDataModelBase
  {
    
  }
}