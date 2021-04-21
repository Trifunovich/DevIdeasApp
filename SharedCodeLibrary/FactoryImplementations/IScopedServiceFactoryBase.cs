namespace SharedCodeLibrary.FactoryImplementations
{
  public interface IScopedServiceFactoryBase<out T>
  {
    T CreateService();
  }
}