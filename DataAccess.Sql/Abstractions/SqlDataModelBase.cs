using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Dapper.Contrib.Extensions;
using DataAccess.Core.Abstractions;

[assembly: InternalsVisibleTo("DataAccess.Tests")]
namespace DataAccess.Sql.Abstractions
{
  public interface ISqlDataModelBase : IDataModelBase
  {
    int Id { get; set; }
  }

  public abstract class SqlDataModelBase : DataModelBase, ISqlDataModelBase
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public override string GetId => Id.ToString();

    public override string ToString()
    {
      return $"{GetType().Name}: [Id: {Id} {base.ToString()}]";
    }
  }
}
