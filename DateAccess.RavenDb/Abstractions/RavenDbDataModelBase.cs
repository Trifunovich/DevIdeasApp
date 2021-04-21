using DataAccess.Core.Abstractions;

namespace DateAccess.RavenDb.Abstractions
{
  public abstract class RavenDbDataModelBase : DataModelBase
  {
    public string Id { get; set; }

    public override string GetId => Id;

    public override string ToString()
    {
      return $"{GetType().Name}: [Id: {Id} {base.ToString()}]";
    }
  }
}