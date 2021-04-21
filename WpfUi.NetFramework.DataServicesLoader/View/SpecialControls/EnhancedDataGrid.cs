using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfUi.NetFramework.DataServicesLoader.View.SpecialControls
{
  public class EnhancedDataGrid : DataGrid
  {
    public ArrayList BoundColumns
    {
      get => (ArrayList)GetValue(BoundColumnsProperty);
      set => SetValue(BoundColumnsProperty, value);
    }

    public static readonly DependencyProperty BoundColumnsProperty =
      DependencyProperty.Register(
        nameof(BoundColumns), typeof(ArrayList),
        typeof(EnhancedDataGrid));

    private void OnDataGridColumnsPropertyChanged()
    {
      Columns.Clear();

      foreach (var o in BoundColumns.OfType<DataGridColumn>())
      {
        Columns.Add(o);
      }
    }

    public EnhancedDataGrid()
    {
      Loaded += EnhancedDataGrid_Loaded;
      Unloaded += EnhancedDataGrid_Unloaded;
    }

    private void EnhancedDataGrid_Unloaded(object sender, RoutedEventArgs e)
    {
      TargetUpdated -= EnhancedDataGrid_TargetUpdated;
    }

    private void EnhancedDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
      TargetUpdated += EnhancedDataGrid_TargetUpdated;
    }

    private void EnhancedDataGrid_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
    {
      if (BoundColumns.Count != Columns.Count)
      {
        OnDataGridColumnsPropertyChanged();
      }
    }
  }
}