using Autofac;

namespace DataServiceProvider.FactoryImplementations
{
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