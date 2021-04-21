namespace DataAccess.Core.Abstractions
{
  public abstract class DataModelBase : DataModelAbstractionBase, IDataModelBase
  {
    public abstract string GetId { get; }
  }
}