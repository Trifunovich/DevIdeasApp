
using System;
using DataAccess.Manager.Services;
using DataServiceProvider.Abstractions;
using DataServiceProvider.FactoryImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace DataServiceProvider.Services
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddDataAccessServiceInternals (this IServiceCollection services)
    {
      services.AddDataAccessManagerInternals();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddScoped<ICarService, CarService>();
      services.AddScoped<ICarUserService, CarUserService>();
      services.AddScoped<ICarPictureService, CarPictureService>();
      services.AddScoped<ICarDocumentService, CarDocumentService>();
      services.AddScoped<ICarDocumentHistoryService, CarDocumentHistoryService>();

      services.AddSingleton<ICarUserServiceFactory, CarUserServiceFactory>();
      services.AddSingleton<ICarServiceFactory, CarServiceFactory>();
      services.AddSingleton<ICarPictureServiceFactory, CarPictureServiceFactory>();
      services.AddSingleton<ICarDocumentServiceFactory, CarDocumentServiceFactory>();
      services.AddSingleton<ICarDocumentHistoryServiceFactory, CarDocumentHistoryServiceFactory>();

      services.AddSingleton<IManagerService, ManagerService>();
      return services;
    }
  }
}