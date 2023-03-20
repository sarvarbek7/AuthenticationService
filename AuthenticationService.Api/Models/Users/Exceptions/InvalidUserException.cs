using Xeptions;

namespace AuthenticationService.Api.Models.Users.Exceptions
{
    public class InvalidUserException : Xeption
    {
        public InvalidUserException()
            : base(message: "User is invalid, please fix it and try again.")
        {
        }

        public InvalidUserException(string parameterName, object value)
            : base(message: $"User {parameterName} can not be {value}, " +
                  $"please fix it and try again")
        { }
    }
}
