using System;
using Autofac;
using MongoDB.Bson.Serialization;

namespace DataServiceProvider.FactoryImplementations
{
  internal class ServiceFactoryBase : IServiceFactoryBase
  {
    protected IServiceProvider ServiceProvider;

    public ServiceFactoryBase(IServiceProvider serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }
  }

  internal class AutoFacServiceFactoryBase : IServiceFactoryBase
  {
    protected IComponentContext ServiceProvider;

    public AutoFacServiceFactoryBase(IComponentContext serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }
  }
}