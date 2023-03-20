using System.Data;
using AuthenticationService.Api.Models.Users;
using AuthenticationService.Api.Models.Users.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            catch (InvalidUserException invalidUserException)
            {
                throw CreateAndLogUserValidationException(invalidUserException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsUserException =
                    new AlreadyExistsUserException(duplicateKeyException);

                throw CreateAndLogUserDependencyValidationException(alreadyExistsUserException);
            }
            catch (DuplicateKeyWithUniqueIndexException duplicateKeyWithUniqueIndexException)
            {
                var alreadyExistsUserException =
                    new AlreadyExistsUserException(duplicateKeyWithUniqueIndexException, 
                    duplicateKeyWithUniqueIndexException.DuplicateKeyValue);

                throw CreateAndLogUserDependencyValidationException(alreadyExistsUserException);
            }
            catch (SqlException sqlException)
            {
                var failedUserStorageException =
                    new FailedUserStorageException(sqlException);

                throw CreateAndLogUserDependencyException(failedUserStorageException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedUserStorageException =
                    new FailedUserStorageException(dbUpdateException);

                throw CreateAndLogUserDependencyException(failedUserStorageException);
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

        private UserDependencyValidationException CreateAndLogUserDependencyValidationException(
            Xeption innerException)
        {
            var userDependencyValidationException = 
                new UserDependencyValidationException(innerException);

            this.loggingBroker.LogError(userDependencyValidationException);

            return userDependencyValidationException;
        }

        private UserDependencyException CreateAndLogUserDependencyException(Xeption innerException)
        {
            var userDependencyException = new UserDependencyException(innerException);

            this.loggingBroker.LogCritical(userDependencyException);

            return userDependencyException;
        }
    }
}
