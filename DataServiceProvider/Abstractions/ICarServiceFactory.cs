using DataServiceProvider.FactoryImplementations;

namespace DataServiceProvider.Abstractions
{
  public interface ICarServiceFactory : IServiceFactoryBase, IScopedServiceFactoryBase<ICarService>
  {
  }
}