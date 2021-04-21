using AuthenticationLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationLibrary.Context
{
  class ApplicationDbContext : IdentityDbContext<AuthenticationUserModel>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RefreshModel> RefreshModels { get; set; }
  }
}
