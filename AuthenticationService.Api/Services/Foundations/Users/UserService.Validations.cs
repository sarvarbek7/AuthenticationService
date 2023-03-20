using System.Text.RegularExpressions;
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
            ValidateUserPhoneNumberIsCorrectFormat(user);
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

        private void ValidateUserPhoneNumberIsCorrectFormat(User user)
        {
            string uzbPhoneNumberFormat = @"^\+998[9873][01345789][0-9]{7}$";

            Match match = Regex.Match(user.PhoneNumber, uzbPhoneNumberFormat);

            if (!match.Success)
            {
                throw new InvalidUserException(
                    parameterName: nameof(User.PhoneNumber), 
                    value: user.PhoneNumber);
            }    
        }
    }
}
