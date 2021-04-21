using System;
using Microsoft.Extensions.DependencyInjection;

namespace SharedCodeLibrary.FactoryImplementations
{
  public abstract class ScopedServiceFactoryBase<T> : ServiceFactoryBase, IScopedServiceFactoryBase<T>
  {
    protected IServiceScope Scope => ServiceProvider.CreateScope();

    protected ScopedServiceFactoryBase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public virtual T CreateService()
    {
      return Scope.ServiceProvider.GetRequiredService<T>();
    }
  }
}