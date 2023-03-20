using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class UserDependencyValidationException : Xeption
    {
        public UserDependencyValidationException(Xeption innerException)
            : base(message: "User dependency error occured.", innerException)
        {
        }
    }
}
