using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace DataServiceProvider.FactoryImplementations
{
  internal class ScopedServiceFactoryBase<T> : ServiceFactoryBase, IScopedServiceFactoryBase<T>
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

  internal class AutoFacScopedServiceFactoryBase<T> : AutoFacServiceFactoryBase, IScopedServiceFactoryBase<T>
  {

    protected AutoFacScopedServiceFactoryBase(IComponentContext serviceProvider) : base(serviceProvider)
    {
    }

    public virtual T CreateService()
    {
      return ServiceProvider.Resolve<T>();
    }
  }
}