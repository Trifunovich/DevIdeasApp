namespace DataAccess.Sql.Models
{
  class CarCarUser
  {
    public int CarId { get; set; }
    public Car Car { get; set; }

    public int CarUserId { get; set; }

    public CarUser CarUser { get; set; }
  }
}