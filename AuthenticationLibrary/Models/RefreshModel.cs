using System;

namespace AuthenticationLibrary.Models
{
  internal class RefreshModel
  {
    public int Id { get; set; }

    public RefreshModel(DateTime expiresOn)
    {
      ExpiresOn = expiresOn;
    }

    public string RefreshToken { get; set; }

    public DateTime ExpiresOn { get; set; }

    public bool IsValid => ExpiresOn >= DateTime.Now;
    
    public string AuthenticationUserModelId { get; set; }
  }
}