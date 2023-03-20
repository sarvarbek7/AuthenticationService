using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class FailedUserServiceException : Xeption
    {
        public FailedUserServiceException(Exception innerException)
            : base(message: "User service is failed, please contact support", 
                  innerException)
        {
        }
    }
}
