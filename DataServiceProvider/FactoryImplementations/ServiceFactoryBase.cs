using System;
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
}