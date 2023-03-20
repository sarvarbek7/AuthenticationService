using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class UserDependencyException : Xeption
    {
        public UserDependencyException(Xeption innerException)
            : base("User dependency error occured, please contact support.", innerException)
        {
        }
    }
}
