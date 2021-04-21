using System.Threading.Tasks;
using AuthenticationLibrary.Models;

namespace AuthenticationLibrary.Authentication
{
  public interface IAuthenticationService
  {
    Task<ResponseModel> TryToLogin(LoginModel model);
    Task<ResponseModel> Register(RegisterModel model);
    Task<ResponseModel> RegisterAdmin(RegisterModel model);
    Task<ResponseModel> TryToRefresh(string refreshToken);
    Task<ResponseModel> LogOut(string username);
  }
}