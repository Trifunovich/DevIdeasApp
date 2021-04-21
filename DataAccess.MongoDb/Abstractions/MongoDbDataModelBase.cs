using System;
using DataAccess.Core.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.MongoDb.Abstractions
{
  public abstract class MongoDbDataModelBase : DataModelBase
  {
    [BsonId]
    public Guid Id { get; set; }

    public override string GetId => Id.ToString();

    public override string ToString()
    {
      return $"{GetType().Name}: [Id: {Id} {base.ToString()}]";
    }
  }
}