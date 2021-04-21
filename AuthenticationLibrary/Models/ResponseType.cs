namespace AuthenticationLibrary.Models
{
  public enum ResponseType
  {
    UserExists,
    UserDoesntExist,
    UserCreationFailed,
    UserSuccessfullyCreated,
    UnableToAuthenticate,
    OperationFailed,
    LoggedOut,
    CredentialsMissing,
    Successful
  }
}