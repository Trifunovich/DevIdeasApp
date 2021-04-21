using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using DataServiceProvider.Abstractions;
using DataServiceProvider.FactoryImplementations;
using FakerSharedLibrary.FakeAbstractions;
using FakerSharedLibrary.Fakers;
using MapperSharedLibrary.Models;
using Microsoft.Xaml.Behaviors.Core;
using SharedCodeLibrary;

namespace WpfUi.NetFramework.DataServicesLoader.ViewModel
{
  class DisplayDataControlViewModel : ViewModelBase, IDisplayDataControlViewModel
  {
    private readonly ICarServiceFactory _carServiceFactory;
    private readonly ICarUserServiceFactory _carUserServiceFactory;
    private readonly ICarPictureServiceFactory _carPictureServiceFactory;
    private readonly ICarDocumentHistoryServiceFactory _carDocumentHistoryServiceFactory;
    private readonly ICarDocumentServiceFactory _carDocumentServiceFactory;
    private readonly IMapper _mapper;
    private DataType _selectedType;
    private ObservableCollection<IOutputDto> _dataCollection = new ObservableCollection<IOutputDto>();
    private int _loadCount = 20;
    private int _insertCount = 50;
    private bool _isLoading;

    public ObservableCollection<DataType> DataTypesCollection { get; } = new ObservableCollection<DataType>();


    public DisplayDataControlViewModel(ICarServiceFactory carServiceFactory,
      ICarUserServiceFactory carUserServiceFactory, ICarPictureServiceFactory carPictureServiceFactory,
      ICarDocumentHistoryServiceFactory carDocumentHistoryServiceFactory, ICarDocumentServiceFactory carDocumentServiceFactory,
      IMapper mapper)
    {
      _carServiceFactory = carServiceFactory;
      _carUserServiceFactory = carUserServiceFactory;
      _carPictureServiceFactory = carPictureServiceFactory;
      _carDocumentHistoryServiceFactory = carDocumentHistoryServiceFactory;
      _carDocumentServiceFactory = carDocumentServiceFactory;
      _mapper = mapper;
    }

    protected override void OnInitialActivate()
    {
      base.OnInitialActivate();
      foreach (var t in Enum.GetValues(typeof(DataType)).OfType<DataType>())
      {
        DataTypesCollection.Add(t);
      }

      SelectedType = DataTypesCollection.FirstOrDefault();
    }

    public ObservableCollection<IOutputDto> DataCollection
    {
      get => _dataCollection;
      private set
      {
        SetAndNotify(ref _dataCollection, value);
      }
    }


    public ICommand LoadingCommand => new ActionCommand(OnLoad);

    private async void OnLoad()
    {
      IsLoading = true;

      var res = await LoadData();

      foreach (var outputDto in res)
      {
        DataCollection.Add(outputDto);
      }

      OnPropertyChanged(nameof(DataCollection));
      IsLoading = false;
    }

    public bool IsLoading
    {
      get => _isLoading;
      set => SetAndNotify(ref _isLoading, value);
    }

    public int LoadCount
    {
      get => _loadCount;
      set => SetAndNotify(ref _loadCount, value);
    }


    public int InsertCount
    {
      get => _insertCount;
      set => SetAndNotify(ref _insertCount, value);
    }


    public DataType SelectedType
    {
      get => _selectedType;
      set
      {
        if (SetAndNotify(ref _selectedType, value))
        {
          DataCollection?.Clear();
        }
      }
    }

    public ICommand InsertCommand => new ActionCommand(OnInsert);

    private async void OnInsert(object obj)
    {
      int count = int.Parse(obj.ToString() ?? string.Empty);
      var res = await InsertData(count);
    }

    private async Task<int?> InsertData(int count)
    {
      int? result = null;

      try
      {
        switch (SelectedType)
        {
          case DataType.Car:
            result = await GetDataForInsert(count, new CarFaker(), _carServiceFactory);
            break;
          case DataType.CarUser:
            result = await GetDataForInsert(count, new CarUserFaker(), _carUserServiceFactory);
            break;
          case DataType.CarDocument:
            result = await GetDataForInsert(count, new CarDocumentFaker(), _carDocumentServiceFactory);
            break;
          case DataType.CarDocumentHistory:
            result = await GetDataForInsert(count, new CarDocumentHistoryFaker(), _carDocumentHistoryServiceFactory);
            break;
          case DataType.CarPicture:
            result = await GetDataForInsert(count, new CarPictureFaker(0, 100, 0, 100), _carPictureServiceFactory);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      finally
      {
        IsLoading = false;
      }

      return result;
    }


    private async Task<int?> GetDataForInsert<P, TInputDto, TOutputDto>(int count, DefaultFake<P> faker, IScopedServiceFactoryBase<IDataAccessService<TInputDto, TOutputDto>> factory)
    where P : FakeDataModelBase
    where TInputDto : IInputDto
    where TOutputDto : IOutputDto
    {
      using (var carService = factory.CreateService())
      {
        var fakes = faker.Generate(count);
        var mapped = fakes.Select(f => _mapper.Map<TInputDto>(f));
        return await carService.Insert(mapped);
      }
    }


    private async Task<IEnumerable<IOutputDto>> LoadData()
    {
      IEnumerable<IOutputDto> result = null;
      DataCollection.Clear();

      try
      {
        switch (SelectedType)
        {
          case DataType.Car:
            result = await GeLoadedData(_carServiceFactory);
            break;
          case DataType.CarUser:
            result = await GeLoadedData(_carUserServiceFactory);
            break;
          case DataType.CarDocument:
            result = await GeLoadedData(_carDocumentServiceFactory);
            break;
          case DataType.CarDocumentHistory:
            result = await GeLoadedData(_carDocumentHistoryServiceFactory);
            break;
          case DataType.CarPicture:
            result = await GeLoadedData(_carPictureServiceFactory);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      finally
      {
        IsLoading = false;
      }

      return result;
    }

    private async Task<IEnumerable<TOutputDto>> GeLoadedData<TInputDto, TOutputDto>(IScopedServiceFactoryBase<IDataAccessService<TInputDto, TOutputDto>> factory)
      where TInputDto : IInputDto
      where TOutputDto : IOutputDto
    {
      using (var carService = factory.CreateService())
      {
        var result = await carService.GetPage(new PagingParameters(1, LoadCount));
        return result;
      }
    }
  }
}
