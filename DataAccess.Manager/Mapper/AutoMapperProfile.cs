using System;
using AutoMapper;
using DataAccess.Core.Abstractions;
using DataAccess.Manager.Helpers;
using DataAccess.Manager.Services;
using DataAccess.Models;
using DataAccess.MongoDb.Models;
using DataAccess.Sql.Models;
using DateAccess.RavenDb.Models;
using MapperSharedLibrary.Models;

namespace DataAccess.Manager.Mapper
{
  internal class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      RegisterMapping<ICarBase, CarInputDto, CarOutputDto>(ResolveCarType());
      RegisterMapping<ICarDocumentBase, CarDocumentInputDto, CarDocumentOutputDto>(ResolveCarDocumentType());
      RegisterMapping<ICarPictureBase, CarPictureInputDto, CarPictureOutputDto>(ResolveCarPictureType());
      RegisterMapping<ICarDocumentHistoryBase, CarDocumentHistoryInputDto, CarDocumentHistoryOutputDto>(ResolveCarDocumentHistoryType());
      RegisterMapping<ICarUserBase, CarUserInputDto, CarUserOutputDto>(ResolveCarUserType());
    }

    private Type ResolveCarType()
    {
      Type result;
      switch (DiResolver.Provider)
      {
        case DatabaseProvider.MongoDb:
          result = typeof(MongoCar);
          break;
        case DatabaseProvider.RavenDb:
          result = typeof(RavenCar);
          break;
        case DatabaseProvider.Sql:
          result = typeof(Car);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return result;
    }

    private Type ResolveCarPictureType()
    {
      Type result;
      switch (DiResolver.Provider)
      {
        case DatabaseProvider.MongoDb:
          result = typeof(MongoCarPicture);
          break;
        case DatabaseProvider.RavenDb:
          result = typeof(RavenCarPicture);
          break;
        case DatabaseProvider.Sql:
          result = typeof(CarPicture);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return result;
    }

    private Type ResolveCarDocumentType()
    {
      Type result;
      switch (DiResolver.Provider)
      {
        case DatabaseProvider.MongoDb:
          result = typeof(MongoCarDocument);
          break;
        case DatabaseProvider.RavenDb:
          result = typeof(RavenCarDocument);
          break;
        case DatabaseProvider.Sql:
          result = typeof(CarDocument);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return result;
    }

    private Type ResolveCarDocumentHistoryType()
    {
      Type result;
      switch (DiResolver.Provider)
      {
        case DatabaseProvider.MongoDb:
          result = typeof(MongoCarDocumentHistory);
          break;
        case DatabaseProvider.RavenDb:
          result = typeof(RavenCarDocumentHistory);
          break;
        case DatabaseProvider.Sql:
          result = typeof(CarDocumentHistory);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return result;
    }

    private Type ResolveCarUserType()
    {
      Type result;
      switch (DiResolver.Provider)
      {
        case DatabaseProvider.MongoDb:
          result = typeof(MongoCarUser);
          break;
        case DatabaseProvider.RavenDb:
          result = typeof(RavenCarUser);
          break;
        case DatabaseProvider.Sql:
          result = typeof(CarUser);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return result;
    }


    private void RegisterMapping<T,  TInputDto, TOutputDto>(Type t) 
      where T : IDataModelBase
      where TInputDto : IInputDto
      where TOutputDto : IOutputDto
    {
      CreateMap(t, typeof(TOutputDto));
      CreateMap(typeof(TInputDto), t);
      CreateMap(typeof(T), typeof(TInputDto));

      CreateMap(typeof(TInputDto), typeof(T));
      CreateMap(typeof(T), typeof(TOutputDto));
      
      CreateMap(typeof(T), t);
      CreateMap(t, typeof(T));
      
      //CreateMap<TP, TOutputDto>(); ;
      //CreateMap<TInputDto, TP>();
      //CreateMap<T, TInputDto>();
      
      //CreateMap<TInputDto, T>();
      //CreateMap<T, TOutputDto>();

      //CreateMap<T, TP>();
      //CreateMap<TP, T>();
    }
    
    
  }
}