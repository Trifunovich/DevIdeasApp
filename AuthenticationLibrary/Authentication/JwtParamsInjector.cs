using System;
using Microsoft.Extensions.Configuration;

namespace AuthenticationLibrary.Authentication
{
  class JwtParamsInjector : IJwtParamsInjector
  {
    private readonly IConfiguration _configuration;

    public JwtParamsInjector(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string ValidAudience => _configuration["JWT:ValidAudience"];
    public string ValidIssuer => _configuration["JWT:ValidIssuer"];
    public string Secret => _configuration["JWT:Secret"];
    public DateTime? Expires => DateTime.Now.AddMinutes(_configuration.GetSection("JWT:Expires").Get<double>());
    public string RefreshSecret => _configuration["JWT:RefreshSecret"];
    public DateTime? RefreshExpires => DateTime.Now.AddMinutes(_configuration.GetSection("JWT:RefreshExpires").Get<double>());
  }



}