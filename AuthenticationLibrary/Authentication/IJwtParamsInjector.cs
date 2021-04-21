using System;

namespace AuthenticationLibrary.Authentication
{
  interface IJwtParamsInjector
  {
    string ValidAudience { get; }
    string ValidIssuer { get; }
    string Secret { get; }
    DateTime? Expires { get;}

    string RefreshSecret { get; }

    DateTime? RefreshExpires { get; }

  }
}