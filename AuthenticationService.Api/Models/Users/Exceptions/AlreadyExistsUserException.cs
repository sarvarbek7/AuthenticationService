using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class AlreadyExistsUserException : Xeption
    {
        public AlreadyExistsUserException(Exception innerException)
            : base(message: "User with id is already exists.", innerException)
        {
        }

        public AlreadyExistsUserException(Exception innerException, string value)
            : base(message: $"User with {value} is already exists.", innerException)
        {
        }
    }
}
