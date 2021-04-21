using DataServiceProvider.FactoryImplementations;

namespace DataServiceProvider.Abstractions
{
  public interface ICarUserServiceFactory : IServiceFactoryBase, IScopedServiceFactoryBase<ICarUserService>
  {

  }

}