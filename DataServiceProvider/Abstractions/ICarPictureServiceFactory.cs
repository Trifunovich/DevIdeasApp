using DataServiceProvider.FactoryImplementations;

namespace DataServiceProvider.Abstractions
{
  public interface ICarPictureServiceFactory : IServiceFactoryBase, IScopedServiceFactoryBase<ICarPictureService>
  {
  }
}