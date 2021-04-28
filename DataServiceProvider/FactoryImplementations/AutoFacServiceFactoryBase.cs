using Autofac;

namespace DataServiceProvider.FactoryImplementations
{
  internal class AutoFacServiceFactoryBase : IServiceFactoryBase
  {
    protected IComponentContext ServiceProvider;

    public AutoFacServiceFactoryBase(IComponentContext serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }
  }
}