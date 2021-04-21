namespace AuthenticationLibrary.Models
{
  public static class UserRoleHelper
  {
    public static string GetUserRoleName(UserRole role)
    {
      return role.ToString();
    }
  }

}
