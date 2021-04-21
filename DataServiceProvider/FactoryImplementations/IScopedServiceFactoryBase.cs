﻿namespace DataServiceProvider.FactoryImplementations
{
  public interface IScopedServiceFactoryBase<out T>
  {
    T CreateService();
  }
}