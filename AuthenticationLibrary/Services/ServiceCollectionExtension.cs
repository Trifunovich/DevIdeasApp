using System;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationLibrary.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthenticationLibrary.Helpers;
using AuthenticationLibrary.Authentication;
using AuthenticationLibrary.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationLibrary.Services
{
  public static class ServiceCollectionExtension
  {
    
    public static IServiceCollection AddAuthenticationLibraryInternals(this IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlite(ConnectionHelper.SqlConnectionString));
      services.AddDefaultIdentity<AuthenticationUserModel>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();
      services.AddSingleton<IJwtParamsInjector, JwtParamsInjector>();
      services.AddScoped<IAuthenticationService, AuthenticationService>();


      var serviceProvider = services.BuildServiceProvider();
      var jwtParamInjector = serviceProvider.GetRequiredService<IJwtParamsInjector>();

      // Adding Authentication  
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })

      // Adding Jwt Bearer  
      .AddJwtBearer(options =>
      {
        string validAudience = jwtParamInjector.ValidAudience;
        string validIssuer = jwtParamInjector.ValidIssuer;
        string secret = jwtParamInjector.Secret;

        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = validAudience,
          ValidIssuer = validIssuer,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
          ClockSkew = TimeSpan.Zero
        };
      });

   
      return services;
    }
  }
}