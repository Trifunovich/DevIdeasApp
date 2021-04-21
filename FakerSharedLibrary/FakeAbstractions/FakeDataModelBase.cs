using DataAccess.Core.Abstractions;

namespace FakerSharedLibrary.FakeAbstractions
{
  public abstract class FakeDataModelBase : DataModelBase
  {
    public string Id { get; set; }

    public override string GetId => Id;

    public override string ToString()
    {
      return $"{GetType().Name}: [Id: {Id} {base.ToString()}]";
    }
  }
}