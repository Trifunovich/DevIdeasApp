using System;

namespace AuthenticationLibrary.Models
{
  public class ResponseModel
  {
    public string Message { get; }
    public ResponseType ResponseType {get; set;}

    public dynamic ResponseData { get; set; }

    public ResponseModel(ResponseType responseType)
    {
      ResponseType = responseType;

      Message = responseType switch
      {
        ResponseType.UserExists => "User already exists!",
        ResponseType.UserCreationFailed => "User creation failed! Please check user details and try again.",
        ResponseType.UserSuccessfullyCreated => "User created successfully!",
        ResponseType.UserDoesntExist => "User doesn't exist!",
        ResponseType.UnableToAuthenticate => "Unable to authenticate",
        ResponseType.OperationFailed => "Operation has failed",
        ResponseType.LoggedOut => "Logged out!",
        ResponseType.CredentialsMissing => "Credentials missing",
        ResponseType.Successful => "Success",
        _ => throw new ArgumentOutOfRangeException(nameof(responseType), responseType, null)
      };
    }
  }
}
