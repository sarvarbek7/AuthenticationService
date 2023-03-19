using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;

namespace AuthenticationService.Api.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateOnRegister(User user)
        {
            ValidateUserIsNotNull(user);
            ValidateUserPhoneNumberIsNotNull(user);
        }

        private void ValidateUserIsNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private void ValidateUserPhoneNumberIsNotNull(User user)
        {
            if (user.PhoneNumber is null)
            {
                throw new InvalidUserException(
                    parameterName: nameof(User.PhoneNumber),
                    value: user.PhoneNumber);
            }
        }
    }
}
