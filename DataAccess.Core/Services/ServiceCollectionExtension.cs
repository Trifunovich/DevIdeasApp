using DataAccess.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Core.Services
{
  public static class ServiceCollectionExtension
  {
    /// <summary>
    /// Adds MS DI dependencies from this library
    /// </summary>
    public static IServiceCollection AddDataAccessCoreInternals (this IServiceCollection services)
    {
      services.AddScoped<IRepositoryInputValidator, RepositoryInputValidator>();
      return services;
    }
  }
}