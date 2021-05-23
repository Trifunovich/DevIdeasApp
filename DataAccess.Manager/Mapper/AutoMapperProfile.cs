using System;
using System.Collections.Generic;
using System.Linq;
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

      if (typeof(T).IsAssignableFrom(typeof(ICarUserBase)))
      {
        switch (DiResolver.Provider)
        {
          case DatabaseProvider.MongoDb:
            CreateMap<ICarUserBase, MongoCarUser>().ForMember(cu => cu.AllCars, opt => opt.MapFrom<MongoCarUserResolver>());
            //CreateMap(typeof(T), typeof(TOutputDto));
            break;
          case DatabaseProvider.RavenDb:
            CreateMap<ICarUserBase, RavenCarUser>().ForMember(cu => cu.AllCars, opt => opt.MapFrom<RavenCarUserResolver>());
            //CreateMap(typeof(T), typeof(TOutputDto));
            break;
          case DatabaseProvider.Sql:
            CreateMap<ICarUserBase, CarUser>().ForMember(cu => cu.AllCars, opt => opt.MapFrom<CarUserResolver>());
          
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      else
      {
        CreateMap(typeof(T), t);
        CreateMap(typeof(T), typeof(TOutputDto));
      }

      if (typeof(TInputDto).IsAssignableFrom(typeof(CarUserInputDto)))
      {
        CreateMap<CarUserInputDto, ICarUserBase>().ConvertUsing<RavenCarUserInputConverter>();
      }
      else
      {
        CreateMap(typeof(TInputDto), t);
      }

      if (t == typeof(CarUser))
      {
        CreateMap<CarUser, CarUserOutputDto>().BeforeMap(BeforeMapCarUser);
      }
      else
      {
        CreateMap(t, typeof(TOutputDto));
      }

      CreateMap(typeof(T), typeof(TInputDto));

      CreateMap(typeof(TInputDto), typeof(T));
    
      CreateMap(t, typeof(T));
    }

    private void BeforeMapCarUser(CarUser cu, CarUserOutputDto cuOutput)
    {
      cu.AllCars = cu.CarCarUsers.Select(c => c.Car as ICarBase).ToList();
    }
  }

  internal class CarCarUserResolver : IValueResolver<CarUser, CarUserOutputDto, List<CarOutputDto>>
  {
    public List<CarOutputDto> Resolve(CarUser source, CarUserOutputDto destination, List<CarOutputDto> destMember, ResolutionContext context)
    {
      var res = source.CarCarUsers.Select(c => context.Mapper.Map<CarOutputDto>(c)).ToList();
      return res;
    }
  }

  class RavenCarUserInputConverter : ITypeConverter<CarUserInputDto, ICarUserBase>
  {
    public ICarUserBase Convert(CarUserInputDto source, ICarUserBase destination, ResolutionContext context)
    {
      destination.AllCars = source.AllCars?.Select(s => context.Mapper.Map<CarInputDto>(source: s) as ICarBase).ToList();
      return context.Mapper.Map<RavenCarUser>(source);
    }
  }

  class RavenCarUserResolver: IValueResolver <ICarUserBase, RavenCarUser, List<ICarBase>> 
  {
    public List<ICarBase> Resolve(ICarUserBase source, RavenCarUser destination, List<ICarBase> destMember, ResolutionContext context)
    {
      return source.AllCars?.Select(s => context.Mapper.Map<RavenCar>(source: s) as ICarBase).ToList();
    }
  }

  class MongoCarUserResolver : IValueResolver<ICarUserBase, MongoCarUser, List<ICarBase>>
  {
    public List<ICarBase> Resolve(ICarUserBase source, MongoCarUser destination, List<ICarBase> destMember, ResolutionContext context)
    {
      return source.AllCars?.Select(s => context.Mapper.Map<MongoCar>(source: s) as ICarBase).ToList();
    }
  }

  class CarUserResolver : IValueResolver<ICarUserBase, CarUser, List<ICarBase>>
  {
    public List<ICarBase> Resolve(ICarUserBase source, CarUser destination, List<ICarBase> destMember, ResolutionContext context)
    {
      return source.AllCars?.Select(s => context.Mapper.Map<Car>(source: s) as ICarBase).ToList();
    }
  }


}