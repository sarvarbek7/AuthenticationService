using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class UserServiceException : Xeption
    {
        public UserServiceException(Xeption innerException)
            : base(message: "User service error occurs, please contact support.",
                  innerException)
        {
        }
    }
}
