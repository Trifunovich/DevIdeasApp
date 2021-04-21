using System;

namespace SharedCodeLibrary.FactoryImplementations
{
  public abstract class ServiceFactoryBase : IServiceFactoryBase
  {
    protected IServiceProvider ServiceProvider;

    public ServiceFactoryBase(IServiceProvider serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }
  }
}