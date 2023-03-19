using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using Xeptions;

namespace AuthenticationService.Api.Foundations.Users
{
    public partial class UserService
    {
        private delegate ValueTask<User> ReturningUserFunction();

        private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullUserException nullUserException)
            {
                throw CreateAndLogUserValidationException(nullUserException);
            }
        }

        private UserValidationException CreateAndLogUserValidationException(
            Xeption innerException)
        {
            var userValidationException = 
                new UserValidationException(innerException);

            this.loggingBroker.LogError(userValidationException);

            return userValidationException;
        }
    }
}
