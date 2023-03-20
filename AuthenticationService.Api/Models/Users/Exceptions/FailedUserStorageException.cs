using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class FailedUserStorageException : Xeption
    {
        public FailedUserStorageException(Exception innerException)
            : base(message: "User storage is failed.", innerException)
        {
        }
    }
}
