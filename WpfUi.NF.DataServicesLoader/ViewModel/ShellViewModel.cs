using WpfUi.NF.DataServicesLoader.Factories;

namespace WpfUi.NF.DataServicesLoader.ViewModel
{
  public interface IShellViewModel
  {
  }

  public class ShellViewModel : ViewModelBase, IShellViewModel
  {
    private readonly IDisplayDataControlViewModelFactory _factory;

    public ShellViewModel(IDisplayDataControlViewModelFactory factory)
    {
      _factory = factory;
      DisplayName = "Testing the data stores using Wpf!";
    }

    protected override void OnInitialActivate()
    {
      base.OnInitialActivate();

      ActiveItem = _factory.CreateViewModel() as ViewModelBase;
    }
  }
}