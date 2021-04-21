using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddDataAccessInternals (this IServiceCollection services)
    {
     
      return services;
    }
  }
}