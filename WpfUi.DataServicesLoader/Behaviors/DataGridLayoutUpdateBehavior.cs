using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

namespace WpfUi.DataServicesLoader.Behaviors
{
  public class DataGridLayoutUpdateBehavior : Behavior<DataGrid>
  {
    public ArrayList BoundColumns
    {
      get => (ArrayList)GetValue(BoundColumnsProperty);
      set
      {
        SetValue(BoundColumnsProperty, value);
        OnDataGridColumnsPropertyChanged();
      }
    }

    public static readonly DependencyProperty BoundColumnsProperty =
      DependencyProperty.Register(
        nameof(BoundColumns), typeof(ArrayList),
        typeof(DataGridLayoutUpdateBehavior));

    protected override void OnAttached()
    {
      base.OnAttached();
      AssociatedObject.TargetUpdated += AssociatedObject_TargetUpdated;
    }

   

    private void AssociatedObject_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
    {
      if (BoundColumns.Count != AssociatedObject.Columns.Count)
      {
        OnDataGridColumnsPropertyChanged();
      }
    }
    

    protected override void OnDetaching()
    {
      base.OnDetaching();
      AssociatedObject.TargetUpdated -= AssociatedObject_TargetUpdated;
    }

    private  void OnDataGridColumnsPropertyChanged()
    {
      AssociatedObject.Columns.Clear();

      foreach (var o in BoundColumns.OfType<DataGridColumn>())
      {
        AssociatedObject.Columns.Add(o);
      }
    }
  }
}