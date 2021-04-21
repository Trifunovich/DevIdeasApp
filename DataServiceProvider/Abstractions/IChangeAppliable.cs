using System.Threading.Tasks;

namespace DataServiceProvider.Abstractions
{
  public interface IChangeAppliable
  {
    Task SaveChanges();
  }
}