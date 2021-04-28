
using System;
using System.Linq;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
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
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      services.AddAutoMapper(assemblies);
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

    public static ContainerBuilder AddDataAccessServiceInternals(this ContainerBuilder container)
    {
      container.AddDataAccessManagerInternals();
      var assemblies = AppDomain.CurrentDomain.GetAssemblies();
      container.RegisterAutoMapper(assemblies);

      container.RegisterType<CarService>().As<ICarService>();
      container.RegisterType<CarUserService>().As<ICarUserService>();
      container.RegisterType<CarPictureService>().As<ICarPictureService>();
      container.RegisterType<CarDocumentService>().As<ICarDocumentService>();
      container.RegisterType<CarDocumentHistoryService>().As<ICarDocumentHistoryService>();

      container.RegisterType<CarServiceFactory>().As<ICarServiceFactory>().SingleInstance();
      container.RegisterType<CarUserServiceFactory>().As<ICarUserServiceFactory>().SingleInstance();
      container.RegisterType<CarPictureServiceFactory>().As<ICarPictureServiceFactory>().SingleInstance();
      container.RegisterType<CarDocumentServiceFactory>().As<ICarDocumentServiceFactory>().SingleInstance();
      container.RegisterType<CarDocumentHistoryServiceFactory>().As<ICarDocumentHistoryServiceFactory>().SingleInstance();

      container.RegisterType<ManagerService>().As<IManagerService>().SingleInstance();

      return container;
    }
  }
}