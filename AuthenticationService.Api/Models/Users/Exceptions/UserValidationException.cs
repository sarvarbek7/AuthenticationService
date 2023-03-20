using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class UserValidationException : Xeption
    {
        public UserValidationException(Xeption innerException)
            : base(
                  message: "User validation error occured, please fix it and try again.",
                  innerException)
        { }
    }
}
