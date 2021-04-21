using System;
using AutoMapper;
using DataAccess.Core.Abstractions;
using DataAccess.Manager.Helpers;
using DataAccess.Manager.TestingStuff;
using DataAccess.Models;
using DataAccess.MongoDb.Models;
using DataAccess.Sql.Models;
using DateAccess.RavenDb.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Manager.Services
{
  internal static class DiResolver
  {

    public static DatabaseProvider Provider => ConnectionHelper.GetDataBaseProvider();
    public static Enums.OrmType OrmType => ConnectionHelper.Orm;

    public static IAdaptedDataRepository<ICarBase> ResolveCarAdapter(IServiceProvider sp)
    {
      switch (Provider)
      {
        case DatabaseProvider.MongoDb:
          return GetAdapterDataRepository<ICarBase, MongoCar>(sp);
        case DatabaseProvider.RavenDb:
          return GetAdapterDataRepository<ICarBase, RavenCar>(sp);
        case DatabaseProvider.Sql:
          return GetAdapterDataRepository<ICarBase, SqlCar>(sp);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static IAdaptedDataRepository<ICarPictureBase> ResolveCarPictureAdapter(IServiceProvider sp)
    {
      switch (Provider)
      {
        case DatabaseProvider.MongoDb:
          return GetAdapterDataRepository<ICarPictureBase, MongoCarPicture>(sp);
        case DatabaseProvider.RavenDb:
          return GetAdapterDataRepository<ICarPictureBase, RavenCarPicture>(sp);
        case DatabaseProvider.Sql:
          return GetAdapterDataRepository<ICarPictureBase, SqlCarPicture>(sp);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static IAdaptedDataRepository<ICarUserBase> ResolveCarUserAdapter(IServiceProvider sp)
    {
      switch (Provider)
      {
        case DatabaseProvider.MongoDb:
          return GetAdapterDataRepository<ICarUserBase, MongoCarUser>(sp);
        case DatabaseProvider.RavenDb:
          return GetAdapterDataRepository<ICarUserBase, RavenCarUser>(sp);
        case DatabaseProvider.Sql:
          return GetAdapterDataRepository<ICarUserBase, SqlCarUser>(sp);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static IAdaptedDataRepository<ICarDocumentBase> ResolveCarDocumentAdapter(IServiceProvider sp)
    {
      switch (Provider)
      {
        case DatabaseProvider.MongoDb:
          return GetAdapterDataRepository<ICarDocumentBase, MongoCarDocument>(sp);
        case DatabaseProvider.RavenDb:
          return GetAdapterDataRepository<ICarDocumentBase, RavenCarDocument>(sp);
        case DatabaseProvider.Sql:
          return GetAdapterDataRepository<ICarDocumentBase, SqlCarDocument>(sp);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static IAdaptedDataRepository<ICarDocumentHistoryBase> ResolveCarDocumentHistoryAdapter(IServiceProvider sp)
    {
      switch (Provider)
      {
        case DatabaseProvider.MongoDb:
          return GetAdapterDataRepository<ICarDocumentHistoryBase, MongoCarDocumentHistory>(sp);
        case DatabaseProvider.RavenDb:
          return GetAdapterDataRepository<ICarDocumentHistoryBase, RavenCarDocumentHistory>(sp);
        case DatabaseProvider.Sql:
          return GetAdapterDataRepository<ICarDocumentHistoryBase, SqlCarDocumentHistory>(sp);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }


    private static AdaptedDataRepository<T, U> GetAdapterDataRepository<T, U>(IServiceProvider sp)
      where T : IDataModelBase
      where U : class, IDataModelBase, T
    {
      var resolver = sp.GetService<IDataRepository<U>>();
      var mapper = sp.GetService<IMapper>();
      return new AdaptedDataRepository<T, U>(resolver, mapper);
    }

  }
}