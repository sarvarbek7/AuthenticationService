using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;

namespace AuthenticationService.Api.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateOnRegister(User user)
        {
            ValidateUserIsNotNull(user);
        }

        private void ValidateUserIsNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }
    }
}
